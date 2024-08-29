using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float jumpForce, moveSpeed, gravityScale, knockBackForce, knockBackTime;
    private float knockBackCounter;
    public CharacterController controller;
    public Vector3 moveDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (knockBackCounter <= 0)
        {
            //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal"));
            moveDirection = moveDirection.normalized * moveSpeed;
            moveDirection.y = yStore;
            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                if (Input.GetButtonDown("Jump"))
                {
                    moveDirection.y = jumpForce;
                }
            }
        }
        else
        {
            knockBackCounter -= Time.deltaTime;

        }
        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);
    }
    public void KnockBack(Vector3 _dir)
    {
        knockBackCounter = knockBackTime;
        moveDirection = _dir * knockBackForce;
        moveDirection.y = knockBackForce;
    }
}
