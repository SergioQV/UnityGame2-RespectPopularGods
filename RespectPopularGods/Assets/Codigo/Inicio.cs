using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Inicio : MonoBehaviour
{
    public GameObject inicioPanel;
    //public GameObject gamepad;
    [SerializeField]
    controladorCamaraTercera ccamtercera;

    void Start()
    {
        ccamtercera.enabled = false;
        Time.timeScale = 0;
        inicioPanel.SetActive(true);
#if UNITY_STANDALONE || UNITY_WEBPLAYER
        //gamepad.SetActive(false);
#else
        //gamepad.SetActive(true);
#endif
    }

    public void PlayGame()
    {
        Time.timeScale = 1;
        inicioPanel.SetActive(false);
        ccamtercera.enabled = true;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Reiniciar()
    {
        SceneManager.LoadScene(0);
    }
}
