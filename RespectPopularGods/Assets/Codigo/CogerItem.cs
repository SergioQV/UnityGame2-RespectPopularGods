using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogerItem : Interactuable {

    public Item item;
    
    public override void interactuar()
    {
        Inventario.instancia.anadeItem(item);
        Destroy(gameObject);
    }

}
