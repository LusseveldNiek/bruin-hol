using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tornado : MonoBehaviour
{
    public float timeInTornado;
    private float tornadoTime;
    public float forceUp;
    private bool inTornado;
    public GameObject player;
    public float tornadoCooldown;
    private float tornadoCooldownTime;
    private bool tornadoCoolingDown;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && tornadoCoolingDown == false)
        {
            player = other.gameObject;
            player.GetComponent<MovementPlayer>().enabled = false;
            inTornado = true;
        }
    }

    private void Update()
    {
        if(inTornado)
        {
            player.transform.position = transform.position + new Vector3(0, 1, 0);
            tornadoTime += Time.deltaTime;
            player.GetComponent<MovementPlayer>().animator.SetBool("isJumping", true);
        }

        if (tornadoTime > timeInTornado)
        {
            tornadoTime = 0;
            inTornado = false;
            player.GetComponent<MovementPlayer>().enabled = true;
            tornadoCoolingDown = true;
            player.GetComponent<MovementPlayer>().animator.SetBool("isJumping", false);
        }
    }

    private void FixedUpdate()
    {
        if (tornadoCoolingDown)
        {
            if(tornadoCooldownTime < 0.3f)
            {
                player.GetComponent<Rigidbody>().AddForce(Vector3.up * forceUp * Time.deltaTime);
            }

            tornadoCooldownTime += Time.deltaTime;
            if (tornadoCooldownTime > tornadoCooldown)
            {
                tornadoCooldownTime = 0;
                tornadoCoolingDown = false;
            }
        }
    }
}