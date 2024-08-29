using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public bool colPlayer = false;
    public GameObject player;
    void Update()
    {
        transform.Translate(Vector3.back * 10 * Time.deltaTime);

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
        if (other.tag == "Door")
        {
            gameObject.SetActive(false);
        }
    }
}
