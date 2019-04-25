using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIinventario : MonoBehaviour {

    public Transform itemsParent;
    public GameObject inventarioUI;
    Inventario inventario;
    SlotInventario[] slots;
    [SerializeField]
    controladorCamaraTercera ccamtercera;

    // Use this for initialization
    void Start () {
        inventario = Inventario.instancia;
        inventario.OnItemChangedCallback += updateUI;

        slots = itemsParent.GetComponentsInChildren<SlotInventario>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventario"))
        {
            inventarioUI.SetActive(!inventarioUI.activeSelf);
            ccamtercera.enabled = !ccamtercera.enabled;
        }
	}


    void updateUI()
    {

        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventario.items.Count)
            {
                slots[i].AnadirItem(inventario.items[i]);
            }
            else
            {
                slots[i].eliminarSlot();
            }
        }

    }
}
