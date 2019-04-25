using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsEnemigo : StatsPersonaje {

    float tiempoEnfriamientoDano = 3f;
    float tiempoSinDano = 0f;

    public override void Muere()
    {
        base.Muere();

        //añadir efectos/animaciones

        //Destroy(gameObject);

    }

    public void Update()
    {
        tiempoSinDano += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "ArmaProtagonista")
        {
            controladorPersonaje protagonista = collider.gameObject.GetComponent<CogerItem>().Personaje.GetComponent<controladorPersonaje>();

            AnimatorClipInfo[] clips = protagonista.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0);

            if (protagonista.enAtaque && (clips[0].clip.name == "golpeEspada") && tiempoSinDano > tiempoEnfriamientoDano)
            {
                StatsJugador stats = collider.gameObject.GetComponent<CogerItem>().Personaje.GetComponent<StatsJugador>();
                recibirDano(stats.dano.getValor());
                tiempoSinDano = 0f;
            }

            
        }
    }

}
