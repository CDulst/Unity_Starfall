using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteDestroyerClaw_07 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var enemys = FindObjectsOfType<ENEM__Claw_07>();
        if (enemys.Length == 0)
        {
            Destroy(gameObject);
        }
    }
}
