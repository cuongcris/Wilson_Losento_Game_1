using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIPause : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenuPanel;
    [SerializeField] public GameObject InstructionMenuButton;
    private bool isInstructionShow = false;
    public void Pause()
    {
        PauseMenuPanel.SetActive(true);
        Time.timeScale = 0f;
    }
    public void ShowInstruction()
    {
        InstructionMenuButton.SetActive(true);
        isInstructionShow = true;
        Time.timeScale = 0f;
    }

    void Update()
    {
        
       if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isInstructionShow == true)
            {
                InstructionMenuButton.SetActive(false);
                isInstructionShow = false;
                Time.timeScale = 1f;
            }
            else
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
