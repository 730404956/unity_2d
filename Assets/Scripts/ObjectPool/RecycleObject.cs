using UnityEngine;
using System.Collections.Generic;
namespace Acetering{
public interface IRecycleObject
{
    void ObjectInit();
    void ObjectDestroy();
    /// <summary>
    /// check if the object is aviable and not in use.
    /// </summary>
    /// <returns>false->if object can't be use for another place</returns>
    bool IsAviable();
    void BindFactory(IRecycleObjectFactory factory);
    /// <summary>
    /// clone the object as prototype. 
    /// </summary>
    /// <param name="prototype"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>object has the same attr as prototype</returns>
    T CopyAttr<T>(T prototype) where T : IRecycleObject;
    T Clone<T>(T prototype) where T : IRecycleObject;

}
public interface RecycleObjectInitiator
{
    /// <summary>
    /// call back when object is created, will execute only once.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    void OnObjectCreate(IRecycleObjectFactory factory);
    /// <summary>
    /// call back after object is created and before object reuse in other place.
    /// </summary>
    void OnObjectInit();
    /// <summary>
    /// call back when object finish its work and before it's recycled.
    /// </summary>
    void OnObjectDestroy();
}
public abstract class RecycleObject : MonoBehaviour, IRecycleObject, RecycleObjectInitiator
{
    protected RecycleObjectInitiator[] initiators;
    protected IRecycleObjectFactory m_factory;
    private bool aviable;
    public abstract void OnObjectCreate(IRecycleObjectFactory factory);
    public abstract void OnObjectInit();
    public abstract void OnObjectDestroy();
    public void ObjectInit()
    {
        aviable = false;
        gameObject.SetActive(true);
        foreach (var item in initiators)
        {
            item.OnObjectInit();
        }
    }
    public bool IsAviable()
    {
        return aviable;
    }
    public void ObjectDestroy()
    {
        foreach (var item in initiators)
        {
            item.OnObjectDestroy();
        }
        gameObject.SetActive(false);
        aviable = true;
        m_factory.Recycle(this);
    }
    public virtual T CopyAttr<T>(T template) where T : IRecycleObject
    {
        return (T)(IRecycleObject)this;
    }
    public void BindFactory(IRecycleObjectFactory factory)
    {
        this.m_factory = factory;
    }
    public virtual T Clone<T>(T template) where T : IRecycleObject
    {
        if (this is T)
        {
            RecycleObject recycleObject = Instantiate(this);
            recycleObject.m_factory = m_factory;
            recycleObject.initiators = recycleObject.GetComponents<RecycleObjectInitiator>();
            foreach (RecycleObjectInitiator item in recycleObject.initiators)
            {
                item.OnObjectCreate(m_factory);
            }
            return (T)((IRecycleObject)recycleObject.CopyAttr(template));
        }
        else
        {
            print("warning! type incompatible");
            return default(T);
        }
    }
}
public interface IRecycleObjectFactory
{
    void AddPrototype(IRecycleObject template);
    T Create<T>(T template) where T : IRecycleObject;
    T GetRecycleObject<T>(T template) where T : IRecycleObject;
    void Recycle(IRecycleObject recycleObject);
}
public class RecycleObjectFactory : IRecycleObjectFactory
{
    protected Dictionary<System.Type, List<IRecycleObject>> prototypes = new Dictionary<System.Type, List<IRecycleObject>>();
    public virtual void AddPrototype(IRecycleObject template)
    {
        if (!prototypes.ContainsKey(template.GetType()))
        {
            List<IRecycleObject> group = new List<IRecycleObject>();
            template.BindFactory(this);
            prototypes.Add(template.GetType(), group);
        }
        else
        {
            MonoBehaviour.print("already has the type:" + template.GetType());
        }
    }
    public virtual T Create<T>(T template) where T : IRecycleObject
    {
        T obj = template.Clone(template);
        prototypes[template.GetType()].Add(obj);
        return obj;
    }
    public T GetRecycleObject<T>(T template) where T : IRecycleObject
    {
        if (prototypes.ContainsKey(template.GetType()))
        {
            foreach (T t in prototypes[template.GetType()])
            {
                if (t.IsAviable())
                {
                    return t.CopyAttr(template);
                }
            }
        }
        else
        {
            AddPrototype(template);
        }
        return Create<T>(template);
    }
    public virtual void Recycle(IRecycleObject recycleObject)
    {
        MonoBehaviour.print("recycle: " + recycleObject);
    }
}}