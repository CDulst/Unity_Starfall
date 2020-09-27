using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ENEM__Claw_13 : MonoBehaviour
{
    public GameObject retreatpoint;
    public GameObject frontalpoint;
    public GameObject frontalAOE;
    public GameObject acollider;
    public GameObject attackcollider;
    public bool lookatplayer = true;
    public bool retreating = true;
    public float timebeforeattack = 5f;
    public float timebeforestopping = 2f;
    public string tofollowroute;
    public List<GameObject> Routepoints;
    public bool reachedposition;
    public int i;
    public bool movingdone;
    public bool AOEActivated;
    public bool destroyable;
    // Start is called before the first frame update
    void Start()
    {
        Transform[] allchildren = GetComponentsInChildren<Transform>();
        foreach (Transform child in allchildren)
        {
            if (child.gameObject.tag == "Claw13__Retreatpoint")
            {
                retreatpoint = child.gameObject;
            }
            else if (child.gameObject.tag == "Claw13__frontalpoint")
            {
                frontalpoint = child.gameObject;
            }
            else if (child.gameObject.tag == "Claw13__AOE")
            {
                frontalAOE = child.gameObject;
            }
            else if (child.gameObject.tag == "Claw13__collider")
            {
                acollider = child.gameObject;
            }
            else if (child.gameObject.tag == "Claw13__attack")
            {
                attackcollider = child.gameObject;
            }

            



        }
        
        attackcollider.SetActive(false);
        frontalAOE.SetActive(false);
       
        Getroute();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movingdone)
        {
            moveroute();
        }
        Lookatplayer();


        if (movingdone)
        {
            if (!AOEActivated)
            {
                frontalAOE.SetActive(true);
                AOEActivated = true;
            }
            
  
            StartCoroutine(Looktime());
            
        }
        if (!lookatplayer)
        {
            
            StartCoroutine(RetreatTime());
            Retreat();
        }
        if (!retreating)
        {
            acollider.SetActive(false);
            attackcollider.SetActive(true);
            
            Rushtowardsplayer();
        }
        StartCoroutine(DestroySelf());
        

      
    }
    private void moveroute()
    {
        transform.position = Vector2.MoveTowards(transform.position, Routepoints[i].transform.position, 5f * Time.deltaTime);
        if (transform.position == Routepoints[i].transform.position)
        {
            reachedposition = true;
        }
    }
    private void Getroute()
    {
        GameObject route = GameObject.FindGameObjectWithTag(tofollowroute);
        Transform[] allchildren = route.GetComponentsInChildren<Transform>();
        foreach (Transform child in allchildren)
        {
            if (child.gameObject.tag == "Route__Child")
            {
                Routepoints.Add(child.gameObject);
            }

        }
        StartCoroutine(Startmoving());
    }
    private IEnumerator Startmoving()
    {
        
            for (i = 0; i < Routepoints.Count; i++)
            {
                reachedposition = false;
                while (!reachedposition)
                {
                    yield return null;
                }


            }
        yield return new WaitForSeconds(0.2f);
        movingdone = true;
        

    }
    private void Lookatplayer()
    {
        if (lookatplayer)
        {
            PlayerMovement player = FindObjectOfType<PlayerMovement>();
            Vector2 direction = new Vector2(
                player.transform.position.x - transform.position.x,
                player.transform.position.y - transform.position.y
                );

            transform.up = direction;
        }
        
       
    }
    private IEnumerator Looktime()
    {
        yield return new WaitForSeconds(timebeforeattack);
        frontalAOE.SetActive(false);
        lookatplayer = false;
        
    }
    private void Retreat()
    {
        if (retreating)
        {
            transform.position = Vector2.MoveTowards(transform.position, retreatpoint.transform.position, 4f*Time.deltaTime);
        }
           
        
    }
    private IEnumerator RetreatTime()
    {
        yield return new WaitForSeconds(timebeforestopping);
        retreating = false;
    }
    private void Rushtowardsplayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, frontalpoint.transform.position, 50f * Time.deltaTime);
    }
    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(50f);
        Destroy(gameObject);
    }
    
}
