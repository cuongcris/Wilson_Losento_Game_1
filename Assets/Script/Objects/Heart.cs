using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public int healRestore = 20; //lượng máu của mỗi trái tim 

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Damageable damageable = collision.GetComponent<Damageable>();
            if (damageable)
            {
                bool wasHeal = damageable.Heal(healRestore); //kiểm tra đã nhặt heart hay chưa
                if (wasHeal)
                    Destroy(gameObject);
            }
        }
    }
}
