using UnityEngine;

public class StatsPersonaje : MonoBehaviour {

    public int saludMaxima = 100;
    public int SaludActual { get; private set; }

    public Stat dano;
    public Stat armadura;

    private void Awake()
    {
        SaludActual = saludMaxima;
    }

    private void Update()
    {
        
    }

    public void recibirDano(int dano)
    {
        dano -= armadura.getValor();
        dano = Mathf.Clamp(dano, 0, int.MaxValue);

        SaludActual -= dano;
        Debug.Log(transform.name + " recibe " + dano + " de daño");
        if(SaludActual <= 0)
        {
            Muere();
        }
    }

    public virtual void Muere()
    {
        Debug.Log(transform.name + " Muerto.");
    }
}
