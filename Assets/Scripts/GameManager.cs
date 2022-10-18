using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Ball;
    bool rocketBoarded = false;
    public Planet []planetPrefabs;
    private int curPlanet = 0; //counts from 0 to planetPrefabs.Length;
    public Rocket rocketPrefab;
    public Camera freeCam;

    public static List<GameObject> objInRange;
    public static Player player;
    public static Rocket rocket;

    public static float fuel = 0;

    public static Planet ico;

    public static float timeLeft = 10f;

    public static bool gameIsOver = false;
    public static bool startReady = true;
    public static bool restartReady = false;
    public static bool inPlay = false;

    public static Camera activeCam;

    public static GameManager sing;


    public UIManager uiManager;


    private void Awake()
    {
        if (sing != null)
            throw new System.Exception("Singleton broken");

        sing = this;
        curPlanet = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        objInRange = new List<GameObject>();
        activeCam = Camera.main;
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
        if (!player)
        {
            Debug.Log("Couldn't find player object in GameManager!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Raycast objects
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask(new string[] { "Selectable" });

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100f, mask))
        {
            GameObject obj = hit.transform.gameObject;

            if (objInRange.Contains(obj))
            {
                // Rocket boarding
                if (Input.GetKeyDown(KeyCode.F) && fuel >= 10)
                {
                    switchCam(rocket.cam);

                    ico.GetComponent<Spinner>().enabled = false;
                    // Get into it!
                    player.gameObject.SetActive(false);
                    StartCoroutine(LiftOff());
                }
            }
        }

        // Update timer
        if (timeLeft > 0 && timeLeft - Time.deltaTime <= 0)
            StartCoroutine(explode());

        timeLeft -= Time.deltaTime;
        uiManager.setTimerTime(timeLeft);
        uiManager.setFuelText(fuel);
    }
    IEnumerator LiftOff()
    {
        int wait = Mathf.Min(2, (int)timeLeft-1);
        for (int i = 0; i < wait; i++)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(wait - i);
        }
        rocketBoarded = true;
        fuel = 0; // Out of fuel

        // Deparent rocket
        rocket.transform.parent = null;
        StartCoroutine(rocket.boostOff());
    }

    public IEnumerator explode()
    {
        ico.explode();

        if (!rocketBoarded)
        {
            StartCoroutine(gameOver());
        }
        else
        {
            // Load in a new planet
            yield return new WaitForSeconds(3);

            spawnPlanet();
            StartCoroutine(landRocket());
        }
    }

    public static IEnumerator gameOver()
    {
        gameIsOver = true;
        inPlay = false;

        switchCam(sing.freeCam);

        yield return new WaitForSeconds(3);
        player.gameObject.SetActive(false);

        startReady = true; // Toggle restart flip flop
        sing.uiManager.setStartMenuActive(true);
        sing.uiManager.setHUDActive(false);


        // Enable cursor for UI interaction
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void startGame()
    {
        gameIsOver = false;
        startReady = false;
        sing.uiManager.setStartMenuActive(false);
        sing.uiManager.setHUDActive(false);
        restartReady = false;
        inPlay = true;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        sing.spawnPlanet();

        if (rocket == null)
        {
            // Spawn rocket
            Rocket rok = Instantiate(sing.rocketPrefab);
        }

        sing.StartCoroutine(sing.landRocket());
    }

    public void spawnPlanet()
    {
        if (ico != null)
            Destroy(ico);

        ico = Instantiate(planetPrefabs[curPlanet]);
        ico.size = 2.3f;
        curPlanet=(curPlanet+1)%planetPrefabs.Length;
    }

    // Assumes rocket is attached to a tile
    public IEnumerator landRocket()
    {
        switchCam(rocket.cam);

        // Deactivate player for landing animation
        player.gameObject.SetActive(false);

        rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;
       
        // Freeze for landing
        rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Rotate planet so that up is aligned with the rocket.
        rocket.transform.localRotation = Quaternion.identity;
        rocket.transform.localPosition = Vector3.up * 10;
        //ico.transform.rotation = Quaternion.Inverse(rocket.transform.rotation);

        float timestamp = Time.time + 3;
        while (Time.time < timestamp)
        {
            rocket.transform.localPosition = Vector3.Lerp(rocket.transform.localPosition, Vector3.up * (ico.size), Time.deltaTime * 2f);
            yield return new WaitForEndOfFrame();
        }
        
        ico.attachRocket(rocket);

        // Dump player back out
        player.gameObject.SetActive(true);
        timeLeft = 10f;
        ico.triggerRumble(timeLeft);
        Spinner sp = ico.GetComponent<Spinner>();
        ico.GetComponent<Spinner>().attachPlayer(player);

        // Swap cam views back to player
        switchCam(player.cam);
        sing.uiManager.setHUDActive(true);
    }

    public static void switchCam(Camera cam)
    {
        activeCam.gameObject.SetActive(false);
        cam.gameObject.SetActive(true);

        activeCam = cam;
    }
}
