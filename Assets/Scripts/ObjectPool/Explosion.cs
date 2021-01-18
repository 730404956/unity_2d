using UnityEngine;
namespace Acetering{

public class Explosion : RecycleObject
{
    ParticleSystem particle;
    public override void OnObjectInit()
    {
        
    }
    public override void OnObjectCreate(IRecycleObjectFactory factory)
    {
        particle = GetComponent<ParticleSystem>();
    }
    public override void OnObjectDestroy()
    {
    }
    public void OnParticleSystemStopped()
    {
        ObjectDestroy();
    }
}}