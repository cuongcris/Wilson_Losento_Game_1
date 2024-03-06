using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //doi == 1, chubby == 2, rocket ==3
    public int Enemytype;

    private int damage;
    public int Damage
    {
        get
        {
            return damage;
        }
        set
        {
            damage = value;
        }
    }

    private float speed;
    public float Speed
    {
        get
        {
            return speed;
        }
        set
        {
            speed = value;
        }
    }
    void Awake()
    {
        if (Enemytype == 1)
        {
            damage = 30;
            speed = 2;
        }
        else if (Enemytype == 2)
        {
            damage = 25;
            speed = 3;
        }
        else if (Enemytype == 3)
        {
            damage = 20;
            speed = 2;
        }
        else
        {
            damage = 15;
            speed = 2;
        }
    }


}
