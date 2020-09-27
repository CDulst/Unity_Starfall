using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEM__Claw_05 : MonoBehaviour
{
    PlayerMovement player;
    GameObject engine;
    public float Enemyhealth = 500;
    public float healthbarlength = 1f;
    public GameObject impactbullet;
    float bulletdamage;
    public GameObject CircleExplosion;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
        Transform[] allchildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allchildren)
        {
            if (child.gameObject.tag == "Engine")
            {
                engine = child.gameObject;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Healthcheck();
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Transform>().localScale = new Vector3(1f, 1, 1);
            engine.GetComponent<Transform>().localScale = new Vector3(0.05460253f, 0.07042941f, 0.04383034f);
        }
        else
        {
            GetComponent<Transform>().localScale = new Vector3(-1f, 1, 1);
            engine.GetComponent<Transform>().localScale = new Vector3(-0.05460253f, 0.07042941f, 0.04383034f);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.7f*Time.deltaTime);
    }

    private void Movement()
    {
        
        if (player.transform.position.x < transform.position.x)
        {
            GetComponent<Transform>().localScale = new Vector3(1f, 1, 1);
        }
        else
        {
            GetComponent<Transform>().localScale = new Vector3(-1f, 1, 1);
        }
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, 0.7f * Time.deltaTime);
    }
    public void DestroyShip()
    {
        Instantiate(CircleExplosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player bullet"|| collision.gameObject.tag == "Claw13__attack" || collision.gameObject.tag == "Meteor" || collision.gameObject.tag == "circle")
        {
            if (collision.gameObject.tag == "Player bullet")
            {
                bulletdamage = collision.GetComponent<projectile>().Bulletdamage;
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.tag == "circle")
            {
                bulletdamage = collision.GetComponent<Explosion_bomb>().Damage;
            }
			else if (collision.gameObject.tag == "circle")
			{
				bulletdamage = collision.GetComponent<Explosion_bomb>().Damage;
			}
			else
            {
                bulletdamage = collision.GetComponent<Attackcollider>().Damage;
            }
            
            StartCoroutine(Hiteffects());
            GameObject bar = this.gameObject.transform.GetChild(5).GetChild(1).gameObject;
            LowerEnemyHealth(bar, bulletdamage);
        }
        
    }
    private void LowerEnemyHealth(GameObject bar, float bulletdamage)
    {
        if (Enemyhealth > 0)
        {
            float divided = bulletdamage / Enemyhealth;
            float newscalex = healthbarlength - (divided * healthbarlength);
            healthbarlength = newscalex;
            bar.GetComponent<Transform>().localScale = new Vector3(healthbarlength, 0.4f, 0);

            Enemyhealth -= bulletdamage;
        }

    }
    private IEnumerator Hiteffects()
    {
        Instantiate(impactbullet, transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.4f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }
    private void Healthcheck()
    {
        if (Enemyhealth <= 0)
        {
            
            DestroyShip();
        }
    }
}
