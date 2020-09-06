using UnityEngine;
using System.Collections.Generic;
[RequireComponent(typeof(Moveable))]
[RequireComponent(typeof(EquipmentGear))]
[RequireComponent(typeof(Backpack))]
[RequireComponent(typeof(Damageable))]
public class Actor : ActorPart, IActor
{
    protected Moveable move_motor;
}
