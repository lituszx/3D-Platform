using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : MonoBehaviour
{
    public GameObject projectile, shootPoint, platform1, platform2, dmgBoss;
    public float speed;
    public float contador = 0;
    public int life;
    public bool goPoint1;
    public GameObject player, win;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3 (player.transform.position.x, transform.position.y, transform.position.z), 5);
/*
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
        */
        contador += Time.deltaTime;
        if (contador >= 1.2f)
        {
            GameObject newProjectile = Instantiate(projectile, shootPoint.transform.position, Quaternion.identity);
            Destroy(newProjectile, 10);
            contador = 0;
        }
        if (platform1.GetComponent<BossPlatform>().action == true && platform2.GetComponent<BossPlatform>().action == true)
        {
            if (dmgBoss != null)
                dmgBoss.GetComponent<Rigidbody>().isKinematic = false;
        }
        if(life == 0)
        {
            win.SetActive(true);
        }
    }
}
