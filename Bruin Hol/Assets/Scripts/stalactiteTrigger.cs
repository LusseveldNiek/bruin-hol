using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stalactiteTrigger : MonoBehaviour
{
    public Rigidbody stalactite;
    public Animator animator;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            stalactite.isKinematic = false;
            stalactite.useGravity = true;
            animator.SetBool("Waiting", false);
            animator.enabled = false;
        }
    }
}
