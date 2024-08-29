using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public HPManager theHpManager;
    public GameObject particles, sparkSound;
    void Start()
    {
        theHpManager = FindObjectOfType<HPManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            theHpManager.SetSpawnPoint(transform.position);
            GameObject newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z), Quaternion.Euler(-90, 0, 0));
            Destroy(newParticles, 3);
            GameObject newSparks = Instantiate(sparkSound);
            Destroy(newSparks, 1);
        }
    }
}
