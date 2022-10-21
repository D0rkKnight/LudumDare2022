using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public GameObject hardModeButton;
    public GameObject hardModeDiamond;
    public GameObject normalModeDiamond;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MenuPlayGame()
    {
        GameManager.sing.startGame();
    }
    public void MenuPlayHardMode()
    {
        GameManager.sing.startGameHardmode();
    }
    public void MenuDebugScene()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void QuitGame()
    {
	    Debug.Log("Quit");
	    Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        switch (GameManager.modeUnlocked)
        {
            case 0:
                hardModeButton.SetActive(false);
                normalModeDiamond.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.08f);
                hardModeDiamond.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.08f);
                break;
            case 1:
                hardModeButton.SetActive(true);
                normalModeDiamond.GetComponent<Image>().color = new Color(1f, 1f, 1f,1f);
                hardModeDiamond.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.08f);
                break;
            case 2:
                hardModeButton.SetActive(true);
                normalModeDiamond.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                hardModeDiamond.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
                break;
        }
    }
}
