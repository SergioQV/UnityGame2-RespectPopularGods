using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour {

    public static Inventario instancia;
    public List<Item> items = new List<Item>();
    public int espacio = 20;
    public delegate void onItemChanged();
    public onItemChanged OnItemChangedCallback;

    private void Awake()
    {
        if(instancia != null)
        {
            return;
        }
        instancia = this;
    }

    public bool anadeItem(Item item)
    {
        if (!item.defaultItem && items.Count < espacio)
        {
            items.Add(item);
            if(OnItemChangedCallback != null)
            {
                OnItemChangedCallback.Invoke();
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void eliminarItem(Item item)
    {
        items.Remove(item);

        if(OnItemChangedCallback != null)
        {
            OnItemChangedCallback.Invoke();
        }
    }
}
