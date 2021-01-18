/*
 * File: ResourcesManager.cs
 * Project/package: Scripts
 * File Created: Thursday, 14th January 2021 2:24:14 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 17th January 2021 2:02:50 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 * 14-01-2021 5pm	LXR	Create.
 */
using UnityEngine;
namespace Acetering
{
    public class ResourcesManager : MonoBehaviour
    {
        [SerializeField]
        private UIText uIText_prototype;
        public UIText GetUIText()
        {
            return uIText_prototype;
        }
    }
}