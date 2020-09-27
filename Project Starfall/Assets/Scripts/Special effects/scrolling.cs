using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scrolling : MonoBehaviour
{
    public float scrolespeed = 0.01f;
    private float offset;
    private float current;
    // Start is called before the first frame update
    void Start()
    {
        offset = Time.deltaTime * scrolespeed;
        current = offset;
    }

    // Update is called once per frame
    void Update()
    {
        
        GetComponent<MeshRenderer>().material.mainTextureOffset =  new Vector2(current, 0);
        current = current + offset;
    }

   public void ChangeSpeed()
    {
        offset = Time.deltaTime * scrolespeed;
       
    }
}
