using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScenePlayerScript : MonoBehaviour
{
    // Cam pointed at the player
    public Transform camFocus;
    public Camera cam;

    void FixedUpdate()
    {
        float hor = Input.GetAxis("Mouse X");
        transform.RotateAround(transform.position, Vector3.up, hor * Time.deltaTime * 100);

        float ver = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;
        // Rot lock hacks
        Vector3 euler = camFocus.localRotation.eulerAngles;
        euler.x -= ver;
        euler.x = Mathf.Clamp(euler.x, 0, 80);
        camFocus.localRotation = Quaternion.Euler(euler);
    }
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
