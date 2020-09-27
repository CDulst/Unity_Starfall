using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public float speed;
    public GameObject Destination;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Destination.transform.position, speed * Time.deltaTime);
        if (transform.position == Destination.transform.position)
        {
            Destroy(gameObject);
        }
    }
}
