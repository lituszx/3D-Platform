using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target, pivot;
    public Vector3 offset;
    public bool useOffsetValues, invertY;
    public float rotateSpeed, maxViewAngle, minViewAngle;
    void Start()
    {
        if (!useOffsetValues)
            offset = target.position - transform.position ;
        pivot.position = target.position;
        pivot.parent = target;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void LateUpdate()
    {

        //Movimiento Camera y aplica al target
        //float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        //target.Rotate(0, horizontal, 0);
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;

        if (invertY)
        {
            pivot.Rotate(vertical, 0, 0);
        }
        else
        {
            pivot.Rotate(-vertical, 0, 0);
        }

        //Limites
        //if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        //{
        //    pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        //}
        //if (pivot.rotation.eulerAngles.x > 180f && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        //{
        //    pivot.rotation = Quaternion.Euler(360 + minViewAngle, 0, 0);
        //}

        //Mueve la camera segun la rotacion del target y el offset
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;
        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);

        transform.position = target.position - (rotation * offset);
        //transform.position = target.position - offset;
        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y - 0.5f, transform.position.z);
        }
        transform.LookAt(target);
    }
}
