using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static List<GameObject> objInRange;

    public static Player player;
    public static Rocket rocket;

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

                    // Get into it!
                    Destroy(player.gameObject);
                }
            }
        }
    }
}
