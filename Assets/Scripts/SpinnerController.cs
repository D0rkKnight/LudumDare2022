using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    private Vector3 surfVelo;
    public float playerSpeed = 100.0f;

    private GameObject playerRef=null;

    public void attachPlayer(GameObject playerRef)
    {
        this.playerRef = playerRef;
    }
    public void detachPlayer()
    {
        playerRef= null;
    }

    void FixedUpdate()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        surfVelo = move * playerSpeed;
        GameObject core = gameObject;
        Vector3 d1 = Vector3.right, d2 = Vector3.forward;
        if (playerRef) {
            d1 = playerRef.transform.right;
            d2 = playerRef.transform.forward;
        }
        transform.RotateAround(core.transform.position, d1, -move.y * Time.deltaTime * playerSpeed);
        transform.RotateAround(core.transform.position, d2, move.x * Time.deltaTime * playerSpeed);

        /*
        float hor = Input.GetAxis("Mouse X");
        transform.RotateAround(core.transform.position, Vector3.up, hor * Time.deltaTime * 100);

        float ver = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        // Rot lock hacks
        Vector3 euler = camFocus.localRotation.eulerAngles;
        euler.x -= ver;
        euler.x = Mathf.Clamp(euler.x, 0, 80);
        camFocus.localRotation = Quaternion.Euler(euler);*/
    }
}
