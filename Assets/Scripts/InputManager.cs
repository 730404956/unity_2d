/*
 * File: InputManager.cs
 * Project/package: Scripts
 * File Created: Thursday, 12th March 2020 10:28:59 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Monday, 18th January 2021 10:58:55 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
namespace Acetering
{

    public class InputManager
    {
        public static float GetHorizontal()
        {
            return Input.GetAxis("Horizontal");
        }
        public static float GetVertical()
        {
            return Input.GetAxis("Vertical");
        }
        public static bool GetFireDown()
        {
            return Input.GetKeyDown(UnityEngine.KeyCode.J);
        }
        public static bool GetFireUp()
        {
            return Input.GetKeyUp(UnityEngine.KeyCode.J);
        }
        public static bool GetChangeWeaponDown()
        {
            return Input.GetKeyDown(UnityEngine.KeyCode.X);
        }
        public static bool GetCollectDown()
        {
            return Input.GetKeyDown(UnityEngine.KeyCode.C);
        }
        public static bool GetReloadDown()
        {
            return Input.GetKeyDown(UnityEngine.KeyCode.R);
        }
        public static bool GetJumpDown()
        {
            return Input.GetKeyDown(UnityEngine.KeyCode.K);
        }
    }
}