using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class FaseChanger : MonoBehaviour
{
    public bool Zoomdin;
    public bool Regular;
    public CinemachineBrain Camera;
    public CinemachineVirtualCamera RegularCamera;
    public CinemachineVirtualCamera ZoomedinCamera;
    public GameObject Stars1;
    public GameObject Stars2;
    public scrolling background;
    public bool speedchanged;
    public List<GameObject> PlayerEngines;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(tester());
        PlayerAttack player = FindObjectOfType<PlayerAttack>();
        PlayerEngines[0] = player.transform.GetChild(2).gameObject;
        PlayerEngines[1] = player.transform.GetChild(3).gameObject;
        PlayerEngines[2] = player.transform.GetChild(4).gameObject;
        PlayerEngines[3] = player.transform.GetChild(5).gameObject;



    }

    // Update is called once per frame
    void Update()
    {
        if (Zoomdin)
        {

            RegularCamera.enabled = false;
            ZoomedinCamera.enabled = true;
            ParticleSystem system = Stars1.GetComponent<ParticleSystem>();
            ParticleSystem system2 = Stars2.GetComponent<ParticleSystem>();
            var emmisionModule = system.emission;
            var emmisionModule2 = system2.emission;
            emmisionModule.rateOverTime = 200f;
            emmisionModule2.rateOverTime = 35f;
            system.startSpeed = 15f;
            system2.startSpeed = 10f;
            PlayerEngines[0].SetActive(false);
            PlayerEngines[1].SetActive(false);
            PlayerEngines[2].SetActive(true);
            PlayerEngines[3].SetActive(true);

            if (!speedchanged)
            {
                background.scrolespeed = 0.03f;
                background.ChangeSpeed();
                speedchanged = true;

            }
        



        }
        else
        {
            RegularCamera.enabled = true;
            ZoomedinCamera.enabled = false;
            ParticleSystem system = Stars1.GetComponent<ParticleSystem>();
            ParticleSystem system2 = Stars2.GetComponent<ParticleSystem>();
            var emmisionModule = system.emission;
            var emmisionModule2 = system2.emission;
            emmisionModule.rateOverTime = 20f;
            emmisionModule2.rateOverTime = 7f;
            system.startSpeed = 5f;
            system2.startSpeed = 1f;
            PlayerEngines[3].SetActive(false);
            PlayerEngines[2].SetActive(false);
            PlayerEngines[1].SetActive(true);
            PlayerEngines[0].SetActive(true);

            if (speedchanged)
            {
                background.scrolespeed = 0.003f;
                background.ChangeSpeed();
                speedchanged = false;

            }
        }
            


    }

    public IEnumerator tester()
    {
        yield return new WaitForSeconds(20);
        Zoomdin = true;
        yield return new WaitForSeconds(20);
        Zoomdin = false;
    }
}