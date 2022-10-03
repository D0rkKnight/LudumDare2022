using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    private Vector3 surfVelo;
    public bool grounded;
    public float jumpHeight;
    public float gravityValue = 9.81f;
    public float playerSpeed = 1f;
    public float sensitivity;
    //private Transform spawner = GameObject.Find("IcoSpawner").GetComponent<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        //Transform spawner = GameObject.Find("IcoSpawner").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey("left shift"))
        {
            playerSpeed = 200f;
        }

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        surfVelo = move * playerSpeed;
        bool spinning = true;

        if (spinning && GameManager.player != null && GameManager.ico != null)
        {
            GameObject Core = GameManager.ico.gameObject;
            transform.RotateAround(Core.transform.position, GameManager.player.transform.right, -move.y * Time.deltaTime * playerSpeed);
            transform.RotateAround(Core.transform.position, GameManager.player.transform.forward, move.x * Time.deltaTime * playerSpeed);
        }
        
    }
    
}
