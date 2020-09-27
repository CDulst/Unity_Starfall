using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public int playerHP = 2000;
    public GameObject Bullet;
    public bool shooting;
    public bool shootingstarted;
    public float RPM;
    public GameObject Shootingposition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!shootingstarted)
        {
            StartCoroutine(Shooting());
        }
        
    }
    private IEnumerator Shooting()
    {
        shootingstarted = true;
        while(shooting)
        {
            Instantiate(Bullet, Shootingposition.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(RPM);

        }

    }
    public void Shootingswitchtrue()
    {
		if (!shooting)
		{
			print("shooting");
			shootingstarted = false;
			shooting = true;

		}
        
    }
    public void Shootingswitchfalse()
    {
        print("stopped shooting");
        shooting = false;
    }
}
