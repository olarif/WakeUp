using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //load next scene in line
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        //SceneManager.LoadScene("Prototype");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
