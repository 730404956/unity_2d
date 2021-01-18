/*
 * File: BaseController.cs
 * Project/package: Charactor
 * File Created: Thursday, 12th March 2020 7:18:42 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Monday, 18th January 2021 9:17:26 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
namespace Acetering
{
    public abstract class BaseController : ActorPart, IController
    {
        protected IMoveable moveable;
        protected override void Init()
        {
            print(this + " init");
            moveable = GetComponent<IMoveable>();
            gear = GetComponent<EquipmentGear>();
        }
        protected virtual void Update()
        {
            float move = InputManager.GetHorizontal();
            if (move != 0)
            {
                moveable.Move(Vector2.right * move);
            }
        }
        public void SetEnable(bool enable)
        {
            this.enabled = enable;
        }
        public virtual void OnEquipmentAdd(Equipment equipment)
        {
            print(equipment.name+" equip");
        }
        public virtual void OnEquipmentRemove(Equipment equipment)
        {
            print(equipment.name+" remove");
        }
    }
}