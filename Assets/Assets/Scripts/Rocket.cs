using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    public Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        GameManager.rocket = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator boostOff()
    {
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

        float timestamp = Time.time + 2;
        while (Time.time < timestamp)
        {
            GetComponent<Rigidbody>().AddForce(transform.up * Time.deltaTime * 40f);
            yield return new WaitForFixedUpdate();
        }
    }
}
