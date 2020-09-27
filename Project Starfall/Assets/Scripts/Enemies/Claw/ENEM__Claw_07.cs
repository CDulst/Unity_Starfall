using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEM__Claw_07 : MonoBehaviour
{
    public float Enemyhealth = 1000f;
    public float attackspeed = 2f;
    public GameObject bullet;
    public bool attacking;
    public string tofollowroute;
    public List<GameObject> Routepoints;
    public int i;
    public bool reachedposition;
    public float movementspeed = 5f;
    public float healthbarlength = 1f;
    public GameObject impactbullet;
    public GameObject explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attack());
        gameObject.SetActive(true);
        Getroute();
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Routepoints[i].transform.position, movementspeed * Time.deltaTime);
        if (transform.position == Routepoints[i].transform.position)
        {
            reachedposition = true;
        }
        DeathCheck();
    }

    private IEnumerator Attack()
    {
        while (attacking)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(attackspeed);
        }
        
    }

    private void Getroute()
    {
        GameObject route = GameObject.FindGameObjectWithTag(tofollowroute);
        Transform[] allchildren = route.GetComponentsInChildren<Transform>();
        foreach (Transform child in allchildren)
        {
            if(child.gameObject.tag == "Route__Child")
            {
                Routepoints.Add(child.gameObject);
            }
            
        }
        StartCoroutine(Startmoving());
    }
    private IEnumerator Startmoving()
    {
        while(true){
            for (i = 0; i < Routepoints.Count; i++)
            {
                reachedposition = false;
                while (!reachedposition)
                {
                    yield return null;
                }
                
                
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

		switch (collision.gameObject.tag)
		{
			case "Player bullet":
				float bulletdamage = collision.GetComponent<projectile>().Bulletdamage;
				Destroy(collision.gameObject);
				StartCoroutine(Hiteffects());
				GameObject bar = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;

				LowerEnemyHealth(bar, bulletdamage);
				break;
			case "Claw13__attack":
				bulletdamage = collision.GetComponent<Attackcollider>().Damage;
				StartCoroutine(Hiteffects());
				bar = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
				print(bar);
				LowerEnemyHealth(bar, bulletdamage);
				break;
			case "circle":
				bulletdamage = collision.GetComponent<Explosion_bomb>().Damage;
				StartCoroutine(Hiteffects());
				bar = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
				print(bar);
				LowerEnemyHealth(bar, bulletdamage);
				break;
			case "Meteor":
				bulletdamage = collision.GetComponent<Meteorite>().Damage;
				StartCoroutine(Hiteffects());
				bar = this.gameObject.transform.GetChild(0).GetChild(1).gameObject;
				print(bar);
				LowerEnemyHealth(bar, bulletdamage);
				break;

		}
	}
    private IEnumerator Hiteffects()
    {
        Instantiate(impactbullet, transform.position, Quaternion.identity);
        GetComponent<Animator>().SetBool("IsHit", true);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.4f);
        GetComponent<SpriteRenderer>().color = Color.white;
        GetComponent<Animator>().SetBool("IsHit", false);
    }
    private void DeathCheck()
    {
        if (Enemyhealth <= 0)
        {
            Instantiate(explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void LowerEnemyHealth(GameObject bar, float bulletdamage)
    {
        if (Enemyhealth > 0)
        {
            float divided = bulletdamage / Enemyhealth;
            float newscalex = healthbarlength - (divided*healthbarlength);
            healthbarlength = newscalex;
            bar.GetComponent<Transform>().localScale = new Vector3(healthbarlength, 0.4f, 0);

            Enemyhealth -= bulletdamage;
        }
        
    }
}
