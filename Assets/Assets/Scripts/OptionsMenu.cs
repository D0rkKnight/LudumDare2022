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
    public GameObject volumeSlider;
    public GameObject timeSlider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setVolume(float arg)
    {
        volume = Mathf.Clamp(arg,0,1);
    }
    public void setDuration(float arg)
    {
        duration = Mathf.Clamp(arg,8, 25);
        if (textToUpdate)
        {
            textToUpdate.GetComponent<TextMeshProUGUI>().SetText(arg.ToString("0s"));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
