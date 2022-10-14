using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class IcoSpawnerSceneManager : MonoBehaviour
{

    //TODO:
    //Press Q and E to move planets
    //Press Space to explode planet
    //Press WASD to interact w/ the spinner
    

    [SerializeField]
    private GameObject []planetPrefabs;

    [SerializeField]
    private int nRows=5;

    //distance planets should be placed from each other
    [SerializeField]
    public float dx = 6.0f;

    private int activei = 0;
    private int activej = 0;
    
    
    private GameObject[] planets;

    public void instantiateObjects()
    {
        planets = new GameObject[nRows*planetPrefabs.Length];
        float x0 = -(planetPrefabs.Length-1) * dx/2.0f;
        for(int i=0;i<planetPrefabs.Length;i++)
        {
            for(int j = 0; j < nRows; j++)
            {
                planets[i * nRows + j] = Instantiate(planetPrefabs[i], transform);
                planets[i * nRows + j].transform.localPosition = new Vector3(x0+i * dx,0,j*dx);
            //Instantiate random tiles and rotate them into place.
            /*GameObject obj = Instantiate(prefabList[Random.Range(0, prefabList.Length)], transform);
                obj.transform.localPosition = (Positions[i]) * size;
                obj.transform.rotation = Quaternion.AngleAxis(Angles[i], Axes[i]);*/
            }
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        instantiateObjects();
        activei=Mathf.FloorToInt(planetPrefabs.Length / 2);
        activej = 0;
        //Destroy(planets[activei * nRows + activej]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
