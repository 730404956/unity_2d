using UnityEngine;
namespace Acetering
{
    public class ActorPart : MonoBehaviour, IActorPart
    {
        private bool inited = false;
        public IDamageable damageable;
        protected IBackpack backpack;
        protected IEquipmentGear gear;
        protected IController controller;
        private void Awake()
        {
            if (!inited)
            {
                damageable = GetComponent<IDamageable>();
                gear = GetComponent<IEquipmentGear>();
                backpack = GetComponent<IBackpack>();
                controller = GetComponent<BaseController>();
                Init();
                inited = true;
                damageable.Init(gear, backpack, controller, damageable);
                backpack.Init(gear, backpack, controller, damageable);
                gear.Init(gear, backpack, controller, damageable);
                controller.Init(gear, backpack, controller, damageable);
            }
        }
        public void Init(IEquipmentGear gear, IBackpack backpack, IController controller, IDamageable damageable)
        {
            if (inited)
                return;
            this.gear = gear;
            this.backpack = backpack;
            this.controller = controller;
            this.damageable = damageable;
            Init();
            inited = true;
        }
        protected virtual void Init()
        {
            print(this+"nothing to init");
        }
        public IEquipmentGear GetGear()
        {
            return gear;
        }
        public IDamageable GetDamageable()
        {
            return damageable;
        }
        public IBackpack GetBackpack()
        {
            return backpack;
        }
        public int GetLayer()
        {
            return gameObject.layer;
        }
        public Transform GetTransform()
        {
            return transform;
        }
        public IController GetController()
        {
            return controller;
        }
    }
}