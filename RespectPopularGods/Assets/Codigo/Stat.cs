using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat {

    [SerializeField]
    private int valorBase;

    private List<int> modificadores = new List<int>();

    public int getValor()
    {
        int valorFinal = valorBase;
        modificadores.ForEach(x => valorFinal += x);
        return valorFinal;
    }

    public void AnadirModificador(int modificador)
    {
        if(modificador != 0)
        {
            modificadores.Add(modificador);
        }
    }

    public void eliminarModificador(int modificador)
    {
        if(modificador != 0)
        {
            modificadores.Remove(modificador); 
        }
    }
}
