using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerTest : MonoBehaviour
{
    public CharacterController controller;
    private Vector3 playerVelocity;
    public bool groundedPlayer;
    public float jumpHeight;
    public float gravityValue = -9.81f;
    public float playerSpeed = 10f;
    public string Jump;
    public GameObject Camera;
    public float sensitivity;
    private Vector3 offset;
    public GameObject Player;
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));// new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        //Vector3 move2 = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        //controller.Move((move * Time.deltaTime * playerSpeed));
        if (Input.GetAxis("Vertical") != 0)
        {
            controller.Move(Camera.transform.forward * Time.deltaTime * playerSpeed * Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            controller.Move(Camera.transform.right * Time.deltaTime * playerSpeed * Input.GetAxis("Horizontal"));
        }
        if (Input.GetKey("left shift"))
        {
            playerSpeed = 30f;
        }
        else
        {
            playerSpeed = 10f;
        }
        //controller.Move(Camera.transform.forward * Time.deltaTime * playerSpeed);
        //if (Input.GetKey(KeyCode.Mouse1))

        //.Move(move + Camera.transform.forward * Time.deltaTime * playerSpeed);
        //}
        if (Input.GetKey(Jump))
        {
            playerVelocity.y = jumpHeight;
        }
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y");
        Player.transform.RotateAround(Player.transform.position, -Vector3.up, -rotateHorizontal * sensitivity);
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        //controller.transform.forward = Player.transform.forward;
    }

}
