using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{
    public GameObject pausePanel;
    //public GameObject gamepad;
    [SerializeField]
    controladorCamaraTercera ccamtercera;

    void Start()
    {
        pausePanel.SetActive(false);
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        //gamepad.SetActive(false);
#else
        //gamepad.SetActive(true);
#endif
    }
    void Update()
    {
        // Activar y desactivar el menú de pausa con ESCAPE
        if (Input.GetKeyDown(KeyCode.Escape) && !pausePanel.activeInHierarchy)
        {
            PauseGame();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && pausePanel.activeInHierarchy)
        {
            ContinueGame();
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        ccamtercera.enabled = false;
    }
    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        ccamtercera.enabled = true;
    }

    public void MenuPrincipal()
    {
        SceneManager.LoadScene(0);
    }
}
