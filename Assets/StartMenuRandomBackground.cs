using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuRandomBackground : MonoBehaviour
{
    
    public Sprite[] backgrounds;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().sprite = backgrounds[0];
        //Debug.Log(this.gameObject.material.mainTexture = backgrounds[1]);
    }
    public void randomize()
    {
        GetComponent<Image>().sprite = backgrounds[(int)Random.Range(0.0f,backgrounds.Length)];

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
