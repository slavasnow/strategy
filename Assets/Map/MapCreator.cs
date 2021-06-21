using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;
/// <summary>
/// Создание случайной карты
/// </summary>
public class MapCreator : MonoBehaviour
{
    [Header("Ообщие переменные")]
    public Vector2Int size; //размер поля
    [Header("Переменные базовово слоя")]
    public Tilemap baseMap; //базовый слой
    public Tilemap baseMapBorders; //границы карты
    public Tile baseTile; // базовые тайлы
    [Header("Переменные слоя объектов")] 
    public Tilemap objectMap;//слой с препятствиями
    public Tile objectTile; //Тайлы препятствий
    public float zoom; // зум шума
    public Vector2 offset; //смещение (потом случайное)
    public float intensiv; //интенсивность пятен
    public float catPlane; //понижение слоя
    
    // Start is called before the first frame update
    void Start()
    {
        GenerateMapBase();
        GenerateMapBorders();
        GenerateObjectMap();
        Physics2D.SyncTransforms();
        GetComponent<NavMeshSurface2d>().BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateMapBase() //генерация базового слоя на карте
    {
        for (int x = -size.x; x < size.x; x++)
        {
            for (int y = -size.y; y < size.y; y++)
            {
                baseMap.SetTile(new Vector3Int(x,y,0), baseTile);
            }
        }
    }

    void GenerateMapBorders() // генерация границ карты
    {
        for (int x = -size.x; x < size.x; x++)
        {
            for (int y = -size.y; y < size.y; y++)
            {
                baseMapBorders.SetTile(new Vector3Int(x,size.y,0), baseTile);
                baseMapBorders.SetTile(new Vector3Int(x,-size.y,0), baseTile);
                baseMapBorders.SetTile(new Vector3Int(size.x,y,0), baseTile);
                baseMapBorders.SetTile(new Vector3Int(-size.x,y,0), baseTile);
            }
        }
    }

    void GenerateObjectMap() // генрерация препятствий
    {
        for (int x = -size.x; x < size.x; x++)
        {
            for (int y = -size.y; y < size.y; y++)
            {
                float gradient = Mathf.PerlinNoise((x+offset.x) / zoom, (y+offset.y) / zoom) * intensiv;
                objectTile.color = GetColorByIntensive(gradient);
                if (gradient > catPlane)
                {
                    if (objectTile.color == Color.gray)
                    { 
                        objectMap.SetTile(new Vector3Int(x,y,0), objectTile);
                    }
                }
                
            }
        }
    }
    
    public Color GetColorByIntensive(float input) //испозование цвета для создания препятствий
    {
        Color output = new Color(); // переменная для вывода
        if (input >= 0.7f) // проверка на соответвие и вывод цвета спрайта
        {
            output = Color.gray;
        }
        return output;
    }
}
