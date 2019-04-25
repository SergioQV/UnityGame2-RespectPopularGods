using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class ManejadorPersonaje : MonoBehaviour {

    #region Singleton

    public static ManejadorPersonaje instancia;

    private void Awake()
    {
        instancia = this;
    }

    #endregion


    public GameObject jugador;


    public void matarPersonaje()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
