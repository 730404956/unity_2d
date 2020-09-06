using UnityEngine;

public class Amor : Equipment
{
    [Range(0, 1000)]
    public int fixed_damage_decrease = 0;
    [Range(0, 100)]
    public float percentage_damage_decrease = 0;
    private void Start()
    {
        onEquip.AddListener((equipment,gear) =>
        {
            EquipmentGear equipmentGear= gear.GetGear() as EquipmentGear;
            Damageable damageable= equipmentGear.GetComponent<Damageable>();
            damageable.AddBeforeDamageListener((self,src)=>{
                int damage =(int)(src.damage*(1 - percentage_damage_decrease))-fixed_damage_decrease;
                src.damage = Mathf.Max(damage, 1);
            });
        });
    }
}