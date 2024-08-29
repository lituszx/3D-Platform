using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClasseRigidPlayer : MonoBehaviour
{
    private Rigidbody rigid;
    private Vector3 moveDir, newDir;
    public float jumpForce, speed, gravity, knockBackForce, knockBackTime, speedRotate;
    private float knockBackCounter;
    public int totalJumps, currentJumps;
    private bool isGrounded;
    public LayerMask groundMask;
    private RaycastHit hitDir, hitMove;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    void Update()
    {
        GroundDetect();
        if (knockBackCounter <= 0)
        {
            if (isGrounded == true)
            {
                transform.Rotate(Vector3.up * speedRotate * Time.deltaTime * Input.GetAxis("Mouse X"));
                currentJumps = 0;
                moveDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
                moveDir = transform.TransformDirection(moveDir);
                moveDir *= speed;
                newDir = new Vector3(moveDir.x, rigid.velocity.y - gravity, moveDir.z);
                if (Input.GetButtonDown("Jump"))
                {
                    rigid.AddForce(Vector3.up * jumpForce);
                }
            }
            else
            {
                if (Input.GetAxis("Horizontal") >= 0.3 || Input.GetAxis("Horizontal") <= 0.3 || Input.GetAxis("Vertical") >= 0.3 || Input.GetAxis("Vertical") <= 0.3)
                {
                    moveDir = new Vector3(Input.GetAxis("Horizontal") * speed, moveDir.y, Input.GetAxis("Vertical") * speed);
                    moveDir = transform.TransformDirection(moveDir);
                }
                if (Input.GetButtonDown("Jump") && currentJumps < totalJumps)
                {
                    Vector3 getVelocity = rigid.velocity;
                    getVelocity.y = 0;
                    rigid.velocity = getVelocity;
                    rigid.AddForce(Vector3.up * jumpForce);
                    currentJumps++;
                }
                if (Physics.Raycast(transform.position, transform.forward, out hitDir, 0.55f) || Physics.Raycast(transform.position, moveDir, out hitMove, 0.7f))
                {
                    moveDir = new Vector3(0, rigid.velocity.y, 0);
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;

        }
        rigid.velocity = newDir;
    }

    public void GroundDetect()
    {
        Collider[] someDetect = Physics.OverlapSphere(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), 0.55f, groundMask);
        if (someDetect.Length > 0) isGrounded = true;
        else isGrounded = false;
    }
    public void KnockBack(Vector3 _dir)
    {
        knockBackCounter = knockBackTime;
        moveDir = _dir * knockBackForce;
        moveDir.y = knockBackForce;
    }
}

