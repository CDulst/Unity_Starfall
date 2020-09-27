using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heavybomb : MonoBehaviour
{
    public float speedx = -1f;
    public float speedy = 0f;
    public float spinningSpeed = 10f;
    public float blastdamage = 7000f;
    public GameObject blast;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speedx * Time.deltaTime, transform.position.y + speedy * Time.deltaTime);
        GetComponent<Transform>().Rotate(0, 0, spinningSpeed * Time.deltaTime);
    }
    public void Destroy()
    {
        GameObject it = Instantiate(blast, transform.position, Quaternion.identity).gameObject;
        it.GetComponent<Explosion_bomb>().Damage = blastdamage;
        Destroy(gameObject);
    }
}
