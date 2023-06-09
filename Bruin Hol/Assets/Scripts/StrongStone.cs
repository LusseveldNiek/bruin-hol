using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StrongStone : MonoBehaviour
{
    public GameObject inventory;
    public GameObject bomb;
    public GameObject bombParticle;
    public Transform bombPosition;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "pickaxe")
        {
            bool hasCoal = inventory.GetComponent<Inventory>().hasCoal;
            bool hasSulfur = inventory.GetComponent<Inventory>().hasSulfur;
            if (hasSulfur && hasCoal)
            {
                inventory.GetComponent<Inventory>().objectsInInventory.Clear();
                for(int i = 0; i < inventory.GetComponent<Inventory>().inventory.Length; i ++)
                {
                    inventory.GetComponent<Inventory>().inventory[i].texture = null;
                    Color currentColor = inventory.GetComponent<Inventory>().inventory[i].color;

                    // Wijzig de alpha-waarde
                    currentColor.a = 0;

                    // Pas de kleur aan
                    inventory.GetComponent<Inventory>().inventory[i].color = currentColor;
                }
                inventory.GetComponent<Inventory>().hasCoal = false;
                inventory.GetComponent<Inventory>().hasSulfur = false;
                GameObject prefabBomb = Instantiate(bomb, bombPosition.position, Quaternion.identity);
                Invoke("InstantiateObject", 2.5f);
                Destroy(gameObject, 2.5f);
                Destroy(prefabBomb, 2.5f);
            }
        }
    }

    public void InstantiateObject()
    {
        GameObject particlePrefab = Instantiate(bombParticle, bombPosition.position, Quaternion.identity);
        Destroy(particlePrefab, 0.7f);
        particlePrefab.GetComponent<AudioSource>().Play();
    }
}
