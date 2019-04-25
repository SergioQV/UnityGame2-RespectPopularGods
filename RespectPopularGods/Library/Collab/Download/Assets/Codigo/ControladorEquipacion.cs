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
    public GameObject[] objetosCreados;

    public delegate void OnEquipmentChanged(Equipacion nuevoItem, Equipacion itemAnterior);
    public OnEquipmentChanged onEquipmentChanged;

    public Transform manoDerechaProtagonista;
    public Transform manoIzquierdaProtagonista;

    Inventario inventario;

    private void Start()
    {
        inventario = Inventario.instancia;
        equipacionActual = new Equipacion[System.Enum.GetNames(typeof(slotEquipacion)).Length];
        objetosCreados = new GameObject[System.Enum.GetNames(typeof(slotEquipacion)).Length];
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
        int indiceSlot = (int)nuevoItem.slotEquipo;
        equipacionActual[indiceSlot] = nuevoItem;

        GameObject nuevoEquipo = Instantiate<GameObject>(nuevoItem.objetoFisico);

        if (indiceSlot == 0)
        {
            nuevoEquipo.transform.parent = manoDerechaProtagonista;
        }
        else
        {
            nuevoEquipo.transform.parent = manoIzquierdaProtagonista;
        }

        nuevoEquipo.GetComponent<CogerItem>().Personaje = ManejadorPersonaje.instancia.jugador.transform;
        nuevoEquipo.transform.localPosition = nuevoItem.posicionAlCoger;
        nuevoEquipo.transform.localEulerAngles = nuevoItem.rotacionAlCoger;
        objetosCreados[indiceSlot] = nuevoEquipo;
    }

    public void desequipar(int indiceSlot)
    {
        if(equipacionActual[indiceSlot] != null)
        {
            
            if(objetosCreados[indiceSlot] != null)
            {
                Destroy(objetosCreados[indiceSlot]);
            }

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
