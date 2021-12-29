using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

/// <summary>
/// Создание случайной карты
/// </summary>
public class MapCreator : MonoBehaviour
{
    [Header("Ообщие переменные")] public Vector2Int size; //размер поля
    [Header("Переменные базовово слоя")] public Tilemap baseMap; //базовый слой
    public Tilemap baseMapBorders; //границы карты
    public Tile baseTile; // базовые тайлы
    [Header("Переменные слоя объектов")] public Tilemap objectMap; //слой с препятствиями
    public Tile objectTile; //Тайлы препятствий
    public float zoom; // зум шума
    public Vector2 offset; //смещение (потом случайное)
    public float intensiv; //интенсивность пятен
    public float catPlane; //понижение слоя

    [Header("Переменные спан генератора")]
    public LayerMask spawnMask; //маска спавна
    public float range; //размер зоны
    public GameObject spawnDestroy; //объект для удаления
    public GameObject build; //здание 
    public int quantitySpawn = 1; //кол-во спавнеров
    public GameObject spawn; //спавнер

    [Header("Префабы героя и солад для спавна")]
    public GameObject hero;
    public GameObject solider;

    // Start is called before the first frame update
    void Start()
    {
        Collider2D attack = Physics2D.OverlapCircle(transform.position, range, spawnMask);

        
        GenerateMapBase(); //простая земял
        GenerateMapBorders(); //границы
        GenerateObjectMap(); ///непроходимые объекты

        Physics2D.SyncTransforms();
        GetComponent<NavMeshSurface2d>().BuildNavMesh();
        
        GenerateBuild(); // база
        //GenerateSpawner(attack); //спавн
        GeneratePositionSpawn();

        SpawnHeroSolider();//появление героя и солдат
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// Генерация базового слоя с исплользаванием двумерного массива
    /// </summary>
    void GenerateMapBase() //генерация базового слоя на карте
    {
        for (int x = -size.x; x < size.x; x++)
        {
            for (int y = -size.y; y < size.y; y++)
            {
                baseMap.SetTile(new Vector3Int(x, y, 0), baseTile);
            }
        }
    }
    /// <summary>
    /// Генерация границ карты с использованем массива, больщего чем базовый слой и заполнением его простыми действиями
    /// </summary>
    void GenerateMapBorders() // генерация границ карты
    {
        for (int x = -size.x - 1; x < size.x + 1; x++)
        {
            for (int y = -size.y - 1; y < size.y + 1; y++)
            {
                baseMapBorders.SetTile(new Vector3Int(x, size.y, 0), baseTile);
                baseMapBorders.SetTile(new Vector3Int(x, -size.y - 1, 0), baseTile);
                baseMapBorders.SetTile(new Vector3Int(size.x, y, 0), baseTile);
                baseMapBorders.SetTile(new Vector3Int(-size.x - 1, y, 0), baseTile);
            }
        }
    }
    /// <summary>
    /// Генерация препятствий с использованием шума перлина и функцией интесива для перевода значений в цвета
    /// </summary>
    void GenerateObjectMap() // генрерация препятствий
    {
        offset.x = Random.Range(-1000, 1000);
        offset.y = Random.Range(-1000, 1000);

        for (int x = -size.x; x < size.x; x++)
        {
            for (int y = -size.y; y < size.y; y++)
            {
                float gradient = Mathf.PerlinNoise((x + offset.x) / zoom, (y + offset.y) / zoom) * intensiv; //шум
                objectTile.color = GetColorByIntensive(gradient); //перевод значения в цвет
                if (gradient > catPlane) //проверка что шум выше высоты и можно рисовать
                {
                    if (objectTile.color == Color.gray)//проверка на сопостовление цвета и отрисовка тайла
                    {
                        objectMap.SetTile(new Vector3Int(x, y, 0), objectTile);
                    }
                }

            }
        }
    }
    /// <summary>
    /// Функция расстановки спавна врагов
    /// </summary>
    /// <param name="attack">Атрибут который возвращает тру если в радиусе имеется какойто оъект</param>
    void GenerateSpawner(Collider2D attack)
    {
        do 
        {
            GeneratePositionSpawn();
            if (attack) //если равен тру то....
            {
                spawnDestroy = attack.transform.gameObject; //добавляется в переменную
                Destroy(spawnDestroy); //уничтожается
            }
            else //если не тру то оставляем на месте
            {
                quantitySpawn--;  
            }
        } while (quantitySpawn > 0); //выполняем цикл пока не будет ноль
    }

    /// <summary>
    /// Функция для спавна 
    /// </summary>
    void GenerateBuild()
    {
        /// <summary>
        /// проверка установка базы
        /// данный цикл позволяет бесконечно проверять установлена ли база, если она не установлена, то выходим из цикла
        /// </summary>
        int x = 0;
        do
        {

        int posX = Mathf.RoundToInt(Random.Range(-range, range)); //позиция на карте мировой
        int posY = Mathf.RoundToInt(Random.Range(-range, range)); //позиция на карте мировой
            
        //получение позиции 
        Vector3 position = objectMap.CellToLocal(new Vector3Int(posX, posY, 0)); //получения позиции из локальной в мировую
        Vector3Int positionInt = new Vector3Int(posX, posY, 0); //локальная переменная вектора типа int
        var tile = objectMap.GetTile(positionInt); //получение спрайта с импользованием вектора инт

            if (tile == null) //если спрайт пустой, то спавним объект
            {
                //Instantiate(spawn, new Vector2(posX, posY), Quaternion.identity);
                //создаем спрайт и добавляем его в переменную
                var obj = Instantiate(build, Vector3Int.RoundToInt(position), Quaternion.identity);
                obj.name = "Base";
                //Instantiate(spawn, Vector3Int.RoundToInt(position), Quaternion.identity, transform.parent);
                obj.transform.parent = gameObject.transform; //делаем дочерним
                x += 1;
               
            };
            if(x == 1) break;
        } while (true);


    }
    //Вспомагаетельные функции
    
    
    /// <summary>
    /// Функцидля создания цвета шума перлина и вывода его в цвет
    /// </summary>
    /// <param name="input"> Пареметр градиента</param>
    /// <returns>Возвращаеит цвет</returns>
    
    public Color GetColorByIntensive(float input) //испозование цвета для создания препятствий
    {
        Color output = new Color(); // переменная для вывода
        if (input >= 0.7f) // проверка на соответвие и вывод цвета спрайта
        {
            output = Color.gray;
        }

        return output;
    }
    
    /// <summary>
    /// Функция для создания свавна на карте тайлов и проверкой что тайл не закрашен
    /// </summary>
    void GeneratePositionSpawn()
    {
        do
        {
            int posX = Mathf.RoundToInt(Random.Range(-size.x + 1, size.x - 1)); //позиция на карте мировой
            int posY = Mathf.RoundToInt(Random.Range(-size.y + 1, size.y - 1)); //позиция на карте мировой
            
            //получение позиции 
            Vector3 position = objectMap.CellToLocal(new Vector3Int(posX, posY, 0)); //получения позиции из локальной в мировую
            Vector3Int positionInt = new Vector3Int(posX, posY, 0); //локальная переменная вектора типа int
            var tile = objectMap.GetTile(positionInt); //получение спрайта с импользованием вектора инт

            //сравнение позиции
            GameObject baseBuild = GameObject.Find("Base");
            float comparisonDistans = Vector3.Distance(baseBuild.transform.position, position);

            if ((tile == null) && (comparisonDistans > 20)) //если спрайт пустой, то спавним объект
            {
                //Instantiate(spawn, new Vector2(posX, posY), Quaternion.identity);
                //создаем спрайт и добавляем его в переменную
                var obj = Instantiate(spawn, Vector3Int.RoundToInt(position), Quaternion.identity);
                //Instantiate(spawn, Vector3Int.RoundToInt(position), Quaternion.identity, transform.parent);
                obj.transform.parent = gameObject.transform; //делаем дочерним
                quantitySpawn--;
            }
            if (quantitySpawn == 0) break;
        } while (true);
 
    }

    /// <summary>
    /// появление героя и солдат
    /// </summary>
    public void SpawnHeroSolider()
    {
        GameObject spawnUnit = build.transform.GetChild(4).gameObject; //Получаем координаты спавна

        var obj = Instantiate(hero, spawnUnit.transform.position, Quaternion.identity); //Спавним героя, добавляем в переменную и ....
        obj.name = "Hero"; //... меняем имя чтобы можно было найти объект на сцене
        for (int i = 0; i < 4; i++)
        {
            Instantiate(solider, spawnUnit.transform.position, Quaternion.identity); //Спавним юнит
        }
    }

    /// <summary>
    /// Отрисовка зоны в которой нельзя расставлять спавны и можно спавниить базу 
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
