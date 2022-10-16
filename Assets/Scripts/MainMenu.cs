using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public void MenuPlayGame()
    {
        GameManager.startGame();
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
        
    }
}
