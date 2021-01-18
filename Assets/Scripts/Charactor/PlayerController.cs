using UnityEngine;
using System;
namespace Acetering
{
    public class PlayerController : BaseController
    {
        protected override void Update()
        {
            base.Update();
            //get horizontal input
            float horizontal = InputManager.GetHorizontal();
            //get vertical input
            float vertical = InputManager.GetVertical();
            if (!Mathf.Approximately(horizontal, 0) || !Mathf.Approximately(vertical, 0))
            {
                Vector2 move_direction = new Vector2(horizontal, vertical);
                // move unit
                moveable.Move(move_direction);
                Weapon current_weapon = gear.GetWeapon();
                if (current_weapon != null)
                {
                    Gun gun = current_weapon.GetComponent<Gun>();
                    if (gun != null)
                        gun.AimDirection(horizontal, vertical);
                }
            }
            // use weapon
            if (InputManager.GetFireDown())
            {
                gear.GetWeapon()?.Use();
            }
            //end use weapon
            if (InputManager.GetFireUp())
            {
                gear.GetWeapon()?.FinishUsing();
            }

        }
    }
}