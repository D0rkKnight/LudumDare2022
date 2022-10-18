using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject startMenu;
    public GameObject hud;
    public GameObject timer;
    public GameObject fuelText;


    public void setTimerTime(float arg)
    {
        if (arg < 0)
            arg = 0;
        timer.GetComponent<TextMeshProUGUI>().SetText("Time left: "+arg.ToString("0s"));
    }
    public void setFuelText(float arg)
    {
        fuelText.GetComponent<TextMeshProUGUI>().SetText("Fuel: " + arg.ToString()+"/10");
    }

    public void setStartMenuActive(bool arg)
    {
        startMenu.SetActive(arg);
    }

    public void setHUDActive(bool arg)
    {
        hud.SetActive(arg);
    }
}
