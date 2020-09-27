using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_bomb : MonoBehaviour
{
    public float Damage = 10000f;
    public void Destroy()
    {
        Destroy(gameObject);
    }
   
}
