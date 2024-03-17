using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Cinemachine.DocumentationSortingAttribute;

public class NextLevelTriiger : MonoBehaviour
{
    public GameObject cameraBoundObject;
    public Vector2 level2 = new Vector2(132.2f, 5.098955f);
    public Text mission;
    public PlayerController playerController;
    public string missionText = "";

    public int offset;

    BoxCollider2D cl;
    public GameObject Player;
    private void Start()
    {
        offset = 0;
        cl = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (playerController._hasKey)
        {
            cl.isTrigger = true;
            if (Player.transform.position.x > gameObject.transform.position.x + 1)
            {
                cameraBoundObject.transform.position = level2;
                cl.isTrigger = false;
                playerController._hasKey = false;
                offset += 10;
                mission.text = missionText;
            }
        }
        else
        {
            cl.isTrigger = false;
        }
    }
}
