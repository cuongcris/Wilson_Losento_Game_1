using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public  Text ScoreText;
    public static int score;
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        ScoreText.text ="Score : " +score.ToString();
    }
}
