using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject damageTextPrefab;
    public GameObject healthTextPrefab;

    public Canvas gameCanvas;

    private void Awake()
    {
        gameCanvas = FindObjectOfType<Canvas>();


    }

    //oneanable là kiểu gọi gọi như awake nhưng có thể goin nhiều lần,có thể gọi là vòng đời đi đến diseable
    private void OnEnable()
    {
        //khi bên damageAble call hit func thì sẽ gọi chacterEvent và characterEvent sẽ nhảy qua UImanager để gọi hàm characterTookDMG
        CharacterEvent.characterDamaged += CharacterTookDmg;
        CharacterEvent.characterHealed += CharacterHealed;
    }
    private void OnDisable()
    {
        //khi bên damageAble call hit func thì sẽ gọi chacterEvent và characterEvent sẽ nhảy qua UImanager để gọi hàm characterTookDMG
        CharacterEvent.characterDamaged -= CharacterTookDmg;
        CharacterEvent.characterHealed -= CharacterHealed;
    }

    public void CharacterTookDmg(GameObject character, int dmgReceiced) //KHI NHẬN DAME
    {
        //create  text at character hit
        Debug.Log(dmgReceiced);
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(damageTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).
            GetComponent<TMP_Text>();
        tmpText.text = dmgReceiced.ToString();
    }

    public void CharacterHealed(GameObject character, int healRestored) //KHI ĂN MÁU
    {
        //create  text at character hit
        Vector3 spawnPosition = Camera.main.WorldToScreenPoint(character.transform.position);

        TMP_Text tmpText = Instantiate(healthTextPrefab, spawnPosition, Quaternion.identity, gameCanvas.transform).
            GetComponent<TMP_Text>();
        tmpText.text = healRestored.ToString();
    }
}
