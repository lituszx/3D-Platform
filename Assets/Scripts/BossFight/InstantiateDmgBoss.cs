using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateDmgBoss : MonoBehaviour
{
    private Rigidbody rigid;
    public GameObject finalBoss;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        rigid.isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Boss")
        {
            Destroy(gameObject);
            finalBoss.GetComponent<FinalBoss>().life--;
        }
    }
}
