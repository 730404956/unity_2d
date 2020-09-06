/*
 * File: PlayerController.cs
 * Project/package: Charactor
 * File Created: Friday, 15th May 2020 2:07:56 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Wednesday, 26th August 2020 10:20:33 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
using System;
public class PlayerController : IController
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
            move_motor.MoveTowards(move_direction);
            Weapon current_weapon = gear.GetWeapon();
            if (current_weapon != null)
            {
                Gun gun = current_weapon.GetComponent<Gun>();
                if (gun != null)
                    gun.AimDirection(horizontal, vertical);
            }
            anim.SetFloat("current_move_speed", move_motor.speed * move_direction.magnitude);
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