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
    private bool showPopup=false;
    private bool playerInRange = false;

    public Damageable damageable;
    public GameObject healBarChildPrefab;
    private GameObject childObject;

    public GameObject PopUpMisition;
         [SerializeField]
    private AudioSource bulletSound;

    Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(FireBullets());
        showPopup = false;
        childObject = Instantiate(healBarChildPrefab, gameObject.transform);
        childObject.transform.localPosition = new Vector3(2.95f, 2.75f, 0f);
    }

    void Update()
    {
        //Update thanh mau
        CalculatorhearthBar();

        // Kiểm tra xem player có trong vùng phát hiện không
        if (Physics2D.OverlapCircle(transform.position, detectionRadius, LayerMask.GetMask("Player")))
        {
            if (showPopup==false)
            {
                showPopup = true;
                PopUpMisition.SetActive(true);
                Time.timeScale = 0f;
            }
            playerInRange = true;
        }
        else
        {
            playerInRange = false;


        }
        if (showPopup == true && playerInRange == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PopUpMisition.SetActive(false);
                Time.timeScale = 1f;
                
            }
        }
    }
    public void OnDeath()
    {
        animator.SetTrigger("Die");
        Destroy(gameObject, 1.8f);

    }

    public float curHealthPercent = 1;
    public void CalculatorhearthBar()
    {
        //Lay do rong hien tai
        Transform transform = childObject.transform;
        Vector3 currentScale = transform.localScale;

        //tinh % mau hien tai
        curHealthPercent = (float) damageable.Health / 500;

        //thay doi do rong theo % hien tai
        currentScale.x = (float) (-4)  * curHealthPercent;

        //cap nhap lai
        childObject.transform.localScale = currentScale;
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
                        bulletSound.Play();
                }
            }
        }
    }

 
}
