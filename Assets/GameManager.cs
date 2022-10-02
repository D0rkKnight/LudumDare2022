using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> objInRange;

    public static Player player;
    public static Rocket rocket;
    bool liftOff = false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        objInRange = new List<GameObject>();
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
                Debug.Log("Scanning valid object");


                // Rocket boarding
                if (Input.GetKeyDown(KeyCode.F))
                {
                    rocket.cam.gameObject.SetActive(true);
                    
                    GameObject.Find("Spawner").GetComponent<Spinner>().enabled = false;
                    // Get into it!
                    player.gameObject.SetActive(false);
                    StartCoroutine(LiftOff());
                    
                    //Destroy(player.gameObject);
                }
            }
        }
        if (liftOff == true)
        {
            rocket.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            Debug.Log("TrueLiftOff");
            rocket.gameObject.GetComponent<Rigidbody>().AddForce(rocket.cam.transform.up*100);
        }
        Debug.Log(rocket.gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }
    IEnumerator LiftOff()
    {
        for (int i = 0; i < 11; i++)
        {
            yield return new WaitForSeconds(1);
            Debug.Log(10 - i);
        }
        liftOff = true;
        Debug.Log("LiftOff!");
    }
}
