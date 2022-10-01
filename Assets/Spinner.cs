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

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKey("left shift"))
        {
            playerSpeed = 20f;
        }
        else
        {
            playerSpeed = 8f;
        }

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        surfVelo = move * playerSpeed;

        transform.rotation *= Quaternion.Euler(move.x, move.y, 0);
    }
}
