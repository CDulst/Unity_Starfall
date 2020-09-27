using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingWall : MonoBehaviour
{
    public float movingspeed = 0.2f;
    public float TimeBeforeDestroy = 20;
    // Start is called before the first frame update
    

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x - movingspeed * Time.deltaTime, transform.position.y);
        StartCoroutine(Destroy());
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(TimeBeforeDestroy);
        Destroy(gameObject);
    }

    
}
