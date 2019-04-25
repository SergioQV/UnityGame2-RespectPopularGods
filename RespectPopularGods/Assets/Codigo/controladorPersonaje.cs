using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class controladorPersonaje : MonoBehaviour
{

    public Camera camaraPrincipal;
    public float velocidad;
    float velocidadDeGiro = 300.0f;
    public float tiempoSinAtacar = 0;

    public bool corriendo = false;
    public bool sprint = false;
    public bool enAtaque = false;
    public bool protegiendo = false;
    Animator anim;


    int tiempoSprint = 1;
    int tiempoNoSprint = 10;
    int tiempoCorriendo = 10;
    int tiempoNoCorriendo = 1;

    // Use this for initialization
    void Start()
    {
        //transform.rotation = new Quaternion(transform.rotation.x, camaraPrincipal.transform.rotation.y, transform.rotation.z);
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }


        float avance = Input.GetAxis("Vertical");
        float giro = Input.GetAxis("Horizontal");

        float vel;


        if (Input.GetKeyDown(KeyCode.Q) && !enAtaque && avance == 0)
        {
            anim.SetTrigger("EstatusAtaque");
            enAtaque = true;
            corriendo = false;
            sprint = false;
        }

        if (avance >= 0)
        {
            vel = velocidad;


            if (Input.GetKeyDown(KeyCode.C) && !enAtaque)
            {
                corriendo = !corriendo;
                if (corriendo)
                {
                    vel += 1;
                    tiempoCorriendo = 10;
                }
                else
                {
                    tiempoNoCorriendo = 1;
                }
            }

            if (!corriendo && avance > 0)
            {
                if (!enAtaque)
                {
                    avance -= 0.05f * tiempoNoCorriendo;
                }

                if (tiempoNoCorriendo < 10)
                {
                    tiempoNoCorriendo++;
                }
                vel /= 2;

                if (enAtaque)
                {
                    vel /= 2;
                }

            }
            else if (corriendo && avance > 0)
            {
                avance -= 0.05f * tiempoCorriendo;

                if (tiempoCorriendo > 0)
                {
                    tiempoCorriendo--;
                }
            }

            if (Input.GetKeyDown(KeyCode.LeftShift) && !enAtaque)
            {
                sprint = true;
                tiempoNoSprint = 10;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                sprint = false;
                tiempoSprint = 1;
            }

            if (sprint && avance > 0)
            {
                avance += 0.05f * tiempoSprint;
                if (tiempoSprint < 10)
                {
                    tiempoSprint++;
                }
                vel += 1;
            }
            else if (!sprint && avance > 0)
            {
                avance += 0.05f * tiempoNoSprint;
                if (tiempoNoSprint > 0)
                {
                    tiempoNoSprint--;
                }
            }

            if (giro != 0)
            {
                transform.Rotate(0, giro * velocidadDeGiro * Time.deltaTime, 0);
            }
        }


        else
        {
            vel = velocidad / 4;
        }



        if (avance != 0 && !protegiendo)
        {
            mirarDireccionCamara();
            transform.Translate(0, 0, avance * vel * Time.deltaTime);
        }
        if (!enAtaque)
        {
            anim.SetFloat("Velocidad", avance);

            if (avance == 0f && Input.GetButtonDown("Fire1") && ControladorEquipacion.instancia.equipacionActual[0] != null)
            {
                anim.SetTrigger("AtaqueEspada");
                enAtaque = true;
                tiempoSinAtacar = 0;
            }
        }

        else
        {
            anim.SetFloat("VelocidadAlerta", avance);

            if (avance == 0f && Input.GetButtonDown("Fire1") && ControladorEquipacion.instancia.equipacionActual[0] != null)
            {
                anim.SetTrigger("AtaqueEspada");
                tiempoSinAtacar = 0;
            }
            else if(avance == 0 && Input.GetKeyDown(KeyCode.LeftShift) && !protegiendo && ControladorEquipacion.instancia.equipacionActual[1] != null)
            {
                anim.SetTrigger("Escudarse");
                protegiendo = true;
                tiempoSinAtacar = 0;
            }

            if (protegiendo)
            {
                tiempoSinAtacar = 0;

                if (Input.GetKeyUp(KeyCode.LeftShift))
                {
                    anim.SetTrigger("DejarDeEscudarse");
                    protegiendo = false;
                }
            }
            

            tiempoSinAtacar += Time.deltaTime;

            if (tiempoSinAtacar >= 4f)
            {
                tiempoSinAtacar = 0;
                enAtaque = false;
                anim.SetTrigger("DejaAtaque");
            }

        }

    }

    public void mirarDireccionCamara()
    {
        Vector3 direccion = (camaraPrincipal.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(-direccion.x, 0, -direccion.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

}
