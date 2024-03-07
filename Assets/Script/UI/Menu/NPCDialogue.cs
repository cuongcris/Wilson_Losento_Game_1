using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NPCDialogue : MonoBehaviour
{
  
    private Transform player;
    private SpriteRenderer speechBubbRenderer;

    public GameObject PopupSignMission;
    private bool isActiveMissionPopup= false;
    void Start()
    {
        speechBubbRenderer = GetComponent<SpriteRenderer>();
        speechBubbRenderer.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            speechBubbRenderer.enabled = true;
            player = collision.gameObject.GetComponent<Transform>();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (isActiveMissionPopup)
                {
                    PopupSignMission.SetActive(false);
                    isActiveMissionPopup = false;
                }
                else
                {
                    isActiveMissionPopup = true;
                    PopupSignMission.SetActive(true);
                }
            }
        
                    
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(PopupSignMission.IsDestroyed()==false)
            {
                PopupSignMission.SetActive(false);
            }
            speechBubbRenderer.enabled=false;
        }
    }
    
   

}
