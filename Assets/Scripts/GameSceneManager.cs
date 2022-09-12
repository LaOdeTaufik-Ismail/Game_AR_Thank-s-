using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    public static bool easy;
    public static bool normal;
    public static bool hard;
    public static float delayTime = 0.3f;
    public void DificulityMenu()
    {
        easy = false;
        normal = false;
        hard = false;
        Invoke("toSceneDificulityMenus", delayTime);
    }
    public void StartEasy()
    {
        Invoke("toSceneStartEasy", delayTime);
    }
    public void StartNormal()
    {
        Invoke("toSceneStartNormal", delayTime);
    }
    public void StartHard()
    {
        Invoke("toSceneStartHard", delayTime);
    }
    public void MainGame()
    {
        Invoke("toSceneMainGame", delayTime);
    }
    public void StartAgain()
    {
        Invoke("toSceneStartAgain", delayTime);
    }
    public void SureQuit()
    {
        Invoke("toSceneSureQuit", delayTime);
    }
    public void QuitGame()
    {
        Invoke("toSceneQuitGame", delayTime);
    }
    public void Menang()
    {
        Invoke("toSceneMenang", delayTime);
    }
    public void PilihTank()
    {
        Invoke("toScenePilihTank", delayTime);
    }


    //toScene no Delay
    void toSceneDificulityMenus()
    {
        SceneManager.LoadScene(4);
    }

    void toSceneSureQuit()
    {
        SceneManager.LoadScene(3);
    }

    void toSceneQuitGame()
    {
        Application.Quit();
    }

    void toSceneStartAgain()
    {
        SceneManager.LoadScene(0);
    }

    void toSceneMainGame()
    {
        if (easy == true)
        {
            Debug.Log("Main Easy");
            SceneManager.LoadScene(6);
        }
        else if (normal == true)
        {
            SceneManager.LoadScene(7);
        }
        else if (hard == true)
        {
            SceneManager.LoadScene(8);
        }
    }

    void toSceneStartHard()
    {
        hard = true;
        SceneManager.LoadScene("Intro");
        Debug.Log(hard);
    }

    void toSceneStartEasy()
    {
        easy = true;
        SceneManager.LoadScene("Intro");
        Debug.Log(easy);
    }

    void toSceneStartNormal()
    {
        normal = true;
        SceneManager.LoadScene("Intro");
        Debug.Log(normal);
    }

    void toSceneMenang()
    {
        SceneManager.LoadScene(2);     
    }
    void toScenePilihTank()
    {
        SceneManager.LoadScene(10);
    }
}
