using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPController : MonoBehaviour
{
	private float speed = 2.0f;
    public CharacterController controller;
    public Transform groundCheck;
    public float groundDis = 0.4f;
    public LayerMask groundMask;
	public bool isOnGround = true;
	private Rigidbody playerRb;
    Vector3 velocity;
    public float gravity = -9.08f;
    public float jumpHeight = 2f;

	

   void Start()
    {
		playerRb = GetComponent<Rigidbody>();
    }
    public void Update()
	{
        // player movement


        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(move * speed * Time.deltaTime);
        controller.Move(velocity * Time.deltaTime);

        // jump

        isOnGround = Physics.CheckSphere(groundCheck.position, groundDis, groundMask);
        if(isOnGround && velocity.y < 0)
        {
            velocity.y = -2f;
        }
      
        if (Input.GetKey(KeyCode.Space) && isOnGround){
            
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

        }

        // crouch
        if (Input.GetKey(KeyCode.C))
        {
			
        }
	}
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
			isOnGround = true;
        }
    }
}
