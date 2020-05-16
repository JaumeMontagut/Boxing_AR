using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject mainmenu;
    public GameObject credits;

    public void LoadScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void GoToCredits()
    {
        mainmenu.SetActive(false);
        credits.SetActive(true);
    }

    public void GoToMainMenu()
    {
        credits.SetActive(false);
        mainmenu.SetActive(true);
    }

    public void Github(string url)
    {
        Application.OpenURL(url);
    }
}
