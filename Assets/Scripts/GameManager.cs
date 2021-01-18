/*
 * File: GameManager.cs
 * Project/package: Scripts
 * File Created: Friday, 16th October 2020 12:34:03 pm
 * Author: Acetering (730404956@qq.com)
 * -----
 * Last Modified: Monday, 18th January 2021 5:42:49 pm
 * Modified By: Acetering (730404956@qq.com>)
 * -----
 * MODIFIED HISTORY:
 * Time           	By 	Comments
 * ---------------	---	---------------------------------------------------------
 */
using UnityEngine;
namespace Acetering
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }
        public IActorPart main_actor { get; private set; }
        public BackpackUI backPackUI { get; private set; }
        [HideInInspector]
        public IRecycleObjectFactory objectPool { get; private set; }
        [HideInInspector]
        public ResourcesManager resourcesManager { get; private set; }
        private void Start()
        {
            instance = this;
            objectPool = new RecycleObjectFactory();
            resourcesManager = GetComponent<ResourcesManager>();
            main_actor = GameObject.Find("hero").GetComponent<IActorPart>();
            backPackUI = GameObject.Find("Canvas").transform.Find("backpack").GetComponent<BackpackUI>();
            backPackUI?.SetUp(main_actor);
            print("GM init done");
        }
    }
}