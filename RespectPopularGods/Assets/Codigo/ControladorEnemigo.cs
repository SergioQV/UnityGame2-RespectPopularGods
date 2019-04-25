using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControladorEnemigo : MonoBehaviour
{
    public float RadioVision = 10f;
    public bool corriendo = false;
    public bool conArco = false;
    public bool conEspada = false;
    public bool disparandoFlecha = false;
    public bool usandoEspada = false;
    public bool muerto = false;
    public bool enRadioVision = false;
    public bool huyendo = false;

    public Equipacion[] armasEnemigo = new Equipacion[3];
    
    GameObject[] objetosCreados = new GameObject[2];

    public Vector3[] posicionesDeHuida = new Vector3[6];

    public Transform manoDerechaEnemigo;
    public Transform manoIzquierdaEnemigo;

    // Controlador de sonido
    public AudioSource audioSource;
    public AudioSource audioSource_origen;
    bool activar = true;
    // --------------------------- //

    Vector3 posicionHuida;
    Transform objetivo;
    GameObject jugador;
    NavMeshAgent agente;
    StatsPersonaje misStats;

    Animator anim;

    int[,] ArrayPuntosAntecedentes =
    {
        {0,0,4}, {2,5,8}, {6,10,10},
        {0,0,35}, {15,50,85}, {65,100,100}
    };
    //Cada conjunto de 3 valores representa un triángulo, y cada uno de esos valores es uno de sus picos.
    //En los conjuntos donde se repiten 2 valores es por ser los triángulos de los extremos, y por tanto no hay nada más
    //allá de sus valores mínimos y máximos

    int[,] ArrayReglas =
    {
        {2,5,5},
        {2,5,4},
        {2,5,3},
        {2,4,5},
        {2,4,4},
        {2,4,3},
        {2,3,5},
        {2,3,4},
        {2,3,3},
        {1,5,5},
        {1,5,4},
        {1,5,3},
        {1,4,5},
        {1,4,4},
        {1,4,3},
        {1,3,5},
        {1,3,4},
        {1,3,3},
        {0,5,5},
        {0,5,4},
        {0,5,3},
        {0,4,5},
        {0,4,4},
        {0,4,3},
        {0,3,5},
        {0,3,4},
        {0,3,3}
    };
    //En este array cada fila es una regla y cada columna el índice del antecedente que aplica.

    float[] FC =
    {
        2,
        2,
        0.5f,
        2,
        2,
        2,
        2,
        2,
        2,
        1,
        1,
        0.5f,
        2,
        2,
        0.5f,
        2,
        2,
        1,
        1.5f,
        0.5f,
        0.5f,
        1.5f,
        1.5f,
        0.5f,
        1.5f,
        1.5f,
        0.5f,
    };
    //Array de pesos de reglas

    string[] etiquetasReglas =
    {
        "AcercarseDistanciaMedia",
        "AcercarseDistanciaMedia",
        "DispararFlecha",
        "AcercarseDistanciaMedia",
        "AcercarseDistanciaMedia",
        "AcercarseDistanciaMedia",
        "AcercarseDistanciaMedia",
        "AcercarseDistanciaMedia",
        "AcercarseDistanciaMedia",
        "DispararFlecha",
        "DispararFlecha",
        "AlejarseDistanciaLarga",
        "AcercarseDistanciaCorta",
        "AcercarseDistanciaCorta",
        "AlejarseDistanciaLarga",
        "AcercarseDistanciaCorta",
        "AcercarseDistanciaCorta",
        "DispararFlecha",
        "AtaqueEspada",
        "AlejarseDistanciaMedia",
        "AlejarseDistanciaLarga",
        "AtaqueEspada",
        "AtaqueEspada",
        "AlejarseDistanciaMedia",
        "AtaqueEspada",
        "AtaqueEspada",
        "AlejarseDistanciaMedia"
    };
    //Array que guarda las acciones de cada regla

    // Use this for initialization
    void Start()
    {
        misStats = GetComponent<StatsPersonaje>();
        objetivo = ManejadorPersonaje.instancia.jugador.transform;
        jugador = ManejadorPersonaje.instancia.jugador;
        agente = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // Controlador de sonido
        float distancia = Vector3.Distance(jugador.transform.position, transform.position);
        
        if (distancia <= RadioVision)
        {
            sounTension_on();
            activar = false;
        }
        else if (distancia > RadioVision)
        {
            sounTension_off();
            activar = true;
        }
        // -------------------- //

        if (misStats.SaludActual <= 0 && !muerto)
        {
            anim.SetTrigger("Muere");
            muerto = true;
            agente.isStopped = true;
        }
        else
        {
            if (distancia <= RadioVision)
            {
                enRadioVision = true;
                string decision = decisionFuzzy();

                switch (decision)
                {
                    case "AcercarseDistanciaMedia":
                    case "AcercarseDistanciaCorta":
                        if (objetivo != jugador.transform)
                        {
                            objetivo = jugador.transform;
                        }

                        huyendo = false;
                        agente.SetDestination(objetivo.position);

                        if (!corriendo)
                        {
                            usandoEspada = false;
                            conEspada = false;
                            disparandoFlecha = false;
                            conArco = false;

                            anim.SetTrigger("Correr");
                            corriendo = true;
                        }
                        break;
                        
                    case "AlejarseDistanciaMedia":
                        posicionHuida = devolverHuidaCercana();
                        agente.SetDestination(posicionHuida);


                        huyendo = true;
                        if (!corriendo)
                        {
                            usandoEspada = false;
                            conEspada = false;
                            disparandoFlecha = false;
                            conArco = false;

                            anim.SetTrigger("Correr");
                            corriendo = true;
                        }
                        break;

                    case "AlejarseDistanciaLarga":
                        posicionHuida = devolverHuidaLejana();
                        agente.SetDestination(posicionHuida);

                        huyendo = true;
                        if (!corriendo)
                        {
                            usandoEspada = false;
                            conEspada = false;
                            disparandoFlecha = false;
                            conArco = false;

                            anim.SetTrigger("Correr");
                            corriendo = true;
                        }

                        break;
                    case "DispararFlecha":

                        if (!conArco)
                        {
                            huyendo = false;
                            corriendo = false;
                            usandoEspada = false;
                            conEspada = false;
                            disparandoFlecha = false;

                            desequiparTodo();
                            GameObject arco = Instantiate(armasEnemigo[1].objetoFisico);
                            GameObject flecha = Instantiate(armasEnemigo[2].objetoFisico);

                            arco.transform.parent = manoIzquierdaEnemigo;
                            flecha.transform.parent = manoDerechaEnemigo;

                            arco.transform.localPosition = armasEnemigo[1].posicionAlCoger;
                            flecha.transform.localPosition = armasEnemigo[2].posicionAlCoger;

                            arco.transform.localEulerAngles = armasEnemigo[1].rotacionAlCoger;
                            flecha.transform.localEulerAngles = armasEnemigo[2].rotacionAlCoger;

                            objetosCreados[0] = flecha;
                            objetosCreados[1] = arco;

                            anim.SetTrigger("DecideArco");
                            conArco = true;
                        }

                        if (!disparandoFlecha)
                        {
                            anim.SetTrigger("Ataca");
                            disparandoFlecha = true;
                        }

                        break;

                    case "AtaqueEspada":
                        if (!conEspada)
                        {
                            huyendo = false;
                            disparandoFlecha = false;
                            conArco = false;
                            corriendo = false;
                            usandoEspada = false;

                            desequiparTodo();

                            GameObject espadon = Instantiate(armasEnemigo[0].objetoFisico);
                            espadon.transform.parent = manoDerechaEnemigo;
                            espadon.transform.localPosition = armasEnemigo[0].posicionAlCoger;
                            espadon.transform.localEulerAngles = armasEnemigo[0].rotacionAlCoger;
                            objetosCreados[0] = espadon;

                            anim.SetTrigger("DecideEspada");
                            conEspada = true;
                        }

                        if (!usandoEspada)
                        {
                            anim.SetTrigger("Ataca");
                            usandoEspada = true;
                        }
                        break;
                }

                if(distancia <= agente.stoppingDistance && !muerto && !huyendo)
                {
                    mirarHaciaObjetivo();
                }
            }
        }


    }

    // Controlador de sonido
    void sounTension_on()
    {
        if (activar)
        {
            audioSource_origen.Pause();
            audioSource.Play();
        }
    }

    void sounTension_off()
    {
        if (!activar)
        {
            audioSource.Pause();
            audioSource_origen.Play();
        }
    }
    // ------------------- //

    void mirarHaciaObjetivo()
    {
        Vector3 direccion = (objetivo.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direccion.x, 0, direccion.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, RadioVision);
    }


    string decisionFuzzy()
    {
        float saludPropia = misStats.SaludActual;
        float saludProtagonista = jugador.GetComponent<StatsJugador>().SaludActual; // sol valores enteros pero pueden guardarse en un float
        float distancia = Vector3.Distance(transform.position, jugador.transform.position);

        float[] parametrosDeCalculo = { distancia, saludProtagonista, saludPropia };

        float[] H = new float[27]; //tenemos 27 reglas y nos interesa el mínimo H de cada una de ellas. Primero las inicializamos a un número alto

        for (int i = 0; i < H.Length; i++)
        {
            H[i] = 999.9f;
        }

        for (int i = 0; i < 27; i++) //porque tenemos 27 reglas
        {

            for (int j = 0; j < 3; j++)
            {
                float min;

                int indice = ArrayReglas[i, j];

                if (parametrosDeCalculo[j] < ArrayPuntosAntecedentes[indice, 0])
                {
                    min = 0;
                }
                else if (parametrosDeCalculo[j] >= ArrayPuntosAntecedentes[indice, 0] && parametrosDeCalculo[j] < ArrayPuntosAntecedentes[indice, 1])
                {
                    min = (parametrosDeCalculo[j] - ArrayPuntosAntecedentes[indice, 0]) / (ArrayPuntosAntecedentes[indice, 1] - ArrayPuntosAntecedentes[indice, 0]);
                }
                else if (parametrosDeCalculo[j] == ArrayPuntosAntecedentes[indice, 1])
                {
                    min = 1;
                }
                else if (parametrosDeCalculo[j] > ArrayPuntosAntecedentes[indice, 1] && parametrosDeCalculo[j] < ArrayPuntosAntecedentes[indice, 2])
                {
                    min = (parametrosDeCalculo[j] - ArrayPuntosAntecedentes[indice, 2]) / (ArrayPuntosAntecedentes[indice, 1] - ArrayPuntosAntecedentes[indice, 2]);
                }
                else
                {
                    min = 0;
                }

                H[i] = Mathf.Min(H[i], min);

            }

        }

        float max = 0;
        int indiceMax = 0;

        for (int i = 0; i < H.Length; i++)
        {
            if (H[i] * FC[i] > max)
            {
                max = H[i] * FC[i];
                indiceMax = i;
            }
        }


        return etiquetasReglas[indiceMax];
    }


    void desequipar(int indiceSlot)
    {

        if (objetosCreados[indiceSlot] != null)
        {
            Destroy(objetosCreados[indiceSlot]);
        }
        

    }

    void desequiparTodo()
    {
        for (int i = 0; i < objetosCreados.Length; i++)
        {
            desequipar(i);
        }
    }

    Vector3 devolverHuidaLejana()
    {
        float distancia = 0;

        Vector3 posicionHuida = Vector3.zero;

        for(int i = 0; i < posicionesDeHuida.Length; i++)
        {
            if (distancia < Vector3.Distance(transform.position, posicionesDeHuida[i]))
            {
                posicionHuida = posicionesDeHuida[i];
                distancia = Vector3.Distance(transform.position, posicionesDeHuida[i]);
            }
        }


        return posicionHuida;
    }

    Vector3 devolverHuidaCercana()
    {
        float distancia = 0;

        Vector3 posicionHuida = Vector3.zero;

        for (int i = 0; i < posicionesDeHuida.Length; i++)
        {
            if (distancia > Vector3.Distance(transform.position, posicionesDeHuida[i]) || distancia == 0)
            {
                posicionHuida = posicionesDeHuida[i];
                distancia = Vector3.Distance(transform.position, posicionesDeHuida[i]);
            }
        }


        return posicionHuida;
    }

}
