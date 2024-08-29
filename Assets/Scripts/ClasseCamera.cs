using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasseCamera : MonoBehaviour
{
    private RaycastHit hitCamera;
    public GameObject lookAtPoint, posCam;
    public float height, distance;
    private Vector3 velocity = Vector3.zero;
    public int maxL, maxR;
    public LayerMask layer;
    void Start()
    {

    }
    void LateUpdate()
    {
        posCam.transform.position = new Vector3(transform.position.x, transform.position.y + height, transform.position.z - distance);
        //limites de la camera, canviar el numero int por el que se quiera
        if(transform.position.x > maxR)
        {
            posCam.transform.position = new Vector3(maxR, transform.position.y + height, transform.position.z - distance);
        }
        if (transform.position.x < maxL)
        {
            posCam.transform.position = new Vector3(maxL, transform.position.y + height, transform.position.z - distance);
        }

        if (Physics.Linecast(lookAtPoint.transform.position, posCam.transform.position, out hitCamera, layer))
        {
            print(hitCamera.collider.name);
            Camera.main.transform.position = hitCamera.point;
        }
        else
        {
            Camera.main.transform.position = posCam.transform.position;
        }
        Camera.main.transform.LookAt(lookAtPoint.transform);
    }
}
