using Unity.VisualScripting;
using UnityEngine;

public class BoxParticleSystem : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem auraParticle;

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    [Header("파티클 설정")]
    [SerializeField]
    private float startSize = 0.3f;
    [SerializeField]
    private float maxStarsize = 1.0f;
    [SerializeField]
    private float minRate = 100;
    [SerializeField]
    private float maxRate = 300;
    

    private void Awake()
    {
        if (auraParticle != null)
        {
            mainModule = auraParticle.main;
            emissionModule = auraParticle.emission;
        }
    }

    public void UpdateParticle(float _ratio, Color curColor)
    { 
        if (auraParticle == null)
        {
            return;
        }

        mainModule.startColor = new ParticleSystem.MinMaxGradient(curColor); //색상 변경

        float cursize = Mathf.Lerp(startSize, maxStarsize, _ratio);

        mainModule.startSize = new ParticleSystem.MinMaxCurve(cursize * startSize, maxStarsize);
        mainModule.startSize = 0.2f + (cursize * 0.01f); //입자 크기 조절



        //오라 효과는 최소 100개 이상있어야 효과가 잘 보인다. 
        emissionModule.rateOverTime = Mathf.Lerp(minRate,maxRate, startSize);
       // emissionModule.rateOverTime = 5 + curClick; //양증가

        if (!auraParticle.isPlaying)
        {
            auraParticle.Play();
        }
    }

    public void StopParticle()
    {
        auraParticle.Stop();
    }
}
