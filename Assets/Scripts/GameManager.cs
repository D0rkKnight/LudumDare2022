using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Ball;
    public Planet []planetPrefabs;
    public Rocket rocketPrefab;
    public Camera freeCam;

    private int curPlanet = 0; //counts from 0 to planetPrefabs.Length;

    bool rocketBoarded = false;
    public bool inPlay = false;

    public static List<GameObject> objInRange;
    public static Player player;
    public static Rocket rocket;

    public static float fuel = 0;

    public static Planet ico;

    public static float timeLeft = 10f;

    public static Camera activeCam;

    public static GameManager sing;


    public UIManager uiManager;

    private float targetFuel = 10.0f;


    private void Awake()
    {
        if (sing != null)
            throw new System.Exception("Singleton broken");

        sing = this;
    }

    void Start()
    {
        objInRange = new List<GameObject>();
        activeCam = Camera.main;

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (!player)
        {
            Debug.Log("Couldn't find player object in GameManager!");
        }

        curPlanet = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inPlay)
        {
            // Raycast objects
            RaycastHit hit;
            LayerMask mask = LayerMask.GetMask(new string[] { "Selectable" });

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, mask))
            {
                GameObject obj = hit.transform.gameObject;

                if (objInRange.Contains(obj))
                {
                    // Rocket boarding / win condition
                    if (Input.GetKeyDown(KeyCode.F) && fuel >= targetFuel)
                    {
                        endRound(true);
                    }
                }
            }

            // Handle lose condition
            if (timeLeft > 0 && timeLeft - Time.deltaTime <= 0)
                endRound(false);

            timeLeft -= Time.deltaTime;
            uiManager.setTimerTime(timeLeft);
            uiManager.setFuel(fuel);
        }
    }





    public void startGame()
    {
        inPlay = false;
        uiManager.setStartMenuActive(false);
        uiManager.setHUDActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        curPlanet = 0;
        spawnPlanet();

        Debug.Log("Rocket bool: "+((bool)rocket).ToString());
        if (rocket)
        {
            Destroy(rocket);
        }            
        // Spawn rocket
        rocket = Instantiate(rocketPrefab);
        



        StartCoroutine(LandRocket());
    }

    //enable player interaction, start countdown, attach the rocket to a tile, etc.
    public void startRound()
    {
        inPlay = true;

        ico.attachRocket(rocket);

        // Dump player back out and enable the spinner
        player.gameObject.SetActive(true);
        Spinner sp = ico.GetComponent<Spinner>();
        sp.attachPlayer(player);
        sp.enabled = true;

        //Init HUD
        timeLeft = OptionsMenu.duration;
        uiManager.setTimerTime(timeLeft);
        uiManager.setTransparency(1.0f);
        uiManager.setFuel(0.0f);
        uiManager.setNeededFuel(10.0f);
        uiManager.setHUDActive(true);

        //Start the planet rumble sequence
        //Rumbling and stuff is handled by ico's Update function.
        ico.triggerRumble(timeLeft);

        // Swap cam views back to player
        switchCam(player.cam);
    }    
    //End the round. Success == true means we switch to the rocket and LaunchRocket, false means game over. 
    public void endRound(bool success)
    {
        inPlay = false;
        if (success)
        {
            switchCam(rocket.cam);
            ico.GetComponent<Spinner>().enabled = false;
            // Get into the rocket!
            player.gameObject.SetActive(false);
            StartCoroutine(LaunchRocket());
        }
        else
        {
            StartCoroutine(EndGame());
        }
    }



    public IEnumerator EndGame()
    {

        switchCam(sing.freeCam);

        yield return new WaitForSeconds(3);
        player.gameObject.SetActive(false);

        sing.uiManager.setStartMenuActive(true);
        sing.uiManager.setHUDActive(false);

        // Enable cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void spawnPlanet()
    {
        if (ico)
            Destroy(ico.gameObject);

        ico = Instantiate(planetPrefabs[curPlanet]);
        ico.GetComponent<Spinner>().enabled = false;
        ico.size = 2.3f;
        curPlanet=(curPlanet+1)%planetPrefabs.Length;
    }



    public IEnumerator LaunchRocket()
    {
        //Rocket takes at most 1.5 seconds to lift off
        // Deparent rocket
        rocket.transform.parent = null;
        float wait = Mathf.Clamp(timeLeft, 0.0f, 1.5f);
        yield return new WaitForSeconds(wait);

        fuel = 0; // Reset fuel

        rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        float timestamp = Time.time + 2;
        while (Time.time < timestamp)
        {
            rocket.GetComponent<Rigidbody>().AddForce(transform.up * Time.deltaTime * 40f);
            yield return new WaitForFixedUpdate();
        }

        //get next planet and start next sequence
        spawnPlanet();
        StartCoroutine(LandRocket());
    }

    // Assumes rocket is attached to a tile
    public IEnumerator LandRocket()
    {
        float rocketStartHeight = 10.0f;
        float rocketSpeed = 2.0f;
        float rocketTargetHeight = ico.size-0.08f;

        switchCam(rocket.cam);

        // Deactivate player for landing animation
        player.gameObject.SetActive(false);

        rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
       
        // Freeze for landing
        rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Rotate planet so that up is aligned with the rocket.
        rocket.transform.localRotation = Quaternion.identity;
        rocket.transform.localPosition = Vector3.up * rocketStartHeight;
        //ico.transform.rotation = Quaternion.Inverse(rocket.transform.rotation);

        float timestamp = Time.time + 3;
        while (Time.time < timestamp)
        {
            rocket.transform.localPosition = Vector3.Lerp(rocket.transform.localPosition, Vector3.up * rocketTargetHeight, 1.0f-Mathf.Exp(-Time.deltaTime * rocketSpeed));
            yield return new WaitForEndOfFrame();
        }

        startRound();
    }

    public static void switchCam(Camera cam)
    {
        activeCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);

        activeCam = cam;
    }
}
