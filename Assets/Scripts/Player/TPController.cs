using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPController : MonoBehaviour
{
	public float speed = 2.0f;
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;
	public bool isOnGround = true;
    public Vector3 velocity;
    public float gravity = -9.08f;
    public float jumpHeight = 2f;
    public Animator anim;

    bool jump = false;

    public void Update()
	{
        //Stop player input until animation is over
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("FinalAttackAnimation"))
        {
            return;
        }

        //Attack
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attack");
            //Deal damage based on trigger
        }
        else
        {
            // player movement

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            anim.SetFloat("X", x);
            anim.SetFloat("Y", z);

            Vector3 move = transform.right * x + transform.forward * z;
            velocity.y += gravity * Time.deltaTime;

            anim.SetFloat("Velocity", move.magnitude);

            controller.Move(move * speed * Time.deltaTime);
            controller.Move(velocity * Time.deltaTime);

            // jump

            isOnGround = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
            if (isOnGround && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
            {
                anim.SetTrigger("Jump");
            }

            if(jump)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }

            // crouch
            if (Input.GetKey(KeyCode.C))
            {
                anim.SetBool("Crouch", true);
            }
            else
            {
                anim.SetBool("Crouch", false);
            }

        }
    }

    public void Jump()
    {
        jump = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
			isOnGround = true;
        }
    }
}
