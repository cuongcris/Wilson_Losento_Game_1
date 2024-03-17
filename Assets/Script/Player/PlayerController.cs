using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //declare
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float airSpeed = 3f;
    public int DameInit = 10;
    public float jumpImpulse = 10f; // luc nhay
    public int Life = 3;
    //khai báo lớp touching direction để lấy thuộc tính static
    TouchingDirection touchingDirection;
    //khởi tạo
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;
    BoxCollider2D cl;
    [SerializeField]
    private AudioSource keySound;
    [SerializeField]
    private AudioSource HitSound;
    public bool _hasKey = false;

    [Header("......Transform......")]
    public Transform level1Spawn;
    public Transform level2Spawn;
    public Transform NextLevelPosition;
    public GameObject UIDie;

    [Header("......Text Display........")]
    public Text HealthDisplay;
    public Text SpeedDisplay;
    public Text DamageDisplay;
    public Text LifeDisplay;

    [SerializeField]
    private AudioSource jumpSound;
    [SerializeField]
    private AudioSource attackSound;
    [SerializeField]
    private AudioSource itemSound;
    //moving and running
    [SerializeField]
    private AudioSource moveSound;
    private bool _isMoving = false;
    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationString.isMoving, value);
        }
    }
    [SerializeField]
    private bool _isRunning = false;
    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        private set
        {
            _isRunning = value;
            animator.SetBool(AnimationString.isRunning, value);
        }
    }
    [SerializeField]
    private bool _isAttacking = false;
    public bool isAttacking
    {
        get
        {
            return _isAttacking;
        }
        private set
        {
            _isAttacking = value;
            animator.SetBool(AnimationString.isAttacking, value);
        }
    }
    //check right or left để đổi sprite trái phải
    private bool _isFacingRight = true;
    public bool isFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //đảo ngược hướng hiện tại
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    //set speed tuỳ theo walk , run , jump
    public float currentMoveSpeed
    {
        get
        {
            if (canMove)
            {
                if (isMoving && !touchingDirection.isOnWall) //không đụng tường
                {

                    if (touchingDirection.isGrounded) //đang ở mặt đất
                    {
                        if (isRunning)
                        {
                            return runSpeed;
                        }
                        else
                        {
                            return walkSpeed;
                        }
                    }
                    else //nếu đang ở trên không
                    {
                        return airSpeed;
                    }

                }
                else return 0; //idle speed = 0
            }
            else
            {
                //lock move
                return 0;
            }


        }
    }
    //

    [SerializeField]
    private bool _canMove=true;
    public bool canMove
    {
        get { return _canMove;/*animator.GetBool(AnimationString.canMove);*/ }
        set
        {
            _canMove = value;
            animator.SetBool(AnimationString.canMove,value);
        }
    }
    [SerializeField]
    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationString.isAlive);
        }
    }



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirection = GetComponent<TouchingDirection>();
        damageable = GetComponent<Damageable>();
        cl = GetComponent<BoxCollider2D>();
    }
    private void Update()
    {
        if (damageable.Health <= 0) {
            HealthDisplay.text = 0 + "";
        }
        else HealthDisplay.text = damageable.Health.ToString();
        
        if (currentMoveSpeed == 0)
        {
            SpeedDisplay.text = 5+"";
        }
        else  SpeedDisplay.text = currentMoveSpeed.ToString();

        DamageDisplay.text = DameInit.ToString();
        LifeDisplay.text = " X"+ Life.ToString();
    }

    private void FixedUpdate() //khi update các tác động vật lý thì dùng fixedUpdate()
    {
        if (!damageable.lockVelocity)         //nếu k bị tấn công thì có thể di chuyển
            rb.velocity = new Vector2(moveInput.x * currentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationString.yVelocity, rb.velocity.y); //set velocity.y để biết đang nhảy lên hay rớt xuống 
    }
   
    private void setFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !isFacingRight)
        {
            ///face right
            isFacingRight = true;
        }
        else if (moveInput.x < 0 && isFacingRight)
        {
            //face left
            isFacingRight = false;
        }
    }

    public void onMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (isAlive)
        {
            isMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);

            // Kiểm tra nếu nhân vật đang di chuyển và âm thanh di chuyển chưa được phát
            if (isMoving && !moveSound.isPlaying)
            {
                moveSound.Play(); // Phát âm thanh di chuyển
            }
            // Nếu nhân vật dừng lại hoặc không có đầu vào di chuyển, dừng phát âm thanh
            else if (!isMoving || moveInput == Vector2.zero)
            {
                moveSound.Stop(); // Dừng phát âm thanh
            }
        }
        else
        {
            isMoving = false;
        }
    }


    public void onRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;

        }
        else if (context.canceled)
        {
            isRunning = false;

        }
    }


  
    public void onJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirection.isGrounded && canMove)
        {
            animator.SetTrigger(AnimationString.jumpTrigger);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);

            // Kiểm tra nếu nhân vật đang nhảy và âm thanh nhảy chưa được phát
            if (!jumpSound.isPlaying)
            {
                jumpSound.Play(); // Phát âm thanh nhảy
            }
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
            StartCoroutine(PlayAttackSoundAfterAnimation());
        }
    }

    private IEnumerator PlayAttackSoundAfterAnimation()
    {
        // Đợi cho animation attack kết thúc
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("Wilson_attack"))
        {
            yield return null;
        }
        // Đợi một khoảng thời gian nhỏ để đảm bảo animation attack kết thúc hoàn toàn
        yield return new WaitForSeconds(0.1f);
        // Phát âm thanh tấn công
        attackSound.Play();
    }
    public void onFireAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.fireAttackTrigger);
        }
    }

    public void onHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }
    public void OnDeath()
    {
        Life--;
        canMove = false;
        if (Life <= 0)
        {
            StartCoroutine(PlayerDieCo(2f));
        }
        else
        {
            StartCoroutine(RespawnAfterDelay(2f)); 
        }
    }
    IEnumerator PlayerDieCo(float delay)
    {
        yield return new WaitForSeconds(delay);
        UIDie.SetActive(true);
        gameObject.SetActive(false);
    }
    //hoi sinh sau 2s
    IEnumerator RespawnAfterDelay(float delay)
    {
        cl.isTrigger = true;
        yield return new WaitForSeconds(delay);
        if(transform.position.x <= NextLevelPosition.position.x)
        {
            transform.position = new Vector2(level1Spawn.position.x, 7);
        }else if (transform.position.x > NextLevelPosition.position.x)
        {
            transform.position = new Vector2(level2Spawn.position.x, 7);
        }
        rb.velocity = Vector2.zero; // Reset velocity
        damageable.ResetHealth(); // Reset health
        canMove = true;
        cl.isTrigger=false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Key"))
        {
            _hasKey = true;
            keySound.Play();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.CompareTag("DameItem"))
        {
            DameInit += 10;
            itemSound.Play();
            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            damageable.Health -= 50;
            HitSound.Play();
            Destroy(collision.gameObject);
        }
    }
}
