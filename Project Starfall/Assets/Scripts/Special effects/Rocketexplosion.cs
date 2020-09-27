using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocketexplosion : MonoBehaviour
{
    public GameObject blast;
    public float blastdamage = 3000f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Rocket")
        {
            Destroy();
        }
    }
    public void Destroy()
    {
        GameObject it = Instantiate(blast, transform.position, Quaternion.identity).gameObject;
        it.GetComponent<Explosion_bomb>().Damage = blastdamage;
        Destroy(transform.parent.gameObject);

    }
    
}

