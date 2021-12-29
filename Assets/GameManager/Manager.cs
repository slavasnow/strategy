using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text money; //надпись кошелька
    public int moneyInt; //переменная, которая увеличивается если умират враг, и уменьшается, если происходит покупка

    public GameObject spawnSolider2; //кнопка спана клонов 2 уровня
    public GameObject spawnSolider3; //кнопка спана клонов 2 уровня

    public Text updateHero; //кнопка апгрейда героя
    public Text updateBase; //кнопка апгрейда базы

    //счетчик времени 
    private float count = 1; //отсчет милисекнд
    public Text time; //текстовое поле 
    public class Timer //клас таймера для более простой насиройки 
    {
        public int hour;
        public int minute;
    }
    public Timer timer = new Timer();

    //Отслеживание объектов и запрешение игры 
    public GameObject baseBuild; //объект здания
    public GameObject hero; //объект героя
    public GameObject gameOver; //объект панели
    public GameObject hudpanel; //объект панели

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        money.GetComponent<Text>().text = $"{moneyInt}";//описание денег

        //описание вермени
        count -= Time.deltaTime;
        if (count <= 0)
        {
            timer.minute += 1;
            if (timer.minute == 60)
            {
                timer.hour += 1;
                timer.minute = 0;
            }
            count = 1;
        }
        time.GetComponent<Text>().text = $" {timer.hour} : {timer.minute}";

        //окончание игрового процесса

        baseBuild = GameObject.Find("/MapCreator/Base"); //ищим объект базы на сцене
        hero = GameObject.Find("Hero"); //ищим объект героя на сцене



        if ((baseBuild == null) | (hero == null)) //как только какой то из объектов изчезает на конец игры 
        {
            gameOver.SetActive(true);
            hudpanel.SetActive(false);
            //Time.timeScale = 0;
        }

        switch (baseBuild.GetComponent<Attributes>().level) //проверка и показ кнопок закупку клонов
        {
            case 2:
                spawnSolider2.SetActive(true);
                break;
            case 3:
                spawnSolider3.SetActive(true);
                updateBase.GetComponent<Text>().text = "MAX";
                break;
        }

        if (hero.GetComponent<Attributes>().level == 3) { updateHero.GetComponent<Text>().text = "MAX"; }
    }
}
