using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Clears game env and reloads everything
public class RestartButton : MonoBehaviour
{
    public Button btn;

    // Start is called before the first frame update
    void Start()
    {
        btn.onClick.AddListener(() =>
        {
            GameManager.startGame();
        });
    }

    // Update is called once per frame
    void Update()
    {
        btn.gameObject.SetActive(GameManager.restartReady);
    }
}