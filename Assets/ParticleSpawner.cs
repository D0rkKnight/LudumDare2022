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
        var matrix = transform.localToWorldMatrix;
        Mesh spheremesh = GameObject.Find("Sphere").GetComponent<MeshFilter>().mesh;
<<<<<<< HEAD
        GameObject spawner = GameObject.Find("Spawner");
=======

>>>>>>> 0d8cb1ef35b539d326878258c21fe825b1577ebb
        Vector3[] verts = spheremesh.vertices;
        for (int i = 0; i < verts.Length; i+=5)
        {
            int j = Random.Range(-5,5);
            if (j > 0)
            {
                var Go = GameObject.Find("Sphere");
                var spawn = Instantiate(ball,matrix.MultiplyPoint3x4(verts[i]), spawner.transform.rotation);
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
