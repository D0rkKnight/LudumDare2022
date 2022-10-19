using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject pauseMenu;
    public GameObject hud;
    public GameObject timer;
    public GameObject fuelText;

    //private float maxTime=10.0f;
    private float neededFuel = 10.0f;
    private float fuel = 0.0f;


    /* transparency TODO */
    public void setTransparency(float arg)
    {

    }
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
    private void updateText()
    {
        fuelText.GetComponent<TextMeshProUGUI>().SetText("Fuel: " + fuel.ToString() + "/" + neededFuel.ToString());
    }


    public void setStartMenuActive(bool arg)
    {
        startMenu.SetActive(arg);
    }

    public void setHUDActive(bool arg)
    {
        hud.SetActive(arg);
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
    }
    public void Unpause()
    {
        pauseMenu.SetActive(false);
    }
}
