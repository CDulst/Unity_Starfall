using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteorite : MonoBehaviour
{
    public float speedx = -2f;
    public float speedy = 0f;
    public float spinningSpeed = 100f;
    public float Damage = 2000f;
    public bool big = true;
    public bool medium;
    public bool small;
    public GameObject Big;
    public GameObject Medium;
    public GameObject Small;
    public GameObject impactbullet;
    public float meteorhealth;
    public float starthealth = 500f;
    public float healthbarlength = 1f;
    float bulletdamage;
    GameObject bar;
    // Start is called before the first frame update
    void Start()
    {
        meteorhealth = starthealth;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + speedx * Time.deltaTime, transform.position.y + speedy*Time.deltaTime);
        Transform[] allchildren = GetComponentsInChildren<Transform>();
        allchildren[1].Rotate(0, 0, spinningSpeed * Time.deltaTime);
        if (meteorhealth <= 0)
        {
            Meteoritesbreaks();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) 
    {

        switch (collision.gameObject.tag)
        {
            case "Player bullet":
                bulletdamage = collision.GetComponent<projectile>().Bulletdamage;
                Destroy(collision.gameObject);
                StartCoroutine(Hiteffects());
                
                bar = this.gameObject.transform.GetChild(1).GetChild(1).gameObject;

                LowerMeteorHealth(bar, bulletdamage);
                break;
            case "Claw13__attack":
                Debug.Log("wtfdude");
                float bulletdamagek = collision.GetComponent<Attackcollider>().Damage;
                StartCoroutine(Hiteffects());
                GameObject bark = this.gameObject.transform.GetChild(1).GetChild(1).gameObject;
                print(bark);
                LowerMeteorHealth(bark, bulletdamagek);
                break;
            case "circle":
                Debug.Log("wtfdude");
                float bulletdamagec = collision.GetComponent<Explosion_bomb>().Damage;
                StartCoroutine(Hiteffects());
                GameObject barc = this.gameObject.transform.GetChild(1).GetChild(1).gameObject;
                print(bar);
                LowerMeteorHealth(barc, bulletdamagec);
                break;
           
            default:
                Debug.Log("shit");

                break;

        }
    }
    private IEnumerator Hiteffects()
    {
        Instantiate(impactbullet, transform.position, Quaternion.identity);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.4f);
        this.gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().color = Color.white;

    }
    private void LowerMeteorHealth(GameObject bar, float bulletdamage)
    {
        if (meteorhealth > 0)
        {
            float divided = bulletdamage / meteorhealth;
            float newscalex = healthbarlength - (divided * healthbarlength);
            healthbarlength = newscalex;
            bar.GetComponent<Transform>().localScale = new Vector3(healthbarlength, 0.4f, 0);

            meteorhealth -= bulletdamage;
        }

    }
    private void Meteoritesbreaks()
    {
 
            if (big)
            {
            GameObject it = Instantiate(Medium, transform.position, Quaternion.identity).gameObject;
            Meteorite variables = it.GetComponent<Meteorite>();
            variables.speedx = -8f;
            variables.speedy = 0f;
            variables.spinningSpeed = 100f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;
           
            it = Instantiate(Medium, transform.position, Quaternion.identity).gameObject;
            variables = it.GetComponent<Meteorite>();
            variables.speedx = 4f;
            variables.speedy = -2f;
            variables.spinningSpeed = 100f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;

            it = Instantiate(Medium, transform.position, Quaternion.identity).gameObject;
            variables = it.GetComponent<Meteorite>();
            variables.speedx = -2f;
            variables.speedy = 4f;
            variables.spinningSpeed = 100f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;

            it = Instantiate(Medium, transform.position, Quaternion.identity).gameObject;
            variables = it.GetComponent<Meteorite>();
            variables.speedx = -4f;
            variables.speedy = -4f;
            variables.spinningSpeed = 100f;
            variables.starthealth = starthealth/2;
            variables.Damage = Damage / 2;


        }
            if (medium)
        {
            GameObject it = Instantiate(Small, transform.position, Quaternion.identity).gameObject;
            Meteorite variables = it.GetComponent<Meteorite>();
            variables.speedx = -16f;
            variables.speedy = 0f;
            variables.spinningSpeed = 200f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;

            it = Instantiate(Small, transform.position, Quaternion.identity).gameObject;
            variables = it.GetComponent<Meteorite>();
            variables.speedx = 8f;
            variables.speedy = -4f;
            variables.spinningSpeed = 200f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;

            it = Instantiate(Small, transform.position, Quaternion.identity).gameObject;
            variables = it.GetComponent<Meteorite>();
            variables.speedx = -4f;
            variables.speedy = 8f;
            variables.spinningSpeed = 200f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;

            it = Instantiate(Small, transform.position, Quaternion.identity).gameObject;
            variables = it.GetComponent<Meteorite>();
            variables.speedx = -8f;
            variables.speedy = -6f;
            variables.spinningSpeed = 200f;
            variables.starthealth = starthealth / 2;
            variables.Damage = Damage / 2;
        }
        Destroy(gameObject);
    }
}
