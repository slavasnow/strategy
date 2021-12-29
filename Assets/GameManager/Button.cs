using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [Header("панель магазина и его настройки")]
    //панель магазина и его настройки
    public GameObject storePanel; //панель магазина
    public bool @selectStore; //селектор проверки открытия
    public GameObject solider; //префаб солдата первого уровня
    public GameObject solider2; //префаб солдата второго уровня
    public GameObject solider3; //префаб солдата третьего уровня

    public GameObject baseBuild; //место появления, а так же объект базы для прокачки

    [Header("панель повышения уровня и его настройки")]
    //
    public GameObject updatePanel; //панель повышения уровня
    public bool @selectUpdate; //селектор проверки открытия
    public GameObject hero; //префаб героя

    [Header("панель меню и его настройки")]
    //панель меню и его настройки
    public GameObject menuPanel;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        baseBuild = GetComponent<Manager>().baseBuild; //получение объекта базы
        hero = GetComponent<Manager>().hero; //получения объекта героя
    }
    /// <summary>
    /// кнопки косающиеся панели магазина покупка клонов
    /// </summary>
    public void SpawnSolider() //покупука солдата первого уроня
    {
        if (GetComponent<Manager>().moneyInt >= 150) //если хватает денег, то нанимаем солдата
        {
            GameObject spawnUnit = baseBuild.transform.GetChild(4).gameObject; //Получаем координаты спавна
            Instantiate(solider, spawnUnit.transform.position, Quaternion.identity); //Спавним юнит
            GetComponent<Manager>().moneyInt -= 150; //отмимаем деньги
        }
    }
    public void SpawnSlider2() //спавн солдата второго уровня
    {
        if (GetComponent<Manager>().moneyInt >= 200) //если хватает денег, то нанимаем солдата
        {
            GameObject spawnUnit = baseBuild.transform.GetChild(4).gameObject; //Получаем координаты спавна
            Instantiate(solider2, spawnUnit.transform.position, Quaternion.identity); //Спавним юнит
            GetComponent<Manager>().moneyInt -= 200; //отмимаем деньги
        }
    }
    public void SpawnSlider3() //спавн солдата второго уровня
    {
        if (GetComponent<Manager>().moneyInt >= 250) //если хватает денег, то нанимаем солдата
        {
            GameObject spawnUnit = baseBuild.transform.GetChild(4).gameObject; //Получаем координаты спавна
            Instantiate(solider3, spawnUnit.transform.position, Quaternion.identity); //Спавним юнит
            GetComponent<Manager>().moneyInt -= 250; //отмимаем деньги
        }
    }
    /// <summary>
    /// Кнопки касающиеся повышения уровня
    /// </summary>
    /// 
    public void UpdateHero() //увеличение уровня героя на 1 + 
    {
        if ((GetComponent<Manager>().moneyInt >= 1000) && (hero.GetComponent<Attributes>().level < 3)) //если хватает денег, то нанимаем солдата
        {
            hero.GetComponent<Attributes>().level += 1; //добавляем уровень герою
            hero.GetComponent<Attributes>().damage *= 2; //умножаем урон на 2 у героя
            hero.GetComponent<Attributes>().fireRate -= 0.2f; //увеличиваем скорострельнось
            hero.GetComponent<Attributes>().health = 100; //востанавливаем здоровье 
            GetComponent<Manager>().moneyInt -= 1000; //отмимаем деньги
        }
    }

    public void UpdateBase() //увеличение уровня базы на 1 + 
    {
        if ((GetComponent<Manager>().moneyInt >= 1000) && (baseBuild.GetComponent<Attributes>().level < 3)) //если хватает денег, то нанимаем солдата
        {
            baseBuild.GetComponent<Attributes>().level += 1; //добавляем уровень герою
            GetComponent<Manager>().moneyInt -= 1000; //отмимаем деньги
        }
    }

    public void CloseStorePanel() //закрытие панели магазина
    {
        storePanel.SetActive(false);
    }

    /// <summary>
    /// Выход из игры
    /// </summary>
    public void ExitGame() //переигровка
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }

    /// <summary>
    /// Кнопки меню
    /// </summary>

    public void MenuPause() //кнопка паузы и отрыктия меню
    {
        menuPanel.SetActive(true);
        GetComponent<Manager>().hudpanel.SetActive(false);
        Time.timeScale = 0;
    }

    public void MenuExit() //выхода из меню
    {
        menuPanel.SetActive(false);
        GetComponent<Manager>().hudpanel.SetActive(true);
        Time.timeScale = 1;
    }
}
