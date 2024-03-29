﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class RocketEnemy : MonoBehaviour
{
    public Rigidbody2D myRb;
    [Header("Target")]
    public Transform target;
    public float chaseRadius; //area find the player
    public float attackRadius; // area where enemy can attack
    //  public Transform homePosition;
    public Animator anim;
    public float moveSpeed;

    public int score;
    public NextLevelTriiger levelTriiger;
    public Enemy enemyAttribute;

    public GameObject[] itemPrefabs; // Mảng chứa các prefab của các vật phẩm
    public float itemDropForce = 2f;
    public SpriteRenderer sprite;
    
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        anim = GetComponent<Animator>();
        myRb = GetComponent<Rigidbody2D>();
        anim.SetBool("wakeUp", true);
        sprite = GetComponent<SpriteRenderer>();
        // Bắt đầu Coroutine để đếm giây
    }
    private void Update()
    {
        StartCoroutine(DeathCountdown());
        if (anim.GetBool("isAlive") == false)
        {
            // Vô hiệu hóa các thành phần của nhân vật
            sprite.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            myRb.simulated = false;
            anim.enabled = false;
        }

        //nếu qua màn thì offset(hệ số theo level) sẽ thay đổi --> dẫn đến chỉ số của quái sẽ thay đổi theo
        if (levelTriiger != null && enemyAttribute != null)
        {
            moveSpeed = enemyAttribute.Speed * (levelTriiger.offset == 0 ? 1f : (float)levelTriiger.offset * 14 / 100);
        }
        else
        {
            moveSpeed = 2f;
        }
    }
    IEnumerator DeathCountdown()
    {
        yield return new WaitForSeconds(4f);
    }
    void FixedUpdate()
    {
        checkDistance();
    }
    public void changeAnim(Vector2 direction)
    {
        if (direction.x < 0)
        {
            sprite.flipX = false;
        }
        else if (direction.x > 0)
        {
            sprite.flipX = true;
        }
    }
    //virtual như kiểu để biết cái này cho lớp khác kế overing lại
    public virtual void checkDistance()
    {
        // Tính khoảng cách giữa nhân vật và mục tiêu
        float distanceToTarget = Vector3.Distance(transform.position, target.position);

        // Kiểm tra nếu khoảng cách nhỏ hơn hoặc bằng bán kính theo dõi nhưng lớn hơn bán kính tấn công
        if (distanceToTarget <= chaseRadius && distanceToTarget > attackRadius)
        {
            // Kích hoạt trạng thái wakeUp
            anim.SetBool("wakeUp", true);

            // Tính vector hướng từ nhân vật đến mục tiêu
            Vector3 directionToTarget = (target.position - transform.position).normalized;

            // Chỉ lấy thành phần x để di chuyển theo chiều ngang
            Vector3 horizontalDirection = new Vector3(directionToTarget.x, 0f, 0f).normalized;

            // Tính vị trí mới dựa trên vector hướng
            Vector3 temp = transform.position + horizontalDirection * moveSpeed * Time.deltaTime;

            // Di chuyển nhân vật
            myRb.MovePosition(temp);

            // Thay đổi animation dựa trên hướng di chuyển
            changeAnim(horizontalDirection);
        }
        else if (distanceToTarget > chaseRadius)
        {
            // Nếu khoảng cách lớn hơn bán kính theo dõi, vô hiệu hóa trạng thái wakeUp
            anim.SetBool("wakeUp", false);
        }
    }
    public void OnDeath()
    {

        int randomIndex = Random.Range(0, 3);
        if (randomIndex < itemPrefabs.Length)
        {
            GameObject randomItemPrefab = itemPrefabs[randomIndex];

            // Tạo prefab được chọn và đặt vị trí là vị trí hiện tại của enemy
            GameObject spawnedItem = Instantiate(randomItemPrefab, transform.position, Quaternion.identity);

            // Áp dụng lực văng cho vật phẩm
            Rigidbody2D rb = spawnedItem.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Tính toán hướng văng ngẫu nhiên
                Vector2 randomDirection = Random.insideUnitCircle.normalized;
                // Áp dụng lực văng
                rb.AddForce(randomDirection * itemDropForce, ForceMode2D.Impulse);
            }
        }
        Score.score += score;
        Destroy(gameObject, 0.5f);
    }
}
