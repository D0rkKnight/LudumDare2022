using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> objInRange;
    public GameObject Ball;
    public static Player player;
    public static Rocket rocket;
<<<<<<< HEAD
    bool liftOff = false;
    public float fuel = 0;
=======
    public static IcoSpawnwPrefabList ico;
    bool rocketBoarded = false;

    public IcoSpawnwPrefabList icoPrefab;
    public Rocket rocketPrefab;

    public static float timeLeft = 10f;
    public static bool gameOver = false;
>>>>>>> 0caa860fc5ab36b4618f0fc2bd7f86d55700c5a7

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        objInRange = new List<GameObject>();

        // Deactivate player for landing animation
        player.gameObject.SetActive(false);

        spawnPlanet();

        // Spawn rocket
        Rocket rok = Instantiate(rocketPrefab, ico.tiles[0].transform);
        rok.transform.localPosition = Vector3.up * 0.5f;
        rok.transform.localRotation = Quaternion.identity;
        rok.cam.gameObject.SetActive(true);

        StartCoroutine(landRocket());
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
                if (Input.GetKeyDown(KeyCode.F))
                {
                    Camera.main.gameObject.SetActive(false);
                    rocket.cam.gameObject.SetActive(true);

                    ico.GetComponent<Spinner>().enabled = false;
                    // Get into it!
                    player.gameObject.SetActive(false);
                    StartCoroutine(LiftOff());
                }
            }
        }
<<<<<<< HEAD
        if (fuel == 10)
        {
            Debug.Log("You Win!");
        }
        if (liftOff == true)
        {
            rocket.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Debug.Log("TrueLiftOff");
            rocket.gameObject.GetComponent<Rigidbody>().AddForce(rocket.cam.transform.up*10000);
        }
        Debug.Log(rocket.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
=======

        // Update timer
        if (timeLeft > 0 && timeLeft - Time.deltaTime <= 0)
            StartCoroutine(explode());

        timeLeft -= Time.deltaTime;
>>>>>>> 0caa860fc5ab36b4618f0fc2bd7f86d55700c5a7
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

        // Deparent rocket
        rocket.transform.parent = null;
        StartCoroutine(rocket.boostOff());
    }

    public IEnumerator explode()
    {
        ico.explode();

        if (!rocketBoarded)
        {
            gameOver = true;

            // Toss the player
            player.enabled = false;

            GameObject pObj = GameManager.player.gameObject;
            Rigidbody prb = pObj.AddComponent<Rigidbody>();
            prb.useGravity = false;
            prb.AddForce(Vector3.up * 100f);

            Destroy(pObj, 3);
            Camera.main.transform.parent = null; // Unlink the camera too
            player = null; // Unlink the player
        }
        else
        {
            // Load in a new planet
            yield return new WaitForSeconds(3);

            spawnPlanet();
            StartCoroutine(landRocket());
        }
    }

    public void spawnPlanet()
    {
        if (ico != null)
            Destroy(ico);

        Instantiate(icoPrefab);
    }

    // Assumes rocket is attached to a tile
    public IEnumerator landRocket()
    {
        rocket.GetComponent<Rigidbody>().velocity = Vector3.zero;

        // Ico attached now, go to it
        rocket.transform.parent = ico.tiles[0].transform;

        // Freeze for landing
        rocket.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        // Rotate planet so that up is aligned with the rocket.
        rocket.transform.localRotation = Quaternion.identity;
        rocket.transform.localPosition = Vector3.up * 10;
        ico.transform.rotation = Quaternion.Inverse(rocket.transform.rotation);

        float timestamp = Time.time + 3;
        while (Time.time < timestamp)
        {
            rocket.transform.localPosition = Vector3.Lerp(rocket.transform.localPosition, Vector3.up * 0, Time.deltaTime * 2f);
            yield return new WaitForEndOfFrame();
        }

        // Dump player back out
        player.gameObject.SetActive(true);
        timeLeft = 10f;

        // Swap cam views back to player
        Camera.main.gameObject.SetActive(false);
        player.cam.gameObject.SetActive(true);
    }
}
