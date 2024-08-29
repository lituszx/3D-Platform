using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    public GameObject point1, point2;
    public bool goPoint1;
    public float speed;
    void Update()
    {
        if (goPoint1 == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, point1.transform.position, speed * Time.deltaTime);
            if (transform.position == point1.transform.position)
            {
                goPoint1 = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, point2.transform.position, speed * Time.deltaTime);
            if (transform.position == point2.transform.position)
            {
                goPoint1 = true;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.transform.SetParent(null);
        }
    }
}
