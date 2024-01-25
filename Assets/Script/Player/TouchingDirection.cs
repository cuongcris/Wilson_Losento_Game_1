using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchingDirection : MonoBehaviour
{
    public ContactFilter2D contactFilter;
    //khoảng cách phát hiện va chạm
    public float groundDistance = 0.05f;
    public float wallDistance = 0.1f;
    public float ceilingDistance = 0.05f;

    //khởi tạo
    BoxCollider2D touchingColl;
    Animator anim;


    //ground
    RaycastHit2D[] groundHits = new RaycastHit2D[5];
    [SerializeField]
    private bool _isGrounded;
    public bool isGrounded
    {
        get { return _isGrounded; }
        private set
        {
            _isGrounded = value;
            anim.SetBool(AnimationString.isGrounded, value);
        }
    }

    //wall
    RaycastHit2D[] wallHits = new RaycastHit2D[1];
    private Vector2 wallDirection => gameObject.transform.localScale.x > 0 ? Vector2.right : Vector2.left;

    [SerializeField]
    private bool _isOnWall;
    public bool isOnWall
    {
        get
        {
            return _isOnWall;
        }
        private set
        {
            _isOnWall = value;
            anim.SetBool(AnimationString.isOnWall, value);
        }
    }
    //celling
    RaycastHit2D[] ceilingHits = new RaycastHit2D[5];
    [SerializeField]
    private bool _isOnCeiling;
    public bool isOnCeiling
    {
        get
        {
            return _isOnCeiling;
        }
        private set
        {
            _isOnCeiling = value;
            anim.SetBool(AnimationString.isOnCeiling, value);
        }
    }

    private void Awake()
    {
        touchingColl = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        //Cast(hướng ,chỉ ra thằng mình đạng chạm vào, lưu trữ objects vừa tìm  ,khoảng cách để chạm )
        isGrounded = touchingColl.Cast(Vector2.down, contactFilter, groundHits, groundDistance) > 0;
        isOnWall = touchingColl.Cast(wallDirection, contactFilter, wallHits, wallDistance) > 0;
        isOnCeiling = touchingColl.Cast(Vector2.up, contactFilter, ceilingHits, ceilingDistance) > 0;
    }
}
