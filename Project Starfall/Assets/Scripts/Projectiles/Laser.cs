using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject LaserReceiver;
    public GameObject LastPoint;
    public LineRenderer lineRenderer;
    public PlayerMovement player;
    public bool Claw2;

    // Start is called before the first frame update
    void Start()
    {
        LaserReceiver = transform.parent.GetChild(7).gameObject;
        LastPoint = transform.parent.GetChild(8).gameObject;
        lineRenderer = GetComponent<LineRenderer>();
        player = FindObjectOfType<PlayerMovement>();
        
       
    }

    // Update is called once per frame
    void Update()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, LaserReceiver.transform.position);
        if (player.transform.position.y < LastPoint.transform.position.y + 0.5f && player.transform.position.y > LastPoint.transform.position.y - 0.5f)
        {
            LaserReceiver.transform.position = player.transform.position;
        }
        else
        {
            LaserReceiver.transform.position = LastPoint.transform.position;
        }
    }
}
