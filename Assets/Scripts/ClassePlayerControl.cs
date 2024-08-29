using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassePlayerControl : MonoBehaviour
{
    public int damage = 1;
    public float jumpForce, speed, gravity, knockBackForce, knockBackTime, speedRotate, radiusAttack, rateAttack, timeAttack;
    private float knockBackCounter, walkTimer;
    public CharacterController control;
    public Vector3 moveDir;
    private int currentJumps, totalJumps;
    public int lineDistance;
    private RaycastHit hitInfo, footInfo;
    private bool catching = false, activeAttack;
    private GameObject actualItem, currentTarget;
    public GameObject coin;
    private List<Collider> enemysOverlap = new List<Collider>();
    public Animator anim;
    public GameObject jumpSound, secondJump, LavaSoundDead, particles, sparkSound;
    public List<GameObject> walkSounds = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        control = GetComponent<CharacterController>();
        totalJumps = 1;
        walkTimer = 0;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Horizontal") == 0 && Input.GetAxisRaw("Vertical") == 0)
        {
            anim.SetBool("Andar", false);
        }
        else
        {
            walkTimer += Time.deltaTime;
            if(walkTimer > 0.3f)
            {
                GameObject newWalkSound = Instantiate(walkSounds[Random.Range(0, walkSounds.Count)]);
                Destroy(newWalkSound, 1);
                walkTimer = 0;
            }
            anim.SetBool("Andar", true);
        }
        if (knockBackCounter <= 0)
        {
            if (control.isGrounded)
            {
                anim.SetInteger("Salto", 0);
                currentJumps = 0;
                moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                //moveDir = transform.TransformDirection(moveDir);
                moveDir *= speed;
                if (Input.GetButtonDown("Jump"))
                {
                    GameObject newSound = Instantiate(jumpSound);
                    Destroy(newSound, 2);
                    moveDir.y = jumpForce;
                    anim.SetInteger("Salto", 1);
                }
                AttackSystem();
                if (Input.GetButtonDown("Fire2"))
                {
                    if (catching == false)
                    {
                        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, lineDistance))
                        {
                            if (hitInfo.collider.tag == "Interactable")
                            {
                                anim.SetBool("Amarra", true);
                                actualItem = hitInfo.collider.gameObject;
                                actualItem.layer = 2;
                                actualItem.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
                                actualItem.GetComponent<Rigidbody>().isKinematic = true;
                                actualItem.transform.SetParent(transform);
                                catching = true;
                            }
                        }
                    }
                    else
                    {
                        actualItem.layer = 0;
                        actualItem.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) + transform.forward;
                        actualItem.GetComponent<Rigidbody>().isKinematic = false;
                        actualItem.transform.SetParent(null);
                        anim.SetBool("Amarra", false);
                        catching = false;
                        actualItem = null;
                    }
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") >= 0.3 || Input.GetAxis("Horizontal") <= 0.3 || Input.GetAxis("Vertical") >= 0.3 || Input.GetAxis("Vertical") <= 0.3)
                {
                    moveDir = new Vector3(Input.GetAxisRaw("Horizontal") * speed, moveDir.y, Input.GetAxisRaw("Vertical") * speed);
                    //moveDir = transform.TransformDirection(moveDir);
                }
                if (Input.GetButtonDown("Jump") && currentJumps < totalJumps)
                {
                    moveDir.y = jumpForce;
                    currentJumps++;
                    GameObject newSound = Instantiate(secondJump);
                    Destroy(newSound, 0.35f);
                    anim.SetInteger("Salto", 2);
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;

        }


        Vector3 newDir = moveDir;
        newDir.y = 0;
        if (newDir != Vector3.zero)
        {
            if (moveDir.x != 0 || moveDir.z != 0)
            {
                transform.rotation = Quaternion.LookRotation(newDir);
            }
        }

        //transform.Rotate(Vector3.up * speedRotate * Time.deltaTime * Input.GetAxis("Mouse X"));
        moveDir.y -= gravity * Time.deltaTime;
        control.Move(moveDir * Time.deltaTime);
    }
    public void KnockBack(Vector3 _dir)
    {
        knockBackCounter = knockBackTime;
        moveDir = _dir * knockBackForce;
        moveDir.y = knockBackForce;
    }
    public void AttackSystem()
    {   
        if(Physics.Raycast(transform.position, Vector3.down, out footInfo, 1.5f))
        {
            if(footInfo.collider.gameObject.tag == "Enemy")
            {
                Destroy(footInfo.collider.gameObject);
                GameObject newCoin = Instantiate(coin, footInfo.collider.gameObject.transform.position, Quaternion.identity);
                moveDir.y = jumpForce;
            }
        }
        if (activeAttack == true)
        {
            enemysOverlap = new List<Collider>(Physics.OverlapSphere(transform.position, radiusAttack));
            for (int i = 0; i < enemysOverlap.Count; i++)
            {
                if(enemysOverlap[i].tag == "Enemy")
                {
                    Destroy(enemysOverlap[i].gameObject);
                    GameObject newCoin = Instantiate(coin, enemysOverlap[i].gameObject.transform.position, Quaternion.identity);
                }
            }
        }
        else
        {
            rateAttack += Time.deltaTime;
            if (rateAttack >= 1f && Input.GetButtonDown("Fire1"))
            {
                anim.SetBool("Golpe", true);
                rateAttack = 0;
                activeAttack = true;
                Invoke("StopAttack", 1f);
            }
        }
    }
    private void StopAttack()
    {
        anim.SetBool("Golpe", false);
        activeAttack = false;
        enemysOverlap.Clear();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Vector3 _hitDir = other.transform.position - transform.position;
            _hitDir = _hitDir.normalized;
            FindObjectOfType<HPManager>().GetDamage(damage, _hitDir);
            GameObject newSparks = Instantiate(sparkSound);
            Destroy(newSparks, 1);
        }
        if(other.tag == "Lava")
        {
            GameObject newSound = Instantiate(LavaSoundDead);
            Destroy(newSound, 2);
            GameObject newParticles = Instantiate(particles, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(-90, 0, 0));
            Destroy(newParticles, 3);
            GameObject newSparks = Instantiate(sparkSound);
            Destroy(newSparks, 1);
        }
    }
}
