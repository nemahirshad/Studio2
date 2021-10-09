using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPController : MonoBehaviour
{
	private float speed = 2.0f;
    public CharacterController controller;
	public float jumpForce = 5.0f;
	public bool isOnGround = true;
	private Rigidbody playerRb;
	

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

        controller.Move(move * speed * Time.deltaTime);


        if (Input.GetKey(KeyCode.Space) && isOnGround) //jump
        {
			playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			isOnGround = false;
        }
        if (Input.GetKey(KeyCode.C)) // crouch
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
