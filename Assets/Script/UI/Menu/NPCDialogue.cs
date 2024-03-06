using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
  
    private Transform player;
    private SpriteRenderer speechBubbRenderer;

    public GameObject PopupSignMission;
    public GameObject PopupInstruction;
    private bool isActive= false;
    void Start()
    {
        speechBubbRenderer = GetComponent<SpriteRenderer>();
        speechBubbRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            
            speechBubbRenderer.enabled = true;
            player = collision.gameObject.GetComponent<Transform>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isActive)
                {
                    PopupSignMission.SetActive(false);
                    isActive = false;
                }
                else
                {
                    isActive = true;
                    PopupSignMission.SetActive(true);
                }
            }
        
                    
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PopupSignMission.SetActive(false);

            speechBubbRenderer.enabled=false;
        }
    }
    
    public void ShowInstruction()

}
