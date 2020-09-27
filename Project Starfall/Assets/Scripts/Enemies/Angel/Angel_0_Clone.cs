using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Angel_0_Clone : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
