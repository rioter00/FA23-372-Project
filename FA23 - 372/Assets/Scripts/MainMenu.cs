using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
   public void PlayButton()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex +  1);
    }

    public void HowToPlay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);

    }

    public void Credits()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 3);
    }
}
