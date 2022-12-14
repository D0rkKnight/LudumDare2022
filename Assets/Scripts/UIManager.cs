using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public GameObject hud;
    public GameObject timer;
    public GameObject fuelText;
    public GameObject hudPlanetText;
    public GameObject gameOverPlanetText;
    public StartMenuRandomBackground smRandomImage;
    public GameObject fueledUpText;
    public GameObject pressFText;
    public GameObject victoryMenu;
    public GameObject hardModeMenu;

    //private float maxTime=10.0f;
    private float neededFuel = 10.0f;
    private float fuel = 0.0f;
    private int thisPlanet = 1;


    /* transparency TODO */
    /*public void setTransparency(float arg)
    {

    }*/
    public void setTimerTime(float arg)
    {
        if (arg < 0)
            arg = 0;
        timer.GetComponent<TextMeshProUGUI>().SetText("Time left: "+arg.ToString("0s"));
    }

    public void setFuel(float arg)
    {
        this.fuel = arg;
        updateText();
    }

    public void setNeededFuel(float arg)
    {
        neededFuel = arg;
        updateText();
    }

    public void setPlanetText(int planet)
    {
        if(hudPlanetText)
            hudPlanetText.GetComponent<TextMeshProUGUI>().SetText("Planet " + planet.ToString() + "/5");
        thisPlanet = planet;
    }
    public void setGameOverPlanetText(int planet)
    {
       if(gameOverPlanetText)
            gameOverPlanetText.GetComponent<TextMeshProUGUI>().SetText("You made it to "+planet.ToString()+"/5 planets.");
        thisPlanet = planet;
    }

    private void updateText()
    {
        fuelText.GetComponent<TextMeshProUGUI>().SetText("Fuel: " + fuel.ToString() + "/" + neededFuel.ToString());
    }


    public void setStartMenuActive(bool arg)
    {
        startMenu.SetActive(arg);
        smRandomImage.randomize();
    }

    public void setHUDActive(bool arg)
    {
        hud.SetActive(arg);
    }

    public void SetFueledActive(bool arg)
    {
        fueledUpText.SetActive(arg);
    }
    public void SetRocketFActive(bool arg)
    {
        pressFText.SetActive(arg);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
    public void Unpause()
    {
        pauseMenu.SetActive(false);
    }
    public void SetGameOverActive(bool arg)
    {
        gameOverMenu.SetActive(arg);
        setGameOverPlanetText(thisPlanet);
    }
    public void SetVictoryActive(bool arg)
    {
        victoryMenu.SetActive(arg);
    }
    public void SetHardmodeVictoryActive(bool arg)
    {
        hardModeMenu.SetActive(arg);
    }
    }
