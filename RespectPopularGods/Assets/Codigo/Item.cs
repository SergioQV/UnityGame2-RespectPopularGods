using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Nuevo Item", menuName ="Inventario/item")]
public class Item : ScriptableObject {

    new public string nombre = "Nuevo item";
    public Sprite icon = null;
    public bool defaultItem = false;

    public virtual void Usar()
    {
        Debug.Log("Usando " + nombre);
    }

    public void eliminarDeInventario()
    {
        Inventario.instancia.eliminarItem(this);
    }
}
