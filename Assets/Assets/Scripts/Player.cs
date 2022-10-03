using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    // Cam pointed at the player
    public Transform camFocus;
    public Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.player = this;
    }

    // Update is called once per frame
    void Update()
    {
        float hor = Input.GetAxis("Mouse X") * Time.deltaTime * 100;
        transform.RotateAround(transform.position, Vector3.up, hor);

        float ver = Input.GetAxis("Mouse Y") * Time.deltaTime * 100;

        // Rot lock hacks
        Vector3 euler = camFocus.localRotation.eulerAngles;
        euler.x -= ver;
        euler.x = Mathf.Clamp(euler.x, 0, 80);
        camFocus.localRotation = Quaternion.Euler(euler);
    }

    private void OnTriggerEnter(Collider other)
    {
        GameManager.objInRange.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        GameManager.objInRange.Remove(other.gameObject);
    }
}
