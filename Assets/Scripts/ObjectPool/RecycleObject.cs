using UnityEngine;
using System.Collections.Generic;
public interface IRecycleObject
{
    IRecycleObject ObjectCreate(IRecycleObjectFactory factory);
    void ObjectInit();
    void ObjectDestroy();
    // IRecycleObject OnObjectCreate(IRecycleObjectFactory factory);
    // void OnObjectInit();
    // void OnObjectDestroy();
    /// <summary>
    /// check if the object is aviable and not in use.
    /// </summary>
    /// <returns>false->if object can't be use for another place</returns>
    bool IsAviable();
    /// <summary>
    /// clone the object as prototype. 
    /// </summary>
    /// <param name="prototype"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns>object has the same attr as prototype</returns>
    T Clone<T>(T prototype) where T : IRecycleObject;
    IRecycleObject Copy(IRecycleObject prototype);

}
public abstract class RecycleObject : MonoBehaviour, IRecycleObject
{
    protected IRecycleObjectFactory m_factory;
    private bool aviable;
    /// <summary>
    /// call back when object is created, will execute only once.
    /// </summary>
    /// <param name="factory"></param>
    /// <returns></returns>
    protected abstract void OnObjectCreate(IRecycleObjectFactory factory);
    /// <summary>
    /// call back after object is created and before object reuse in other place.
    /// </summary>
    protected abstract void OnObjectInit();
    /// <summary>
    /// call back when object finish its work and before it's recycled.
    /// </summary>
    protected abstract void OnObjectDestroy();
    public virtual IRecycleObject ObjectCreate(IRecycleObjectFactory factory)
    {
        m_factory = factory;
        OnObjectCreate(factory);
        return this;
    }
    public void ObjectInit()
    {
        gameObject.SetActive(true);
        aviable = false;
        OnObjectInit();
    }
    public bool IsAviable()
    {
        return aviable;
    }
    public void ObjectDestroy()
    {
        OnObjectDestroy();
        aviable = true;
        gameObject.SetActive(false);
        m_factory.Recycle(this);
    }
    public virtual T Clone<T>(T prototype) where T : IRecycleObject
    {
        if (this is T)
        {
            IRecycleObject recycleObject = Instantiate(this);
            return (T)recycleObject;
        }
        else
        {
            return default(T);
        }
    }
    public virtual IRecycleObject Copy(IRecycleObject prototype) {
        return this;
    }
}
public interface IRecycleObjectFactory
{
    void AddPrototype(IRecycleObject obj);
    T Create<T>(T template) where T : IRecycleObject;
    T GetRecycleObject<T>(T template) where T : IRecycleObject;
    void Recycle(IRecycleObject recycleObject);
}
public class RecycleObjectFactory : IRecycleObjectFactory
{
    protected Dictionary<System.Type, List<IRecycleObject>> prototypes;
    public RecycleObjectFactory() {
        prototypes = new Dictionary<System.Type, List<IRecycleObject>>();
    }
    public virtual void AddPrototype(IRecycleObject obj)
    {
        if (!prototypes.ContainsKey(obj.GetType()))
        {
            List<IRecycleObject> group = new List<IRecycleObject>();
            prototypes.Add(obj.GetType(), group);
        }
    }
    public virtual T Create<T>(T template) where T : IRecycleObject
    {
        T obj = template.Clone(template);
        obj.ObjectCreate(this);
        prototypes[template.GetType()].Add(obj);
        return obj;
    }
    public T GetRecycleObject<T>(T template) where T : IRecycleObject
    {
        T temp;
        if (prototypes.ContainsKey(template.GetType()))
        {
            foreach (T t in prototypes[template.GetType()])
            {
                if (t.IsAviable())
                {
                    temp=(T)t.Copy(template);
                    temp.ObjectInit();
                    return temp;
                }
            }
        }
        else
        {
            List<IRecycleObject> group = new List<IRecycleObject>();
            prototypes.Add(template.GetType(), group);
        }
        temp= Create<T>(template);
        temp.ObjectInit();
        return temp;
    }
    public virtual void Recycle(IRecycleObject recycleObject)
    {
        MonoBehaviour.print("recycle: " + recycleObject);
    }
}