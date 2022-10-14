using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerTestScript : MonoBehaviour
{
    public GameObject beachObject;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ExplodePlanet());
    }

    private IEnumerator ExplodePlanet()
    {
        yield return new WaitForSeconds(3.0f);
        beachObject.GetComponent<PlanetScript>().explode();
        yield return new WaitForSeconds(3.0f);
        Destroy(beachObject);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
