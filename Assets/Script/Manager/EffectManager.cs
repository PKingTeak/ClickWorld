using System.Diagnostics.Tracing;
using UnityEngine;

public class Effects : MonoBehaviour
{ 
    
}

public class EffectManager
{
    [SerializeField]
    private Effects TestEffect;

    private PoolingSystem<Effects> effectPoolingQ = new();

    private EffectManager instance;
    public EffectManager Instance
    {
        get
        { 
            if (instance == null)
            {
                instance = new EffectManager();
            }
            return instance;
        }
    }
   



}
