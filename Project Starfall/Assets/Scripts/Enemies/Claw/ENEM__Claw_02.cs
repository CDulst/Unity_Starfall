using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ENEM__Claw_02 : MonoBehaviour
{
    //Rocket
    // Rocket Floats
    public float Rocketdamage = 500f;
    public float RocketMovement = 1f;
    //Rocket Gameobjects
    public GameObject Rocketroute;
    public List<GameObject> Rocketpoints;
    public GameObject RocketAOE;
    public List<GameObject> RocketStartingPoints;
    public GameObject rocket;

    //Rocket Bool
    public bool Rocketrouteon;
    public bool firingrockets = true;
    //Laser
    //Laser Floats
    public float Laserspeed = 5f;
    public float Laserdamage = 100f;
    //Laser Bool
    public bool Laserrouteon;
    public bool Lasernumberchosen;
    public bool laserreached;
    //Laser Gameobjects
    public List<GameObject> Laserroute;
    public GameObject Laser;

    //Gun
    //Gun Floats
    public float Gundamage = 50f;
    //Gun Bool
    public bool Gunrouteon;
    public bool idle;
    public bool idleon = true;
    public bool moving;
    public Rigidbody2D myrigidbody;
    public GameObject Route;



    

    public List<GameObject> Gunroute;
    public List<GameObject> GunPoints;

    public GameObject idlepoint;
    float bulletdamage;
    public GameObject impactbullet;
    public float GunMovement = 0.02f;

    public bool reachedposition;
    public GameObject enemyBullet;

    public int numberlaser;


    public float idlemovement = 5f;
    public bool shooting;
    public int k;
    int happenonce = 0;

    public float health = 30000f;
    public float healthbarlength = 1f;
    GameObject bar;

    //shield
    public GameObject shield;
    public GameObject shieldbar;
    public bool shielddown;
    public float shieldstrength = 1000f;
    public float shieldlowering = 0.00002f;
    public float shieldlength;
    public GameObject brokenshield;


    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(AI());
        myrigidbody = GetComponent<Rigidbody2D>();
        Getbulletpoints();
        GetRocketPoints();
        GetLaser();
        GetShield();

        Getroutes();



    }

    // Update is called once per frame
    void Update()
    {
        if (!shielddown && shieldbar.GetComponent<Transform>().localScale.x > 0)
        {
            HigherShield();
        }
        if (health <= 0)
        {
            Destroy(gameObject);
        }
        
        if (idleon)
        {
            if (happenonce == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, idlepoint.transform.position, idlemovement * Time.deltaTime);
                if (transform.position == idlepoint.transform.position)
                {
                    happenonce = 1;
                }

            }

            StartCoroutine(IdleMovement());
        }
        else if (Gunrouteon)
        {
            happenonce = 0;
            StartCoroutine(Bulletspawn());
            GunAttack();


        }
        else if (Rocketrouteon)
        {
            happenonce = 0;
            StartCoroutine(RocketAttack());
        }
        else if (Laserrouteon)
        {
            happenonce = 0;
            StartCoroutine(LaserAttack());
        }
    }
    private IEnumerator IdleMovement()
    {

        if (!moving)
        {


            idle = false;
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
            idle = true;

        }

    }

    public void GetShield()
    {
        shield = gameObject.transform.GetChild(12).gameObject;
        shieldbar = shield.transform.GetChild(0).gameObject;
        
    }
    public void GetRocketPoints()
    {
        for (int y = 0; y < 24; y++)
        {
            Rocketpoints[y] = Route.transform.GetChild(4).GetChild(y).gameObject;
        }

    }
    public void Getbulletpoints()
    {
        GunPoints[0] = transform.GetChild(5).GetChild(0).gameObject;
        GunPoints[1] = transform.GetChild(5).GetChild(1).gameObject;
        GunPoints[2] = transform.GetChild(5).GetChild(2).gameObject;
    }
    public void Getroutes()
    {

        Transform[] allchildren = Route.GetComponentsInChildren<Transform>();
        Rocketroute = Route.transform.GetChild(3).gameObject;
        idlepoint = Route.transform.GetChild(2).gameObject;
        foreach (Transform child in allchildren)
        {

            if (child.gameObject.tag == "Gunattack")
            {
                int i = 0;
                foreach (Transform point in child.GetComponentsInChildren<Transform>())
                {
                    if (point.gameObject.tag != "Gunattack")
                    {
                        Gunroute[i] = point.gameObject;
                        i += 1;
                    }

                }

            }
            else if (child.gameObject.tag == "Laserattack")
            {
                int i = 0;
                foreach (Transform side in child.GetComponentsInChildren<Transform>())
                {
                    if (side.gameObject.tag != "Laserattack")
                    {
                        foreach (Transform point in child.GetComponentsInChildren<Transform>())
                        {
                            if (point.gameObject.tag != "DW")
                            {
                                Laserroute[i] = point.gameObject;
                                i += 1;
                            }


                        }

                    }




                }

            }





        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player bullet" || collision.gameObject.tag == "Claw13__attack" || collision.gameObject.tag == "Meteor" || collision.gameObject.tag == "circle")
        {
            if (collision.gameObject.tag == "Player bullet")
            {
                bulletdamage = collision.GetComponent<projectile>().Bulletdamage;
                Destroy(collision.gameObject);

                bar = this.gameObject.transform.GetChild(9).GetChild(1).gameObject;
                if (!shielddown)
                {
                    LowerShield();
                }
                else
                {
                    LowerHealth(bar, bulletdamage);
                }
                
            }
            else if (collision.gameObject.tag == "circle")
            {
                float bulletdamagek = collision.GetComponent<Explosion_bomb>().Damage;
                GameObject bark = this.gameObject.transform.GetChild(9).GetChild(1).gameObject;
                print(bark);
                if (!shielddown)
                {
                    LowerShield();
                }
                else
                {
                    LowerHealth(bar, bulletdamage);
                }
            }
            else if (collision.gameObject.tag == "circle")
            {
                float bulletdamagec = collision.GetComponent<Explosion_bomb>().Damage;
                GameObject barc = this.gameObject.transform.GetChild(9).GetChild(1).gameObject;
                print(barc);
                if (!shielddown)
                {
                    LowerShield();
                }
                else
                {
                    LowerHealth(bar, bulletdamage);
                }
            }
            else if (collision.gameObject.tag == "Meteor")
            {
                float bulletdamaged = collision.GetComponent<Meteorite>().Damage;
                GameObject bard = this.gameObject.transform.GetChild(9).GetChild(1).gameObject;
                print(bard);
                if (!shielddown)
                {
                    LowerShield();
                }
                else
                {
                    LowerHealth(bar, bulletdamage);
                }
            }
            else
            {
                bulletdamage = collision.GetComponent<Attackcollider>().Damage;
            }

            StartCoroutine(Hiteffects());

        }

    }
    private void LowerHealth(GameObject bar, float bulletdamage)
    {
        if (health > 0)
        {
            float divided = bulletdamage / health;
            float newscalex = healthbarlength - (divided * healthbarlength);
            healthbarlength = newscalex;
            bar.GetComponent<Transform>().localScale = new Vector3(healthbarlength, 0.4f, 0);

            health -= bulletdamage;
        }
    }

        private IEnumerator Hiteffects()
    {
        Instantiate(impactbullet, transform.GetChild(6).transform.position, Quaternion.identity);
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.1f);
    }
    public void GunAttack()
    {


        transform.position = Vector2.MoveTowards(transform.position, Gunroute[k].transform.position, GunMovement * Time.deltaTime);
        if (transform.position == Gunroute[0].transform.position || transform.position == Gunroute[1].transform.position || transform.position == Gunroute[2].transform.position)
        {
            k += 1;
        }
        else if (transform.position == Gunroute[3].transform.position)
        {
            k = 0;
        }



    }
    public void LowerShield()
    {
        float divided = bulletdamage / shieldstrength;
        float newscalex = shieldlength + (divided * 7);
        shieldlength = newscalex;
        shieldbar.GetComponent<Transform>().localScale = new Vector3(shieldlength, 3f, 0);

        shieldstrength -= bulletdamage;
        if (shieldlength >= 7)
        {
            print("ShieldDown");
           GameObject broken = Instantiate(brokenshield, shield.transform.position, Quaternion.identity).gameObject;
            broken.transform.parent = gameObject.transform;
            Destroy(shield);
            Destroy(transform.GetChild(11).gameObject);
            shielddown = true;
        }

    }
    public void HigherShield()
    {
       
        float divided = shieldlowering / shieldstrength;
        float newscalex = shieldlength - (divided * 7);
        shieldlength = newscalex;
        shieldbar.GetComponent<Transform>().localScale = new Vector3(shieldlength, 3f, 0);

        shieldstrength += shieldlowering;
       

    }

    public IEnumerator Bulletspawn()
    {
        if (!shooting)
        {
            shooting = true;
            while (Gunrouteon)
            {
                Instantiate(enemyBullet, GunPoints[0].transform.position, Quaternion.identity);
                Instantiate(enemyBullet, GunPoints[1].transform.position, Quaternion.identity);
                Instantiate(enemyBullet, GunPoints[2].transform.position, Quaternion.identity);
                yield return new WaitForSeconds(0.3f);
            }
            shooting = false;
        }

    }
    public IEnumerator RocketAttack()
    {
        transform.position = Vector2.MoveTowards(transform.position, Rocketroute.transform.position, RocketMovement * Time.deltaTime);
        yield return new WaitForSeconds(2f);

        while (firingrockets && Rocketrouteon)
        {
            firingrockets = false;
            int lastnumber = 0;
            GetComponent<Animator>().SetBool("Firingrocket", true);
            for (int t = 0; t < 4; t++)
            {
                int number = Random.Range(0, 23);
                while (number == lastnumber)
                {
                    number = Random.Range(0, 23);
                }
                lastnumber = number;
                Instantiate(RocketAOE, Rocketpoints[number].transform.position, Quaternion.identity);

                GameObject Rocket = Instantiate(rocket, transform.GetChild(3).GetChild(t).transform.position, Quaternion.identity);

                Rocket.GetComponent<Missile>().speed = 20f;
                Rocket.GetComponent<Missile>().Destination = Rocketpoints[number];
            }
            yield return new WaitForSeconds(0.5f);
            GetComponent<Animator>().SetBool("Firingrocket", false);
            yield return new WaitForSeconds(1f);
            firingrockets = true;
        }
    }
    public void GetLaser()
    {
        Laser = transform.GetChild(4).gameObject;
        Laser.SetActive(false);
    }
    public IEnumerator LaserAttack()
    {

        if (!Lasernumberchosen)
        {
            numberlaser = Random.Range(0, 2);
            Lasernumberchosen = true;
        }

        if (numberlaser == 0)
        {
            if (transform.position == Laserroute[1].transform.position || laserreached)
            {
                Laser.SetActive(true);
                laserreached = true;
                transform.position = Vector2.MoveTowards(transform.position, Laserroute[2].transform.position, Laserspeed * Time.deltaTime);

            }
            else if (!laserreached)
            {
                transform.position = Vector2.MoveTowards(transform.position, Laserroute[1].transform.position, Laserspeed * Time.deltaTime);
            }
            if (transform.position == Laserroute[2].transform.position)
            {
                Laser.SetActive(false);
                
            }


        }
        if (numberlaser == 1)
        {
            if (transform.position == Laserroute[3].transform.position || laserreached)
            {
                Laser.SetActive(true);
                laserreached = true;
                transform.position = Vector2.MoveTowards(transform.position, Laserroute[4].transform.position, Laserspeed * Time.deltaTime);

            }
            else if (!laserreached)
            {
                transform.position = Vector2.MoveTowards(transform.position, Laserroute[3].transform.position, Laserspeed * Time.deltaTime);
            }
            if (transform.position == Laserroute[4].transform.position)
            {
                Laser.SetActive(false);
                
            }
        }

        yield return new WaitForSeconds(2f);
    }
private IEnumerator AI()
    {

       
        while (true)
        {
         
            idleon = true;
            Lasernumberchosen = false;
            laserreached = false;
            yield return new WaitForSeconds(Random.Range(0,2));
            int numberAI = Random.Range(0,4);
            if (numberAI == 0)
            {
                idleon = false;
                Gunrouteon = true;
                yield return new WaitForSeconds(Random.Range(5,12));
               
                Gunrouteon = false;
            }
            else if (numberAI == 1)
            {
                idleon = false;
                Rocketrouteon = true;
                yield return new WaitForSeconds(Random.Range(5, 12));
               
                Rocketrouteon = false;
            }
            else if (numberAI == 2)
            {
                idleon = false;
                Laserrouteon = true;
                yield return new WaitForSeconds(4);
                Laserrouteon = false;
                
               
            }
            
           
          
          
           

        }
   
    }
        

}
