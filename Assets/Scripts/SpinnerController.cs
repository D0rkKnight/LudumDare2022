using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    private Vector3 surfVelo;
    public float playerSpeed = 100.0f;
    //private Transform spawner = GameObject.Find("IcoSpawner").GetComponent<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        //Transform spawner = GameObject.Find("IcoSpawner").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        surfVelo = move * playerSpeed;
        GameObject core = gameObject;
        transform.RotateAround(core.transform.position, new Vector3(1,0,0), -move.y * Time.deltaTime * playerSpeed);
        transform.RotateAround(core.transform.position, new Vector3(0,0,1), move.x * Time.deltaTime * playerSpeed);
    }
}
