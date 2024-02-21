using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    //pixel per second
    public Vector3 moveSpeed = new Vector3(0, 600, 0); //mặc định chiều y sẽ đi lên với tốc độ 600
    public float timeToFade = 1f;

    RectTransform textTranform;
    TextMeshProUGUI textMeshProUGUI;

    private float timeElapsed = 0f; //thoi gian troi qua
    private Color startColor;

    // Start is called before the first frame update
    void Awake()
    {
        textTranform = GetComponent<RectTransform>();
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        startColor = textMeshProUGUI.color;
    }
    void Update()
    {
        textTranform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;
        if (timeElapsed < timeToFade)
        {
            float fadeAlpha = startColor.a * (1 - (timeElapsed / timeToFade));
            textMeshProUGUI.color = new Color(startColor.r, startColor.g, startColor.b, fadeAlpha);
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
