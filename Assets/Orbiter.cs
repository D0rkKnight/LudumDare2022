using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter : MonoBehaviour
{
    private Camera cam;
    public Transform center;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delt = target.position - center.position;

        cam.transform.position = target.position + delt.normalized * 3f;
        cam.transform.LookAt(center, target.forward);
    }
}
