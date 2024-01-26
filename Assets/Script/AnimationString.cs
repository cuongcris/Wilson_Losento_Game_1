using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationString : MonoBehaviour
{
    //boolean
    internal static string isMoving = "isMoving";
    internal static string isRunning = "isRunning";
    internal static string isGrounded = "isGrounded";
    internal static string isOnWall = "isOnWall";
    internal static string isOnCeiling = "isOnCeiling";
    internal static string isAlive = "isAlive";
    internal static string isHit = "isHit";

    internal static string canMove = "canMove";
    internal static string yVelocity = "yVelocity";
    internal static string hasTarget = "hasTarget";
    internal static string lockVelocity = "lockVelocity";
    internal static string isAttacking = "isAttacking";

    //trigger
    internal static string attackTrigger = "attack";
    internal static string jumpTrigger = "jump";
    internal static string hitTrigger = "hit";
    internal static string attackCountDown = "attackCountdown";
    internal static string fireAttackTrigger = "fireAttack";
}
