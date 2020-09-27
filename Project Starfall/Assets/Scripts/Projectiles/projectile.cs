using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    public float Projectilespeed = 20f;
    private Rigidbody2D Myrigidbody;
    public bool enemy;
    public float Bulletdamage = 100f;
    // Start is called before the first frame update
    void Start()
    {
        Myrigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy)
        {
            Myrigidbody.velocity = new Vector2((transform.position.x + Projectilespeed) * Time.deltaTime, 0);
        }
        else
        {
            Myrigidbody.velocity = new Vector2((transform.position.x - Projectilespeed) * Time.deltaTime, 0);
        }
        
    }
}
