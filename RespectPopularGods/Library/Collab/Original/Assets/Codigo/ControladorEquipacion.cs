using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorEquipacion : MonoBehaviour {

    #region Singleton
    public static ControladorEquipacion instancia;

    private void Awake()
    {
        instancia = this;
    }
    #endregion

    public Equipacion[] equipacionActual;

    public delegate void OnEquipmentChanged(Equipacion nuevoItem, Equipacion itemAnterior);
    public OnEquipmentChanged onEquipmentChanged;

    Inventario inventario;

    private void Start()
    {
        inventario = Inventario.instancia;
        equipacionActual = new Equipacion[System.Enum.GetNames(typeof(slotEquipacion)).Length];
    }

    public void Equipar(Equipacion nuevoItem)
    {
        Equipacion itemAnterior = equipacionActual[(int)nuevoItem.slotEquipo];

        if (itemAnterior != null)
        {
            inventario.anadeItem(itemAnterior);
        }
        if (onEquipmentChanged != null)
        {
            onEquipmentChanged.Invoke(nuevoItem, itemAnterior);
        }
        equipacionActual[(int)nuevoItem.slotEquipo] = nuevoItem;
        
    }

    public void desequipar(int indiceSlot)
    {
        if(equipacionActual[indiceSlot] != null)
        {
            
            Equipacion itemAnterior = equipacionActual[indiceSlot];
            inventario.anadeItem(itemAnterior);
            equipacionActual[indiceSlot] = null;

            if (onEquipmentChanged != null)
            {
                onEquipmentChanged.Invoke(null, itemAnterior);
            }
        }
    }

    public void desequiparTodo()
    {
        for(int i = 0; i < equipacionActual.Length; i++)
        {
            desequipar(i);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            desequiparTodo();
        }
    }
}
