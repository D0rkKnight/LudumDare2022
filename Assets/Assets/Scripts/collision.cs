using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collision : MonoBehaviour
{
    
    public GameObject gm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        float fuel = gm.GetComponent<GameManager>().fuel;
        if (collision.gameObject.tag == "Player")
        {

            Destroy(gameObject,1);
            fuel += 1;
            Debug.Log(fuel);
        }
    }
}
