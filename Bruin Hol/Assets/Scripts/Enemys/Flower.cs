using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower : MonoBehaviour
{
    public GameObject player;
    public GameObject bullet;
    private float bulletTimer;
    public float bulletTimerLimit;
    public int bulletLifetime;
    public float flowerRange;
    public Transform spawnPosition;
    public float upShooting;
    public float bulletSpeed;
    public Animator animator;
    public float animationLimit;
    public float animationTime;
    private bool playingAnimation;
    public AudioSource flower;
    public GameObject particle;
    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) < flowerRange)
        {
            bulletTimer += Time.deltaTime;
            if (bulletTimer > bulletTimerLimit)
            {
                GameObject instantiatedBullet = Instantiate(bullet, spawnPosition.position, Quaternion.identity);
                float distance = Vector3.Distance(transform.position, player.transform.position);
                instantiatedBullet.GetComponent<FlowerBullet>().speed = distance * bulletSpeed;
                Vector3 direction = ((player.transform.position - transform.position) - new Vector3(0, upShooting, 0)).normalized;
                FlowerBullet bulletScript = instantiatedBullet.GetComponent<FlowerBullet>();
                bulletScript.ShootInDirection(direction);
                Destroy(instantiatedBullet, bulletLifetime);
                bulletTimer = 0;
                animator.SetBool("schooting", true);
                playingAnimation = true;
                flower.Play();
            }
        }

        if(playingAnimation)
        {
            animationTime += Time.deltaTime;
            if(animationTime > animationLimit)
            {
                playingAnimation = false;
                animator.SetBool("schooting", false);
                animationTime = 0;
            }
        }

        

        if(player.transform.position.x > transform.position.x)
        {
            transform.localRotation = Quaternion.Euler(0, 90, 0);
        }

        else
        {
            transform.localRotation = Quaternion.Euler(0, -90, 0);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickaxe")
        {
            Destroy(gameObject);
            GameObject particlePrefab = Instantiate(particle, transform.position, Quaternion.identity);
            Destroy(particlePrefab, 1);
        }
    }
}
