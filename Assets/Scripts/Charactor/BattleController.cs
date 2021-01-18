using UnityEngine;
namespace Acetering
{
    public class BattleController : BaseController
    {
        new protected IBattleMoveable moveable;
        BattlerAnimationManager anim;
        protected override void Init()
        {
            base.Init();
            moveable = GetComponent<IBattleMoveable>();
            anim = transform.Find("model").GetComponent<BattlerAnimationManager>();
        }
        protected override void Update()
        {
            base.Update();
            if (InputManager.GetJumpDown())
            {
                anim.Jump();
            }
            if (InputManager.GetFireDown())
            {
                anim.Attack();
            }
        }
        public void OnDie(IDamageable self, Damager src)
        {
            anim.Die();
        }
        public void OnHit(IDamageable self, Damager src)
        {
            anim.Damaged();
        }
        public override void OnEquipmentAdd(Equipment equipment)
        {
            anim.damagers.Add(equipment.damager);
        }
        public override void OnEquipmentRemove(Equipment equipment)
        {
            anim.damagers.Remove(equipment.damager);
        }
    }
}