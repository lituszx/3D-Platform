using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleControl : MonoBehaviour
{
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (FindObjectOfType<GameManager>().GetComponent<HPManager>().activeCapsule == true)
            {
                FindObjectOfType<GameManager>().GetComponent<HPManager>().activeCapsule = false;
                anim.SetBool("Go", false);
            }

        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            anim.SetBool("Go", true);
        }
    }
}
