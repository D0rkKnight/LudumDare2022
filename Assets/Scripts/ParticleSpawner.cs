using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GameObject ball;
    //Mesh spheremesh = GameObject.Find("Sphere").GetComponent<MeshFilter>().mesh;
    //Vector3[] verts = spheremesh.vertices;
    // Start is called before the first frame update
    void Start()
    {
        var matrix = GameObject.Find("Particles").transform.localToWorldMatrix;
        Mesh spheremesh = GameObject.Find("Particles").GetComponent<MeshFilter>().mesh;
        //GameObject spawner = GameObject.Find("Spawner");

        Vector3[] verts = spheremesh.vertices;
        for (int i = 0; i < verts.Length; i+=5)
        {
            int j = Random.Range(-5,5);
            if (j > 0)
            {
                //var Go = GameObject.Find("Particles");
                var spawn = Instantiate(ball, transform);
                spawn.transform.localPosition = matrix.MultiplyPoint3x4(verts[i]);
                spawn.transform.rotation = transform.localRotation;
            }
            //var Go = GameObject.Find("Sphere");
            //var spawn = Instantiate(ball, matrix.MultiplyPoint3x4(verts[i]),transform.rotation);
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
