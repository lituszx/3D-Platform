using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControl : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject door, pointToGo, switchOnSound;
    public bool active = false;
    // Update is called once per frame
    void Update()
    {
        if(active)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, pointToGo.transform.position, 0.5f * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            active = true;
            GameObject newSound = Instantiate(switchOnSound);
            Destroy(newSound, 2);
        }
    }
}
