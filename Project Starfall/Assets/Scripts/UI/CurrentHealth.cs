using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentHealth : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float currenthealth = FindObjectOfType<PlayerHealth>().CurrentHealthPoints;
        GetComponent<Text>().text = currenthealth.ToString();
    }
}
