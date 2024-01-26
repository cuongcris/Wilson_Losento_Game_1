﻿using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatEnemy : MonoBehaviour
{
    public DitectionZone biteDetectionZone;
    Damageable damageable;

    public float batSpeed = 2f;
    public float wayPointReachedDistance = 0.1f;   //khoảng cách va chạm với waypoint
    public List<Transform> wayPoints;
    public Collider2D deadCollder;

    Transform nextWayPoint;
    int wayPointNumber = 0;

    Animator animator;
    Rigidbody2D rb;

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
    public bool canMove
    {
        get { return animator.GetBool(AnimationString.canMove); }
    }
    void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        damageable = GetComponent<Damageable>();
    }
    private void Start()
    {
        nextWayPoint = wayPoints[wayPointNumber];
    }


    void Update()
    {
        hasTarget = biteDetectionZone.detectionCol.Count > 0;
    }

    private void FixedUpdate()
    {
        if (damageable.isAlive)
        {
            if (canMove)
            {
                Flight();
            }
            else
            {
                rb.velocity = Vector3.zero;
            }
        }
        else
        {

        }
    }

    private void Flight()
    {
        //fly to next point 
        Vector2 directionToWayPoint = (nextWayPoint.position - transform.position).normalized;
        //check if we have reched waypoint already
        float distance = Vector2.Distance(nextWayPoint.position, transform.position);
        rb.velocity = directionToWayPoint * batSpeed;
        
        //swicht way point
        if (distance <= wayPointReachedDistance)
        {
            updateLocalScale();
            //next way point
            wayPointNumber++;
            if (wayPointNumber >= wayPoints.Count)
            {
                //reset way point 
                wayPointNumber = 0;
            }
            nextWayPoint = wayPoints[wayPointNumber];
        }
    }

    private void updateLocalScale()
    {
        Vector3 localScale = transform.localScale;
        if (transform.localScale.x > 0)
        {
            //flip right
            if (rb.velocity.x < 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
        else
        {
            //flip left
            if (rb.velocity.x > 0)
            {
                transform.localScale = new Vector3(-1 * localScale.x, localScale.y, localScale.z);
            }
        }
    }
    public void onDeath()
    {

        rb.gravityScale = 2;
        rb.velocity = new Vector2(0, rb.velocity.y);
        deadCollder.enabled = true;
    }
}