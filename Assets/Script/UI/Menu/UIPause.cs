using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

     void Update()
    {
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Resume()
    {
        PauseMenuPanel.SetActive(false);
        Time.timeScale = 1f;

    }

    public void Exit()
    {
        Resume();
        SceneManager.LoadScene(0);
    }
}
