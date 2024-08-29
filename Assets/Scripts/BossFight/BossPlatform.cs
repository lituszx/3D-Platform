using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPlatform : MonoBehaviour
{
    public bool action = false;
    public GameObject switchOnSound;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            action = true;
            GameObject newSound = Instantiate(switchOnSound);
            Destroy(newSound, 2);
        }
    }
}
