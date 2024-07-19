using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public Button button;
    public Text text;
    public bool isPaused;
    // Start is called before the first frame update
   void Start()
    {
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key pressed");
            Time.timeScale = 0;
            button.gameObject.SetActive(true);
            text.gameObject.SetActive(true);
        }

    }
    public void ContinuedGame()
    {
        Time.timeScale = 1;
        button.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
