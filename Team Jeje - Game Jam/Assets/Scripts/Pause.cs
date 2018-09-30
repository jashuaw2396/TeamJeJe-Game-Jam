using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    public bool m_paused = false;

    // Use this for initialization
    void Start ()
    {
        
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
        //Disable scripts that still work while timescale is set to 0
    }
    private void ContinueGame()
    {
        Time.timeScale = 1;
        m_paused = false;
        //enable the scripts again
    }
}
