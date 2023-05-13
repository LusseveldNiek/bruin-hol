using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementPlayer : MonoBehaviour
{
    [Header("Player")]
    public float speedPlayer;
    public float jumpForce; 
    public float fallSpeed;
    public Rigidbody playerRigidbody;

    public GameObject sprintParticle;
    private bool isNotSpawning;

    [Header("Jumping")]
    public bool isGrounded;
    private RaycastHit groundHit;
    public float height; //hoogte tussen speler en grond

    [Header("Sprinting")]
    public float sprintSpeed; //sprint snelheid
    public float fastSprintSpeed; //fast sprint snelheid
    private float sprintCounter; // speler wordt langzaam sneller
    private float beginPlayerSpeed; // reset snelheid speler
    private float sprintMultiplier; //voor andere sprint fase verdubbelaar
    private float sprintTimer; //timer hoelang sprint fase
    public float maxSprintSpeed;

    [Header("WallJumping")]
    public float wallJumpForce;
    private RaycastHit wallRight;
    private RaycastHit wallLeft;
    public float slideSpeed;

    public bool isOnRightWall;
    public bool isOnLeftWall;

    public bool rightWallJumping;
    public bool leftWallJumping;

    [Header("Attacking")]
    public int damage;

    

    void Start()
    {
        beginPlayerSpeed = speedPlayer;
    }

    void Update()
    {
        if(isOnRightWall == false && isOnLeftWall == false)
        {
            //horizontal movement
            transform.Translate(Input.GetAxis("Horizontal") * speedPlayer * Time.deltaTime, 0, 0);
        }
        
        IncreaseMass();
        Sprinting();
        WallJumping();
        Attacking();
    }

    void FixedUpdate()
    {
        //jumping
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
            playerRigidbody.AddForce(Vector3.up * jumpForce);
        }

        //jumping from right wall
        if (Input.GetKey(KeyCode.Space) && isOnRightWall && rightWallJumping == false)
        {
            rightWallJumping = true;
            playerRigidbody.AddForce(-Vector3.right * wallJumpForce);
            playerRigidbody.AddForce(Vector3.up * wallJumpForce);
        }

        //jumping from left wall
        if (Input.GetKey(KeyCode.Space) && isOnLeftWall && leftWallJumping == false)
        {
            leftWallJumping = true;
            playerRigidbody.AddForce(Vector3.right * wallJumpForce);
            playerRigidbody.AddForce(Vector3.up * wallJumpForce);
        }

        //als de speler de grond aanraakt springt de speler niet meer van een muur
        if(isGrounded)
        {
            leftWallJumping = false;
            rightWallJumping = false;
        }
    }

    void IncreaseMass()
    {
        //maakt springen korter
        height = groundHit.distance;
        if (isGrounded == false)
        {
            Physics.Raycast(transform.position, -transform.up, out groundHit, 100);          
            Physics.gravity = new Vector3(0, -groundHit.distance * fallSpeed, 0);
        }

        else
        {
            Physics.gravity = new Vector3(0, -10, 0);
        }
    }

    void WallJumping()
    {
        //raycast left checking for wall
        if(leftWallJumping == false)
        {
            Physics.Raycast(transform.position, -transform.right, out wallLeft, 0.5f);
            if(rightWallJumping)
            {
                wallRight = new RaycastHit();
            }
        }
        
        //raycast right checking for wall
        if(rightWallJumping == false)
        {
            Physics.Raycast(transform.position, transform.right, out wallRight, 0.5f);
            if(leftWallJumping)
            {
                wallLeft = new RaycastHit();
            }
        }

        //player hitting wall
        if(wallLeft.transform != null)
        {
            if(wallLeft.transform.gameObject.tag == "wall" && leftWallJumping == false)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -slideSpeed, playerRigidbody.velocity.z);
                isOnLeftWall = true;
            }
        }
        else if (wallRight.transform != null)
        {
            if(wallRight.transform.gameObject.tag == "wall" && rightWallJumping == false)
            {
                playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x, -slideSpeed, playerRigidbody.velocity.z);
                isOnRightWall = true;
            }
        }

        else
        {
            isOnRightWall = false;
            isOnLeftWall = false;
        }

        // ik check wanneer de wall jumping klaar is als de speler de grond aan heeft geraakt, maar als de speler een andere muur raakt, is het springen ook klaar
        if(isOnLeftWall)
        {
            rightWallJumping = false;
        }

        if(isOnRightWall)
        {
            leftWallJumping = false;
        }
    }

    void Sprinting()
    {
        //sprinten
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speedPlayer += sprintCounter * sprintCounter * sprintMultiplier;
            sprintCounter += sprintSpeed * Time.deltaTime;

            sprintTimer += Time.deltaTime;
            if(sprintTimer > 0.3f)
            {
                sprintMultiplier = fastSprintSpeed;
                //print("sprintingFast");


                //sprint particle
                //GameObject particle = Instantiate(sprintParticle, transform.position, Quaternion.identity);
                //Destroy(particle, 1);
            }
        }

        else
        {
            speedPlayer = beginPlayerSpeed;
            sprintCounter = 0;
            //print("reset");
            sprintTimer = 0;
            sprintMultiplier = 1;
        }

        speedPlayer = Mathf.Clamp(speedPlayer, 0, maxSprintSpeed);
    }

    void Attacking()
    {
        if(Input.GetKey(KeyCode.E))
        {

        }
    }

    //is on ground
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
