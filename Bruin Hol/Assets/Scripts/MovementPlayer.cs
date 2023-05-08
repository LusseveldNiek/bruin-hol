using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    public float speedPlayer;
    public float jumpForce;
    public float feetRadius;
    public Rigidbody playerRigidbody;

    public bool isGrounded;
    public Transform feet;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(Input.GetAxis("Horizontal") * speedPlayer, 0, 0);

        if(Input.GetKey("w") && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }
}
