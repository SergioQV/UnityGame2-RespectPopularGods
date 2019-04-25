using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactuable : MonoBehaviour
{

    public float radio = 3f;
    public bool enRadio = false;
    public Transform Personaje;
    

    private void Update()
    {
        float distancia = Vector3.Distance(Personaje.position, transform.position);

        if(distancia <= radio)
        {
            enRadio = true;

            if (Input.GetKeyDown(KeyCode.E))
            {
                interactuar();
            }

        }
        else
        {
            enRadio = false;
        }
    }

    public virtual void interactuar()
    {

    }

}
