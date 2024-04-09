using System.Collections;
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




    /// convert the llinear volume scalle to decibel
    public static float LinerToDeciblels(int linear) {
        float linearScaleRange = 20f;
        return Mathf.Log10((float)linear / linearScaleRange) * 20f;
    }


    public static bool ValidateCheckEmptyString(Object thisObject, string fieldName, string stringToCheck) {
        if (stringToCheck == "") {
            Debug.Log(fieldName + " is empty and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }

    public static bool ValidateCheckNullValue(Object thisObject, string fieldName, UnityEngine.Object objectToCheck) {
        if (objectToCheck == null) {
            Debug.Log(fieldName + " is null and must contain a value in object " + thisObject.name.ToString());
            return true;
        }
        return false;
    }


    public static bool ValidateCheckEnumerableValues(Object thisObject, string fieldName, IEnumerable enumerableObjectToCheck) {
        bool error = false;
        int count = 0;

        if (enumerableObjectToCheck == null) {
            Debug.Log(fieldName + " is null in object " + thisObject.name.ToString());
            return true;
        }

        foreach (var item in enumerableObjectToCheck) {
            if (item == null) {
                Debug.Log(fieldName + " has null values in object " + thisObject.name.ToString());
                error = true;
            } else {
                count++;
            }
        }

        if (count == 0) {
            Debug.Log(fieldName + " has no values in object " + thisObject.name.ToString());
            error = true;
        }
        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, int valueToCheck, bool isZeroAllowed) {
        bool error = false;
        if (isZeroAllowed) {
            if (valueToCheck < 0) {
                Debug.Log(fieldName + " must contain a positive value or zero in object " + thisObject.name.ToString());
                error = true;
            }
        } else {
            if (valueToCheck <= 0) {
                Debug.Log(fieldName + " must contain a positive value in object " + thisObject.name.ToString());
                error = true;
            }
        }
        return error;
    }

    public static bool ValidateCheckPositiveValue(Object thisObject, string fieldName, float valueToCheck, bool isZeroAllowed) {
        bool error = false;

        if (isZeroAllowed) {
            if (valueToCheck < 0) {
                Debug.Log(fieldName + " must contain a positive value or zero in object" + thisObject.name.ToString());
                error = true;
            }

        } else {
            if (valueToCheck <= 0) {
                Debug.Log(fieldName + " must contain a positive value in object " + thisObject.name.ToString());
                error = true;
            }
        }

        return error;
    }

    public static bool ValidateCheckPositiveRange(Object thisObject, string fieldNameMinimum, float valueToCheckMinimum,
      string fieldNameMaximum, float valueToCheckMaximum, bool isZeroAllowed) {
        bool error = false;
        if (valueToCheckMinimum > valueToCheckMaximum) {
            Debug.Log(fieldNameMinimum + " must be less than or equal to " + fieldNameMinimum + " in object " + thisObject.name.ToString());
            error = true;
        }

        if (ValidateCheckPositiveValue(thisObject, fieldNameMinimum, valueToCheckMinimum, isZeroAllowed)) error = true;

        if (ValidateCheckPositiveValue(thisObject, fieldNameMaximum, valueToCheckMaximum, isZeroAllowed)) error = true;

        return error;

    }

    public static bool ValidateCheckPositiveRange(Object thisObject, string fieldNameMinimum, int valueToCheckMinimum,
  string fieldNameMaximum, int valueToCheckMaximum, bool isZeroAllowed) {
        bool error = false;
        if (valueToCheckMinimum > valueToCheckMaximum) {
            Debug.Log(fieldNameMinimum + " must be less than or equal to " + fieldNameMinimum + " in object " + thisObject.name.ToString());
            error = true;
        }

        if (ValidateCheckPositiveValue(thisObject, fieldNameMinimum, valueToCheckMinimum, isZeroAllowed)) error = true;

        if (ValidateCheckPositiveValue(thisObject, fieldNameMaximum, valueToCheckMaximum, isZeroAllowed)) error = true;

        return error;

    }

 

}
