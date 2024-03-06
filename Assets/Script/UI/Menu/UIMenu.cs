using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenu : MonoBehaviour
{
    public void NewGame ()
    {
        SceneManager.LoadScene(1);

        // Play button click sound after loading scene
    }

    public void ExitGame()
    {
        Application.Quit();
    }
  
}
