using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTriiger : MonoBehaviour
{
    public GameObject cameraBoundObject;
    public Transform level1;
    public Transform level2;

    BoxCollider2D cl;
    public GameObject Player;
    private void Start()
    {
        cl = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (Player.transform.position.x> gameObject.transform.position.x+1)
        {
            cameraBoundObject.transform.position = new Vector2((level2.position.x + 5), 0);
            cl.isTrigger = false;
        }
    }
   
}
