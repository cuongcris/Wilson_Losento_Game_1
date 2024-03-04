using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirection), typeof(Damageable))]  //ĐẢM BẢO LUÔN CÓ RB
public class ChuppyEnemy : MonoBehaviour
{
    public float walkAcceleration = 30f; //gia tốc
    public float maxSpeed = 3f;
    public DitectionZone attackZone;


    Rigidbody2D rb;
    Animator animator;
    TouchingDirection touchingDirection; //must add script to enemy component 
    Damageable Damageable;
    //enum state
    public enum walkAbleDirection { Left, Right };
    //walk derection
    private walkAbleDirection _walkDirection;
    private Vector2 walkDirectionVector = Vector2.right;
    public walkAbleDirection walkDirection
    {
        get
        {
            return _walkDirection;
        }
        set
        {
            if (_walkDirection != value)
            {
                //flip sprite
                gameObject.transform.localScale = new Vector2(gameObject.transform.localScale.x * -1, gameObject.transform.localScale.y);

                if (value == walkAbleDirection.Left)
                {
                    walkDirectionVector = Vector2.right;
                }
                if (value == walkAbleDirection.Right)
                {
                    walkDirectionVector = Vector2.left;
                }
            }
            _walkDirection = value;
        }
    }

    //attack zone
    private bool _hasTarget = false;
    public bool hasTarget
    {
        get
        {
            return _hasTarget;
        }
        private set
        {
            _hasTarget = value;
            animator.SetBool(AnimationString.hasTarget, value);
        }
    }

    //can move
    public float walkStopRate = 0.05f;  //speed when see the player
    public bool canMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }

    public float attackCountDown
    {
        get
        {
            return animator.GetFloat(AnimationString.attackCountDown);
        }
        private set
        {
            animator.SetFloat(AnimationString.attackCountDown, Mathf.Max(value, 0));//value never less than 0         
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        Damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        hasTarget = true;
    }


    // Update is called once per frame
    void Update()
    {
        hasTarget = attackZone.detectionCol.Count > 0;
        if (attackCountDown > 0)
            attackCountDown -= Time.deltaTime;
    }
    private void FixedUpdate()
    {
        if ((touchingDirection.isGrounded && touchingDirection.isOnWall)) //nếu đụng từong thì flip enemy
        {
            flipDirection();
        }
        if (!Damageable.lockVelocity)
        {
            if (canMove && touchingDirection.isGrounded)
            {
                //từ tốc độ đi bộ sẽ tăng tốc dần theo speed và theo hướng đang đối mặt đến khi max speed
                rb.velocity = new Vector2(
                        Mathf.Clamp(rb.velocity.x + (walkAcceleration * walkDirectionVector.x * Time.fixedDeltaTime), -maxSpeed, maxSpeed), rb.velocity.y);

            }
            else
            {
                rb.velocity = new Vector2(Mathf.Lerp(rb.velocity.x, 0, walkStopRate), rb.velocity.y); ;
            }
        }

    }

    private void flipDirection()
    {
        if (walkDirection == walkAbleDirection.Left)
        {
            walkDirection = walkAbleDirection.Right;
        }
        else if (walkDirection == walkAbleDirection.Right)
        {
            walkDirection = walkAbleDirection.Left;
        }
        else
        {
            Debug.LogError("not set direction");
        }
    }
    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    public void onCliffDetected() //khi dưới chân k phải mặt đất thì gọi hàm này
    {
        if (touchingDirection.isGrounded)
        {
            flipDirection();
        }
    }
    public void onDeath()
    {

        Destroy(gameObject, 1f);
    }
}
