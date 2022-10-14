using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
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
    private GameObject pauseMenu;

    [SerializeField]
    private GameObject planetHolder;

    [SerializeField]
    private int nRows=5;

    //distance planets should be placed from each other
    [SerializeField]
    public float dx = 6.0f;

    private int activei = 0;
    private int activej = 0;
    
    private GameObject[] planets;


    private Vector3 planetPosition(int i,int j)
    {
        float x0 = -(planetPrefabs.Length - 1) * dx / 2.0f;
        return new Vector3(x0 + i * dx, 0, j * dx);
    }

    public void instantiateObjects()
    {
        planets = new GameObject[nRows*planetPrefabs.Length];
        for (int i=0;i<planetPrefabs.Length;i++)
        {
            for(int j = 0; j < nRows; j++)
            {
                planets[i * nRows + j] = Instantiate(planetPrefabs[i], planetHolder.transform);
                planets[i * nRows + j].transform.localPosition = planetPosition(i, j);
                planets[i * nRows + j].GetComponent<PlanetScript>().deactivate();
            }
        }
    }


    
    // Start is called before the first frame update
    void Start()
    {
        instantiateObjects();
        activei=Mathf.FloorToInt(planetPrefabs.Length / 2);
        activej = 0;
        //planets[activei * nRows + activej].AddComponent<SpinnerController>();
        planets[activei * nRows + activej].GetComponent<PlanetScript>().activate();
    }


    // Update is called once per frame
    void Update()
    {
        planetHolder.transform.position=Vector3.Lerp(planetHolder.transform.position, -planetPosition(activei,activej), 1.0f-Mathf.Exp(-Time.deltaTime * 5f));
        if(Input.GetKeyDown(KeyCode.Q)){
            prevPlanet();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            nextPlanet();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(planets[activei * nRows + activej])
            {
                planets[activei * nRows + activej].GetComponent<PlanetScript>().triggerRumble(5.0f);
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Resume();
            else
                Pause();
        }

    }

    private bool paused = false;
    public void Pause()
    {
        paused = true;
        Time.timeScale = 0.0f;
        if (pauseMenu)
        {
            pauseMenu.SetActive(true);
        }
    }
    public void Resume()
    {
        paused = false;
        Time.timeScale = 1.0f;
        if (pauseMenu)
        {
            pauseMenu.SetActive(false);
        }
    }

    private void nextPlanet()
    {
        planets[activei * nRows + activej].GetComponent<PlanetScript>().deactivate();
        activej += 1;
        if (activej >= nRows)
        {
            activej = 0;
            activei = (activei + 1) % planetPrefabs.Length;
        }
        planets[activei * nRows + activej].GetComponent<PlanetScript>().activate();
    }
    private void prevPlanet()
    {
        planets[activei * nRows + activej].GetComponent<PlanetScript>().deactivate();
        activej -= 1;
        if (activej < 0)
        {
            activej = nRows-1;
            activei -= 1;
            if (activei < 0)
            {
                activei = planetPrefabs.Length - 1;
            }
        }
        planets[activei * nRows + activej].GetComponent<PlanetScript>().activate();
    }
}
