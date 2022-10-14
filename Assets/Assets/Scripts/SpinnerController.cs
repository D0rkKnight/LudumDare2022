using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerController : MonoBehaviour
{
    private Vector3 surfVelo;
    public float playerSpeed = 100.0f;

    void FixedUpdate()
    {
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        surfVelo = move * playerSpeed;
        GameObject core = gameObject;
        transform.RotateAround(core.transform.position, Vector3.right, -move.y * Time.deltaTime * playerSpeed);
        transform.RotateAround(core.transform.position, Vector3.forward, move.x * Time.deltaTime * playerSpeed);

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
