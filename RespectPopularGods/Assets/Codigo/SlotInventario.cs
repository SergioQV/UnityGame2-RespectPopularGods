using UnityEngine;
using UnityEngine.UI;

public class SlotInventario : MonoBehaviour {

    public Image icon;
    public Button botonEliminar;

    Item item;

    public void AnadirItem(Item nuevoItem)
    {
        item = nuevoItem;
        icon.sprite = item.icon;
        icon.enabled = true;
        botonEliminar.interactable = true;

    }

    public void eliminarSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false;
        botonEliminar.interactable = false;
    }

    public void onRemoveButton()
    {
        Inventario.instancia.eliminarItem(item);
    }

    public void usarItem()
    {
        if(item != null)
        {
            item.Usar();
        }
    }
}
