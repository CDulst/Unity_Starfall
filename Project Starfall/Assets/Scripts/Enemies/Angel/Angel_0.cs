using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_0 : MonoBehaviour
{
    public bool zigZag;
    private bool ZigZagMoving;
    private bool MovingToPoint;
    private bool SpawnClone = true;
    private Vector2 currentlocation;
    public GameObject parts;
    public GameObject TeleportationClone;
    public GameObject MovementPoints;
    public GameObject Startpoint;
    public GameObject Currentpoint;
    // Start is called before the first frame update
    void Start()
    {
        Startpoint = MovementPoints.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (zigZag)
        {
            currentlocation = transform.position;
        }
        if (MovingToPoint)
        {
            Moving(Currentpoint);
            if (SpawnClone)
            {
                StartCoroutine(SpawnTeleport());
            }
            
        }
        ZigZag();
    }
    private void ZigZag()
    {
        if (zigZag && !ZigZagMoving)
        {
           
            GetComponent<Animator>().SetBool("ZigZag", true);
            ZigZagMoving = true;
        }
        else if (!zigZag && ZigZagMoving)
        {
            GetComponent<Animator>().SetBool("ZigZag", false);
            ZigZagMoving = false;
            Debug.Log(currentlocation);
            GetComponent<Animator>().enabled = false;
            TeleportOriginal();
        }

    }
    private void TeleportOriginal()
    {
        Transform[] allchildren = transform.GetComponentsInChildren<Transform>();
        foreach(Transform child in allchildren)
        {
            if (child.tag == "Angel-Zero__part")
            {
                Color tmp = child.gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 0f;
                child.gameObject.GetComponent<SpriteRenderer>().color = tmp;
            }
          
        }
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(false);

        Instantiate(TeleportationClone, transform.position, Quaternion.identity);

        Currentpoint = Startpoint;

        MovingToPoint = true;
        SpawnClone = true;
      }

      private void Moving (GameObject point)

    {
        transform.position = Vector2.MoveTowards(transform.position, point.transform.position, 10f * Time.deltaTime);
        if (transform.position == point.transform.position)
        {
            Debug.Log("hello");
            MovingToPoint = false;
            SpawnClone = false;
            Reapier();
        }
    }
    private IEnumerator SpawnTeleport()
    {
        SpawnClone = false;
        yield return new WaitForSeconds(0.1f);
        Instantiate(TeleportationClone, transform.position, Quaternion.identity);
        SpawnClone = true;
    }
    private void Reapier()
    {
        Debug.Log("hello");
        Transform[] allchildren = transform.GetComponentsInChildren<Transform>();
        foreach (Transform child in allchildren)
        {
            if (child.tag == "Angel-Zero__part")
            {
                Color tmp = child.gameObject.GetComponent<SpriteRenderer>().color;
                tmp.a = 100f;
                child.gameObject.GetComponent<SpriteRenderer>().color = tmp;
            }

        }
        transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        GetComponent<Animator>().enabled = true;

    }
}
    
