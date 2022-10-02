using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.rocket = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
