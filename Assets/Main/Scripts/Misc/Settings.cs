using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings {



    #region Animator Parameters
    public static int isMoving = Animator.StringToHash("isMoving");
    public static int isIdle = Animator.StringToHash("isIdle");

    public static int aimLeft = Animator.StringToHash("aimLeft");
    public static int aimRight = Animator.StringToHash("aimRight");
    public static int aimUp = Animator.StringToHash("aimUp");
    public static int aimUpRight = Animator.StringToHash("aimUpRight");
    public static int aimUpLeft = Animator.StringToHash("aimUpLeft");
    public static int aimDown = Animator.StringToHash("aimDown");
    public static int aimDownLeft = Animator.StringToHash("aimDownLeft");
    public static int aimDownRight = Animator.StringToHash("aimDownRight");

    public static int aimDirection = Animator.StringToHash("aimDirection");



    #endregion

    // if the target distance is less than this then the aim angle will be uses(calculated from player),
    // else the weapon aim angle will  be used(calculated from the weapon shoot position)
    public const float useAimAngleDistance = 3.5f;

    public const int targetFrameRateToSpreadPathfindingOver = 60; // 帧率，用于对敌人移动控制负载均衡

    public const float playerMoveDistanceToRebuildPath = 1f;
    public const float enemyPathRebuildCooldown = 2f;
}