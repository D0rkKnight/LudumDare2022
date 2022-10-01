using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTest : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 surfVelo;
    public bool grounded;
    public float jumpHeight;
    public float gravityValue = 9.81f;
    public float playerSpeed = 1f;
    public string Jump;
    public float sensitivity;
    private Vector3 offset;

    public Transform gravSource;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
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

        //controller.Move(Camera.transform.forward * Time.deltaTime * playerSpeed);
        //if (Input.GetKey(KeyCode.Mouse1))

        //.Move(move + Camera.transform.forward * Time.deltaTime * playerSpeed);
        //}
        /*if (Input.GetKey(Jump))
        {
            playerVelocity.y = jumpHeight;
        }*/

        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");

        transform.RotateAround(transform.position, -Vector3.up, -rotateHorizontal * sensitivity);

        Vector3 contMove = Camera.main.transform.up * surfVelo.y + Camera.main.transform.right * surfVelo.x;

        float fall = gravityValue;
        if (controller.isGrounded)
            fall = 0;

        Debug.Log(contMove);
        Vector3 dirToGround = -(transform.position - gravSource.position).normalized;
        contMove += dirToGround * fall;

        controller.Move(contMove * Time.fixedDeltaTime);



        // Point player up
        transform.LookAt(Camera.main.transform.up, -Camera.main.transform.forward);
    }

}
