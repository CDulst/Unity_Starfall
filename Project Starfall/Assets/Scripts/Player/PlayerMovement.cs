using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    float horizontalmove;
    float verticalmove;
    public Joystick joystick;
    public Rigidbody2D myrigidbody;
    public bool idlefinished = true;
    public bool moving;
    public bool idleactivation;
    public bool joystickmoved;
    public GameObject startingpoint;
    public bool abletomove = true;
    public bool Bounceup;
    public bool Bouncedown;
    public bool Triggerentered;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    // Update is called once per frame
    void Update()
    {

        velocitycheck();
        if (Bounceup)
        {
            Velocityup();
        }
        if (Bouncedown)
        {
            Velocitydown();
        }
        if (abletomove)
        {
            Move();
        }
        if (idlefinished)
        {
           StartCoroutine(IdleMovement());
        }
    }

    private void Move()
    {
        if (joystick.Horizontal > 0 || joystick.Horizontal < 0)
        {
            moving = true;
         
            joystickmoved = true;
        }
        else
        {
            

            if (joystickmoved)
            {
           
                transform.position = Vector2.MoveTowards(transform.position, startingpoint.transform.position, 10f * Time.deltaTime);
                if (transform.position == startingpoint.transform.position)
                {
                    joystickmoved = false;
                    moving = false;
                }
            }
          
        }

            idleactivation = true;
            horizontalmove = joystick.Horizontal * speed;
            verticalmove = joystick.Vertical * speed;
            Vector2 playervelocity = new Vector2(horizontalmove * Time.deltaTime, verticalmove * Time.deltaTime);
            myrigidbody.velocity = playervelocity;
          




    }
    private IEnumerator IdleMovement()
    {
       if (!moving)
        {


            idlefinished = false;
            for (float i = 1f; i <= 13f;)
            {
                myrigidbody.velocity = new Vector2(0, (transform.position.y + i) * Time.deltaTime);
                i += 0.1f;
                if (moving)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
            for (float i = 13f; i >= 0f;)
            {
                myrigidbody.velocity = new Vector2(0, (transform.position.y + i) * Time.deltaTime);
                i -= 0.1f;
                if (moving)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);

            }
            for (float i = 1f; i <= 14f;)
            {
                myrigidbody.velocity = new Vector2(0, (transform.position.y - i) * Time.deltaTime);
                i += 0.1f;
                if (moving)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
            for (float i = 14f; i >= 0f;)
            {
                myrigidbody.velocity = new Vector2(0, (transform.position.y - i) * Time.deltaTime);
                i -= 0.1f;
                if (moving)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);

            }
            idlefinished = true;

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!Triggerentered)
        {
            if (collision.gameObject.tag == "Wall")
            {
                Triggerentered = true;
                StartCoroutine(Bounceback());
            }
        }
       
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            Triggerentered = false;
            StartCoroutine(Bounceback());
        }
    }

    private void velocitycheck()
    {
        if (myrigidbody.velocity.y > 0)
        {
 
        }
        if (myrigidbody.velocity.y < 0)
        {
   
        }
    }

    private void Velocityup()
    {
        myrigidbody.velocity = new Vector2(0, transform.position.y + 400f * Time.deltaTime);
        Debug.Log("jood");
    
    }
    private void Velocitydown()
    {
        myrigidbody.velocity = new Vector2(0, transform.position.y - 400f * Time.deltaTime);
        Debug.Log("nigger");
        
    }
    private IEnumerator Bounceback()
    {
        if (myrigidbody.velocity.y > 0)
        {
            Bouncedown = true;
            abletomove = false;
            yield return new WaitForSeconds(0.4f);
            abletomove = true;
            Bouncedown = false;
            
            
        }
        if (myrigidbody.velocity.y < 0)
        {


            Bounceup = true;
            abletomove = false;
            yield return new WaitForSeconds(0.4f);
            abletomove = true;
            Bounceup = false;

        }

    }


}
