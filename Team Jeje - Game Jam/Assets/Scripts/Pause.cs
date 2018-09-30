using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public bool m_paused = false;
    public GameObject m_ui_menu;

    // Use this for initialization
    void Start ()
    {
        PauseGame();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!m_paused)
            {
                PauseGame();
            }
            else if (m_paused)
            {
                ContinueGame();
            }
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        m_paused = true;
        m_ui_menu.SetActive(true);
        //Disable scripts that still work while timescale is set to 0
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        m_paused = false;
        m_ui_menu.SetActive(false);
        //enable the scripts again
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void SoundToggle()
    {

    }
}
