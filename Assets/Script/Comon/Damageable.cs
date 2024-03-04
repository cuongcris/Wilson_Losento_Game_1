using Assets.Script;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//dùng để chứa máu, dame và death , có thể dùng cho cả player and enemy
public class Damageable : MonoBehaviour
{
    //init
    Animator animator;
    public UnityEvent<int, Vector2> damageableHit;
    public UnityEvent damageableDeath;

    //level up health
    public string Type;
    public int ValuePlus = 5; 

    [SerializeField]
    private int _maxHealth = 100 ;
    public  int MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    [SerializeField]
    private int _health =100;
    public  int Health
    {
        get
        {
            return _health;
        }
        set
        {
            _health = value;
            if (_health <= 0)
            {
                isAlive = false;

            }

        }
    }
    [SerializeField]
    private bool _isAlive = true;
    private bool isInvincible = false;


    private float timeSinceHit = 0;
    public float invincibleTimer = 0.25f;

    public bool isAlive
    {
        get { return _isAlive; }
        private set
        {
            _isAlive = value;
            animator.SetBool(AnimationString.isAlive, value);
            if (value == false)
            {
                damageableDeath.Invoke();
            }
        }
    }
    public bool lockVelocity
    {
        get
        {
            return animator.GetBool(AnimationString.lockVelocity);
        }
        set
        {
            animator.SetBool(AnimationString.lockVelocity, value);
        }
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        //lockVelocity = false;
        if (isInvincible)
        {
            if (timeSinceHit > invincibleTimer)
            {
                //thời gian reset đòn đánh lớn hơn thời gian bất tử
                //remove invincibility
                isInvincible = false;
                timeSinceHit = 0; //reset 
            }
            timeSinceHit += Time.deltaTime;
        }
    }

    //return took dame or not
    public bool Hit(int damage, Vector2 knockBack)
    {
        if (isAlive && !isInvincible) //nếu còn sống và không bất tử 
        {
            Health -= damage;
            isInvincible = true; //bất tử là kiểu đánh 1 cái xong đợi 1 xí mới đánh phát nữa đc
            //thông báo cho các thành phần khác là đã nhận đòn đánh 
            animator.SetTrigger(AnimationString.hitTrigger);

            damageableHit?.Invoke(damage, knockBack); //dấu ? để nếu damageableHit null thì k gọi invokle, và ngược lại

            //bị đánh là gọi hàm event này để hiện text dmg
           CharacterEvent.characterDamaged.Invoke(gameObject, damage);
            return true;
        }
        //unable to hit
        return false;
    }


    public bool Heal(int HealRestored)
    {
        if (isAlive && Health < MaxHealth)
        {
            int maxHeal = Mathf.Max(MaxHealth - Health, 0);
            int actualHeal = Mathf.Min(maxHeal, HealRestored);
            Health += actualHeal;

            //nhặt heart thì hiện heal text
            CharacterEvent.characterHealed.Invoke(gameObject, actualHeal);
            return true;
        }
        return false;
    }
    public void ResetHealth()
    {
        isAlive = true;
        lockVelocity = false;
        
        _health = _maxHealth;
    }
}
