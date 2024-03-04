using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMenuScroling : MonoBehaviour
{
    public float SpeedRawImage = 2f;

     void Update()
    {
        transform.position += new Vector3 (SpeedRawImage * Time.deltaTime, 0);
        if(transform.position.x >= 10)
        {
            transform.position = new Vector3(-7, transform.position.y);
        }
    }
}
