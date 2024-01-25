﻿using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    //declare
    Vector2 moveInput;
    public float walkSpeed = 5f;
    public float runSpeed = 7f;
    public float airSpeed = 3f;
    public float jumpImpulse = 10f; // luc nhay

    //khai báo lớp touching direction để lấy thuộc tính static
    TouchingDirection touchingDirection;
    //khởi tạo
    Rigidbody2D rb;
    Animator animator;
    Damageable damageable;

    //moving and running
    [SerializeField]
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
    public bool canMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
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
        Debug.Log(moveInput);
        if (isAlive)
        {
            isMoving = moveInput != Vector2.zero;

            setFacingDirection(moveInput);
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
        }
    }

    public void onAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationString.attackTrigger);
        }
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
}