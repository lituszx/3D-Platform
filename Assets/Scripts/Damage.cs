using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damage = 1;
    void Start()
    {
        
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Vector3 _hitDir = other.transform.position - transform.position;
            _hitDir = _hitDir.normalized;
            FindObjectOfType<HPManager>().GetDamage(damage, _hitDir);
        }
    }
}
