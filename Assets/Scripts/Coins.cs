using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    public int value;
    public GameObject sound;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FindObjectOfType<GameManager>().AddCoin(value);
            GameObject newSound = Instantiate(sound);
            Destroy(newSound, 2);
            Destroy(gameObject);
        }
    }
}
