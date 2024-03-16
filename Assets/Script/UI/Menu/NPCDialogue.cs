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
    bool checkkey = false;
    void Start()
    {
        speechBubbRenderer = GetComponent<SpriteRenderer>();
        speechBubbRenderer.enabled = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            checkkey = true;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Player")
        {

            speechBubbRenderer.enabled = true;
            //player = collision.gameObject.GetComponent<Transform>();
            if (checkkey)
            {
                PopupSignMission.SetActive(!isActiveMissionPopup);
                isActiveMissionPopup = !isActiveMissionPopup;
                checkkey = false;

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
