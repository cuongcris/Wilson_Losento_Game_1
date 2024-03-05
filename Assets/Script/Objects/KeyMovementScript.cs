using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMovementScript : MonoBehaviour
{
    void Update()
    {
        Quaternion currentRotation = transform.rotation;
        Quaternion newYRotation = Quaternion.Euler(currentRotation.eulerAngles.x, currentRotation.eulerAngles.y + 1f, currentRotation.eulerAngles.z);
        transform.rotation = newYRotation;
    }

}
