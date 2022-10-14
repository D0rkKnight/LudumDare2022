using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerText : CustomText
{
    public override void setText()
    {
        float display = Mathf.Max(0, GameManager.timeLeft);
        txt.text = display.ToString("F3");

        txt.enabled = GameManager.inPlay;
    }
}
