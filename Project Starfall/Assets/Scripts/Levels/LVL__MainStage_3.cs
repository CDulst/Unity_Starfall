using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVL__MainStage_3 : MonoBehaviour
{
    public List<GameObject> spawnpointsClaw_07;
    public List<GameObject> spawnpointsClaw_13;
    public List<GameObject> spawnpointsClaw_05;
    public GameObject claw05;
    public GameObject claw07__1;
    public GameObject claw07__2;
    public GameObject claw07__3;
    public GameObject claw07__4;
    public GameObject claw07__5;
    public GameObject claw13__1;
    public GameObject claw13__2;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawnscript());
    }

    // Update is called once per frame
    void Update()
    {

    }
    private IEnumerator Spawn__Claw07(float waittime, int totalspawns, int spawnpointnumber, GameObject Claw07Type, string Claw07TypeS, string route )
    {
        for(int i = 0; i < totalspawns; i++)
        {
            Instantiate(spawnpointsClaw_07[spawnpointnumber], spawnpointsClaw_07[spawnpointnumber].transform.position, Quaternion.identity);
            Instantiate(Claw07Type, transform.position, Quaternion.identity);
            GameObject[] objs = GameObject.FindGameObjectsWithTag(Claw07TypeS);
            foreach (GameObject enemy in objs)
            {
                enemy.GetComponent<ENEM__Claw_07>().tofollowroute = route;
            }
            yield return new WaitForSeconds(waittime);
            Debug.Log("+1");
        }
       
    }
 private void Spawn__Claw13(int spawnpointnumber, GameObject Claw13Type, string Claw13TypeS, string route )
    {
       
            Instantiate(spawnpointsClaw_13[spawnpointnumber], spawnpointsClaw_13[spawnpointnumber].transform.position, Quaternion.identity);
            Instantiate(Claw13Type, transform.position, Quaternion.identity);
            GameObject[] objs = GameObject.FindGameObjectsWithTag(Claw13TypeS);
            foreach (GameObject enemy in objs)
            {
            enemy.GetComponent<ENEM__Claw_13>().tofollowroute = route;
            }
            
        
       
    }
private void Spawn__Claw05(int spawnpointnumber, GameObject Claw05Type)
    {
        Instantiate(spawnpointsClaw_05[spawnpointnumber], spawnpointsClaw_05[spawnpointnumber].transform.position, Quaternion.identity);
        Instantiate(Claw05Type, transform.position, Quaternion.identity);
    }
   public IEnumerator Spawnscript()
    {
        StartCoroutine(Spawn__Claw07(1f, 6, 0, claw07__1, "claw07__1", "Route__claw07--1"));
        yield return new WaitForSeconds(2f);
        StartCoroutine(Spawn__Claw07(1f, 2, 1, claw07__2, "claw07__2", "Route__claw07--2"));
        yield return new WaitForSeconds(5f);
        Spawn__Claw13(0, claw13__1, "Claw13__1", "Route__claw13--1");
        yield return new WaitForSeconds(20f);
        Spawn__Claw05(0, claw05);
        Spawn__Claw13(1, claw13__2, "Claw13__2", "Route__claw13--2");
        yield return new WaitForSeconds(0.5f);
        Spawn__Claw13(2, claw13__1, "Claw13__1", "Route__claw13--3");
        

    }
    
}
