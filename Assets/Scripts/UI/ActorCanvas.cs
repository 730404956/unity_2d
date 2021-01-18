/*
 * File: ActorCanvas.cs
 * Project/package: UI
 * File Created: Thursday, 14th January 2021 2:03:40 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Sunday, 17th January 2021 2:06:06 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 * 14-01-2021 5pm	LXR	create.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Acetering
{
    public class ActorCanvas : MonoBehaviour
    {
        protected Transform center;
        protected Bar HP_bar, MP_bar;
        void Start()
        {
            center = transform.Find("center");
            HP_bar = transform.Find("hp_bar").GetComponent<Bar>();
            MP_bar = transform.Find("mp_bar").GetComponent<Bar>();
            HP_bar.Init();
            MP_bar.Init();
        }
        public void UpdateHpBar(IDamageable me, Damager src)
        {
            HP_bar.SetBarPercentage(me.GetCurrentHP() / (float)me.GetMaxHP());
        }
        public void UpdateMpBar()
        {
            print("未实现的更新mp方法");
        }
        public void ShowText(string text)
        {
            UIText uIText = GameManager.instance.objectPool.GetRecycleObject(GameManager.instance.resourcesManager.GetUIText());
            uIText.ShowText(text, center);
        }
        public void ShowDamageText(IDamageable dst, Damager src)
        {
            ShowText("" + src.damage);
        }
    }
}
