using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screenboundrie : MonoBehaviour
{
    public List<bool> boundries;
    private void Start()
    {
        PlayerAttack player = FindObjectOfType<PlayerAttack>();
       
    }
    // Start is called before the first frame update
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (boundries[0])
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.x < 0)
                {

                    collision.gameObject.transform.position = new Vector2(transform.position.x + 1.5f, collision.transform.position.y);
                }

            }
        }
        else if (boundries[1])
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.y > 0)
                {

                    collision.gameObject.transform.position = new Vector2(collision.transform.position.x, 5.2f);
                }

            }
        }
        else if (boundries[2])
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.x > 0)
                {

                    collision.gameObject.transform.position = new Vector2(10f, collision.transform.position.y);
                }

            }
        }
        else if (boundries[3])
        {
            if (collision.gameObject.tag == "Player")
            {
                if (collision.GetComponent<Rigidbody2D>().velocity.y < 0)
                {

                    collision.gameObject.transform.position = new Vector2(collision.transform.position.x , -5f);
                }

            }
        }
    }


}
