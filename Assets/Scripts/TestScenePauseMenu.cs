using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestScenePauseMenu : MonoBehaviour
{
    public void OpenMainMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainScene");
    }
}
