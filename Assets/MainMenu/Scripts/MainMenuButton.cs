using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
    /// <summary>
    /// Загрузка игровой сцены по имени сцены
    /// </summary>
    public void NewGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    /// <summary>
    /// Закрытие приложения 
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
