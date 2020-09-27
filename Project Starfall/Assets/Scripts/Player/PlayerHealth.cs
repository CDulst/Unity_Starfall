using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float CurrentHealthPoints = 10000;
    public float TotalHealthPoints = 100000;
    public GameObject UIHealthBar;
    public float healthbarlength = 1f;
    public GameObject ImpactBullet;
  

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("ok");
        
        switch (collision.gameObject.tag)
        {
            case "Enemy bullet":
                float DealthDamage = collision.gameObject.GetComponent<projectile>().Bulletdamage;
                Destroy(collision.gameObject);
                StartCoroutine(Hiteffects());
                LowerHealthBar(DealthDamage);
                break;
            case "Claw05__attack":
                Destroy(collision.gameObject);
                collision.gameObject.GetComponentInParent<ENEM__Claw_05>().DestroyShip();
                break;
            case "Claw13__attack":
                print("ok2");
                float DealthDamageK = collision.gameObject.GetComponent<Attackcollider>().Damage;
                StartCoroutine(Hiteffects());
                LowerHealthBar(DealthDamageK);
                break;
            case "circle":
                print("ok2");
                float DealthDamageQ = collision.gameObject.GetComponent<Explosion_bomb>().Damage;
                StartCoroutine(Hiteffects());
                LowerHealthBar(DealthDamageQ);
                break;
			case "Meteor":
			
				float DealthDamageM = collision.gameObject.GetComponent<Meteorite>().Damage;
				StartCoroutine(Hiteffects());
				LowerHealthBar(DealthDamageM);
                break;
            case "Player bullet":
                break;
            case "Rocket":
                break;
            case "NoHitmarker":
                break;
            case "Laserreceiver":
                break;
            case "Boundrie":
                break;
            default:
                StartCoroutine(Hiteffects());
                LowerHealthBar(1000f);

                break;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laserreceiver")
        {
            
			
		    float DealthDamageL = collision.gameObject.GetComponent<laserDamage>().Damage;
            GetComponent<SpriteRenderer>().color = Color.red;
            LowerHealthBar(DealthDamageL);
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Laserreceiver")
        {


         
            GetComponent<SpriteRenderer>().color = Color.white;
            

        }

    }
    private IEnumerator Hiteffects()
    {
        
        UIHealthBar.GetComponent<Animator>().SetBool("IsHit", true);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.4f);
        GetComponent<SpriteRenderer>().color = Color.white;
        UIHealthBar.GetComponent<Animator>().SetBool("IsHit", false);

    }
    public void LowerHealthBar (float DealthDamage)
    {
        if (CurrentHealthPoints > 0)
        {
            float divided = DealthDamage / CurrentHealthPoints;
            float newscalex = healthbarlength - (divided * healthbarlength);
            healthbarlength = newscalex;
            UIHealthBar.GetComponent<RectTransform>().localScale = new Vector3(healthbarlength, 1f, 1f);
            CurrentHealthPoints -= DealthDamage;

        }
    }
}
