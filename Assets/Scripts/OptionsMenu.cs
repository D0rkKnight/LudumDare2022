using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionsMenu : MonoBehaviour
{
    public static float volume = 1.0f;
    public static float duration = 10.0f;

    [SerializeField]
    private GameObject textToUpdate;

    [SerializeField]
    private GameObject volumeSlider;

    [SerializeField]
    private GameObject timeSlider;

    [SerializeField]
    private GameObject MainMusic;

    public void setVolume(float arg)
    {
        volume = Mathf.Clamp(arg,0,1);
        AudioListener.volume = volume;
        //MainMusic.GetComponent<AudioSource>().volume = volume;
    }
    public void setDuration(float arg)
    {
        duration = Mathf.Clamp(arg,8, 25);
        if (textToUpdate)
        {
            textToUpdate.GetComponent<TextMeshProUGUI>().SetText(arg.ToString("0s"));
        }
    }
}
