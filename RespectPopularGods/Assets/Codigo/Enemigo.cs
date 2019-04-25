using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StatsPersonaje))]
public class Enemigo : Interactuable {


    ManejadorPersonaje manejadorPersonaje;
    StatsPersonaje misStats;

    private void Start()
    {
        manejadorPersonaje = ManejadorPersonaje.instancia;
        misStats = GetComponent<StatsPersonaje>();
    }

    public override void interactuar()
    {
        /*
        base.interactuar();
        CombatePersonaje combateJugador = manejadorPersonaje.jugador.GetComponent<CombatePersonaje>();
        
        if(combateJugador != null)
        {
            combateJugador.atacar(misStats);
        }
        */
    }
}
