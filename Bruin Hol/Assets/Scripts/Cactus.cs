using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public GameObject player;
    public Transform cactusUp;
    private bool cactusActivated;
    private float beginHeight;
    public float upSpeed;
    public float upHeight;
    public float moveSpeed;
    private bool cactusIsWalking;
    private RaycastHit hit;
   
    void Start()
    {
        beginHeight = transform.position.y;
    }

    void Update()
    {
        if(Vector3.Distance(cactusUp.position, player.transform.position) < 2)
        {
            cactusActivated = true;
        }

        if(cactusActivated)
        {
            if(transform.position.y < beginHeight + upHeight)
            {
                transform.Translate(Vector3.up * upSpeed * Time.deltaTime);
            }

            else if(transform.position.y > beginHeight + upHeight)
            {
                
                cactusActivated = false;
                cactusIsWalking = true;
    
            }
        }

        if(cactusIsWalking)
        {
            cactusActivated = false;
            if (Vector3.Distance(cactusUp.position, player.transform.position) > 4)
            {
   
                if(transform.position.y > beginHeight)
                {
                    transform.Translate(-Vector3.up * upSpeed * Time.deltaTime);
                }

                else if(transform.position.y < beginHeight)
                {
                    cactusIsWalking = false;
                }
            }

            else
            {
                transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
            }

            Physics.Raycast(-Vector3.up, transform.position + new Vector3(0, 0.1f, 0), out hit, 0.1f);
            if(hit.transform.gameObject == null)
            {
                transform.Translate(-Vector3.up * upSpeed * Time.deltaTime);
            }

        }
    }
}
