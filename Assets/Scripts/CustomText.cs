using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class CustomText : MonoBehaviour
{
    public TMP_Text txt;


    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        setText();
    }

    public abstract void setText();
}