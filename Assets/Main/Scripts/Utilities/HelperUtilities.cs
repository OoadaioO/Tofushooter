using UnityEngine;

public class HelperUtilities {

    public static float GetAngleFromVector(Vector2 vector) {
        float radians = Mathf.Atan2(vector.y, vector.x);
        float degree = radians * Mathf.Rad2Deg;

        return degree;
    }

    public static Vector3 GetDirectionVectorFromAngle(float angle) {
        Vector3 directionVector = new(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0);
        return directionVector;
    }


    public static AimDirection GetAimDirection4(float angleDegrees) {
        AimDirection aimDirection;

        if (angleDegrees >= -67 && angleDegrees < 67) {
            aimDirection = AimDirection.Right;
        } else if (angleDegrees >= 67 && angleDegrees < 112) {
            aimDirection = AimDirection.Up;
        } else if (angleDegrees >= -112 && angleDegrees < -67) {
            aimDirection = AimDirection.Down;
        } else {
            aimDirection = AimDirection.Left;
        }

        return aimDirection;
    }

    public static AimDirection GetAimDirection3(float angleDegrees) {
        AimDirection aimDirection;

        if (angleDegrees >= -90 && angleDegrees < 67) {
            aimDirection = AimDirection.Right;
        } else if (angleDegrees >= 67 && angleDegrees < 112) {
            aimDirection = AimDirection.Up;
        } else {
            aimDirection = AimDirection.Left;
        }

        return aimDirection;
    }


    public static AimDirection GetAimDirection8(float angleDegrees) {
        AimDirection aimDirection;

        if (angleDegrees > 22f && angleDegrees <= 67f) {
            // 右上 22-67
            aimDirection = AimDirection.UpRight;
        } else if (angleDegrees > 67f && angleDegrees <= 112f) {
            // 上 67 - 112
            aimDirection = AimDirection.Up;
        } else if (angleDegrees > 112f && angleDegrees <= 158f) {
            // 左上 112-158
            aimDirection = AimDirection.UpLeft;
        } else if ((angleDegrees > 158f && angleDegrees <= 180f) || (angleDegrees > -180f && angleDegrees <= -158f)) {
            // 左 158- 180 -180 ~ -158
            aimDirection = AimDirection.Left;
        } else if (angleDegrees > -158f && angleDegrees <= -112f) {
            // 左下  -158 ~ -112
            aimDirection = AimDirection.DownLeft;
        } else if (angleDegrees > -112f && angleDegrees <= -67) {
            // 下 -112 ~ -67
            aimDirection = AimDirection.Down;
        } else if (angleDegrees >= -67 && angleDegrees < -22) {
            // 右下 -67 ~ -22
            aimDirection = AimDirection.DownRight;
        } else {
            // 右
            aimDirection = AimDirection.Right;
        }

        return aimDirection;
    }

}
