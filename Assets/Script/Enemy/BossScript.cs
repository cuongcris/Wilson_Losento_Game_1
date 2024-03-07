using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Transform GunPosition;
    public GameObject bulletPrefab;
    public float bulletSpeed = 5f;
    public float fireRate = 1f;
    public float detectionRadius = 5f;

    private bool playerInRange = false;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(FireBullets());
    }

    void Update()
    {
        // Kiểm tra xem player có trong vùng phát hiện không
        if (Physics2D.OverlapCircle(transform.position, detectionRadius, LayerMask.GetMask("Player")))
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }
    }
    public void OnDeath()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.8f);

    }

    

    IEnumerator FireBullets()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f); // Mỗi 1 giây

            if (playerInRange)
            {
                // Tính toán hướng bắn ngẫu nhiên
                Vector3 fireDirection = new Vector3(Random.Range(-1f, 0), Random.Range(-1f, 0), 0f).normalized;

                // Tạo đạn từ prefab
                GameObject bullet = Instantiate(bulletPrefab, GunPosition.position, Quaternion.identity);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

                if (rb != null)
                {
                    // Thiết lập tốc độ cho đạn
                    rb.velocity = fireDirection * bulletSpeed;
                }
            }
        }
    }

 
}
