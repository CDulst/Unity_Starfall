using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroyer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Particledestroyer());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator Particledestroyer()
    {
        yield return new WaitForSeconds(2f);
        Destroy(gameObject);

    }
}
