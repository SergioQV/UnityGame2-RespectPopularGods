using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Nueva equipacion", menuName ="Inventario/Equipacion")]
public class Equipacion : Item {

    public slotEquipacion slotEquipo;

    public int modificadorArmadura;
    public int modificadorDano;

    public GameObject objetoFisico;

    public Vector3 posicionAlCoger;
    public Vector3 rotacionAlCoger;

    public override void Usar()
    {
        base.Usar();
        ControladorEquipacion.instancia.Equipar(this);
        eliminarDeInventario();
    }
}

public enum slotEquipacion {arma,escudo};
