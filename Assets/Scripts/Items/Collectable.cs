using UnityEngine;
using UnityEngine.Events;
using System;
namespace Acetering{
[Serializable]
public class OnCollectEvent : UnityEvent<Collectable, IBackpack> { }
[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
    public bool auto_collect = false;
    public OnCollectEvent OnCollect;
    [SerializeField]
    protected int ready_for_collect = 0;
    new protected Collider2D collider;
    private void Start()
    {
        collider = this.GetComponent<Collider2D>();
        Init();
    }
    public void Init()
    {
        print("Collectable " + gameObject + " init");
        gameObject.SetActive(true);
    }
    public void CollectBy(IBackpack backpack)
    {
        print(gameObject + " collect by " + backpack);
        OnCollect?.Invoke(this,backpack);
        gameObject.SetActive(false);
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (auto_collect)
        {
            IActorPart actor = other.gameObject.GetComponent<IActorPart>();
            if (actor != null)
            {
                CollectBy(actor.GetBackpack());
            }
        }
        else
        if (InputManager.GetCollectDown())
        {
            IActorPart actor = other.gameObject.GetComponent<IActorPart>();
            if (actor != null)
            {
                CollectBy(actor.GetBackpack());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }
    public void Init(Vector2 pos)
    {
        transform.position = pos;
        Init();
    }
}}