using UnityEngine.Events;
using System;
[Serializable]
public class ItemEvent : UnityEvent<Item, IBackpack> { };
[Serializable]
public class OnCollectEvent : UnityEvent<Collectable, IBackpack> { }
[Serializable]
public class EquipmentEvent : UnityEvent<Equipment, IEquipmentGear> { };
[Serializable]
public class OnDamaged : UnityEvent<IDamageable, Damager> { };