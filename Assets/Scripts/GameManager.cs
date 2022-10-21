using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Ball;
    public Planet []planetPrefabs;
    public Rocket rocketPrefab;
    public Camera freeCam;

    private int curPlanet = 0; //counts from 0 to planetPrefabs.Length;

    public bool inPlay = false;
    private bool paused = false;
    private bool pausable = true; //used to disable pause on the game over screen

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


    private int nBackgroundPlanets = 0;
    private Planet[] backgroundPlanets;

    /* modeUnlocked 0 - game not won yet
     * modeUnlocked 1 - Game won, hardmode unlocked
     * modeUnlocked 2 - Hard mode won
     * enums? Who needs um
    */
    public static int modeUnlocked = 0;

    private bool hardModeActive = false;


    private void Awake()
    {
        if (sing != null)
            throw new System.Exception("Singleton broken");

        sing = this;
    }
    void instantiateBackgroundPlanets()
    {
        backgroundPlanets = new Planet[nBackgroundPlanets];
        for (int i = 0; i < nBackgroundPlanets; i++) {
            backgroundPlanets[i]=Instantiate(planetPrefabs[(int)Random.Range(0.0f, planetPrefabs.Length)]);
            backgroundPlanets[i].gameObject.transform.position=Random.onUnitSphere*Random.Range(50f,200f);
            backgroundPlanets[i].gameObject.transform.rotation=Random.rotationUniform;
           
        }
    }
    private void attachBackgroundPlanets()
    {
        if (ico)
        {
            foreach (Planet p in backgroundPlanets)
            {
                p.gameObject.transform.parent = ico.transform;
            }
        }
    }
    private void detachBackgroundPlanets()
    {
        foreach (Planet p in backgroundPlanets)
        {
            p.gameObject.transform.parent = null;
        }
    }

    void Start()
    {
        paused = false;
        objInRange = new List<GameObject>();
        activeCam = Camera.main;

        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (!player)
        {
            Debug.Log("Couldn't find player object in GameManager!");
        }

        curPlanet = 0;
        instantiateBackgroundPlanets();
    }

    // Update is called once per frame
    void Update()
    {
        uiManager.SetRocketFActive(false);
        if (fuel >= targetFuel)
        {
            uiManager.SetFueledActive(true);
        } else
        {
            uiManager.SetFueledActive(false);
        }

        if (paused)
        {
            if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
            {
                Unpause();
            }
        } 
        else
        {

            if (pausable && (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)))
            {
                Pause();
                Debug.Log("game state: " + GameManager.modeUnlocked);
            }
            

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
                        if (fuel >= targetFuel)
                        {
                            uiManager.SetRocketFActive(true);
                        }
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
    }

    public void Pause()
    {
        Time.timeScale = 0.0f;
        uiManager.Pause();
        paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void Unpause()
    {
        Time.timeScale = 1.0f;
        uiManager.Unpause();
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }






    public void startGameHardmode()
    {
        startGame(true);
    }

    public void startGame(bool hardmode=false)
    {
        hardModeActive=hardmode;
        inPlay = false;
        uiManager.setStartMenuActive(false);
        uiManager.setHUDActive(true);
        pausable = true; //reset pausable if we came from game over menu

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        fuel = 0;
        timeLeft = OptionsMenu.duration;

        curPlanet = 0;
        spawnPlanet();

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
        fuel = 0;
        timeLeft = OptionsMenu.duration;
        uiManager.setTimerTime(timeLeft);
        //uiManager.setTransparency(1.0f);
        uiManager.setFuel(0.0f);
        uiManager.setNeededFuel(targetFuel);
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


    public void EndGameNow()
    {
        switchCam(sing.freeCam);

        player.gameObject.SetActive(false);

        sing.uiManager.setStartMenuActive(true);
        sing.uiManager.setHUDActive(false);

        // Enable cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public IEnumerator EndGame()
    {

        switchCam(sing.freeCam);

        yield return new WaitForSeconds(3);

        uiManager.SetGameOverActive(true);
        pausable = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        /*
        player.gameObject.SetActive(false);

        sing.uiManager.setStartMenuActive(true);
        sing.uiManager.setHUDActive(false);

        // Enable cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;*/
    }

    public void spawnPlanet()
    {
        detachBackgroundPlanets();
        if (ico)
            Destroy(ico.gameObject);

        ico = Instantiate(planetPrefabs[curPlanet % planetPrefabs.Length]);
        ico.GetComponent<Spinner>().enabled = false;
        ico.size = 2.3f;
        curPlanet++;
        uiManager.setPlanetText(curPlanet);
        attachBackgroundPlanets();
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
            rocket.GetComponent<Rigidbody>().AddForce(transform.up * 10f); 
            rocket.transform.localRotation = Quaternion.Slerp(rocket.transform.localRotation, Quaternion.identity, 1.0f - Mathf.Exp(-Time.deltaTime *1f));
            yield return new WaitForFixedUpdate();
        }

        //get next planet and start next sequence
        spawnPlanet();

        if (curPlanet == 6)
        {
            pausable = false;
            Time.timeScale = 0.0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            if (hardModeActive)
            {
                //hard mode won
                if (modeUnlocked == 1)
                    modeUnlocked = 2;
                uiManager.SetHardmodeVictoryActive(true);

            } else
            {
                uiManager.SetVictoryActive(true);
                //unlock hard mode if not already unlocked
                if (modeUnlocked == 0)
                    modeUnlocked = 1;
            }
        } else
        {
            StartCoroutine(LandRocket());
        }
    }
    public void ReturnFromGameWon()
    {
        StartCoroutine(LandRocket());
        pausable = true;
        Time.timeScale = 1.0f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

        fuel = 0;
        timeLeft = OptionsMenu.duration;
        uiManager.setTimerTime(timeLeft);
        //uiManager.setTransparency(1.0f);
        uiManager.setFuel(0.0f);
        uiManager.setNeededFuel(targetFuel);

        rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
       
        // Freeze for landing
        rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Rotate planet so that up is aligned with the rocket.
        //rocket.transform.localRotation = Quaternion.identity;
        rocket.transform.localPosition = Vector3.up * rocketStartHeight;
        //ico.transform.rotation = Quaternion.Inverse(rocket.transform.rotation);

        float timestamp = Time.time + 3;
        while (Time.time < timestamp)
        {
            rocket.transform.localPosition = Vector3.Lerp(rocket.transform.localPosition, Vector3.up * rocketTargetHeight, 1.0f-Mathf.Exp(-Time.deltaTime * rocketSpeed));
            rocket.transform.localRotation = Quaternion.Slerp(rocket.transform.localRotation,Quaternion.identity,1.0f - Mathf.Exp(-Time.deltaTime * rocketSpeed));
            yield return new WaitForEndOfFrame();
        }
        rocket.transform.localRotation = Quaternion.identity;

        startRound();
    }

    public static void switchCam(Camera cam)
    {
        activeCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);

        activeCam = cam;
    }
}
