using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsJugador : StatsPersonaje {

    // Use this for initialization
    void Start() {
        ControladorEquipacion.instancia.onEquipmentChanged += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipacion nuevoItem, Equipacion itemAnterior)
    {
        if(nuevoItem != null)
        {
            armadura.AnadirModificador(nuevoItem.modificadorArmadura);
            dano.AnadirModificador(nuevoItem.modificadorDano);
        }
        if(itemAnterior != null)
        {
            armadura.eliminarModificador(itemAnterior.modificadorArmadura);
            dano.eliminarModificador(itemAnterior.modificadorDano);
        }
    }

    public override void Muere()
    {
        base.Muere();
        ManejadorPersonaje.instancia.matarPersonaje();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "ArmaEnemigo")
        {
            StatsEnemigo stats = collision.gameObject.GetComponent<CogerItem>().Personaje.GetComponent<StatsEnemigo>();
            recibirDano(stats.dano.getValor());
        }
    }
}
