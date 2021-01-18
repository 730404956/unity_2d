using UnityEngine;
namespace Acetering
{

    public class ConsumableItem : BaseItem
    {
        public bool consumable;
        public bool multiple = false;
        public int number = 1;
        public void UseItem(IActorPart actor)
        {
            operations[0]?.callBack?.Invoke(this, actor.GetBackpack());
            if (consumable)
            {
                this.number--;
                if (number <= 0)
                {
                    actor.GetBackpack().RemoveItem(this);
                    Destroy(this);
                    Destroy(gameObject);
                }
            }
        }
    }
}