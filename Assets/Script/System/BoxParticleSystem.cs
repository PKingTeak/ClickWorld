using UnityEngine;

public class BoxParticleSystem : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem auraParticle;

    private ParticleSystem.MainModule mainModule;
    private ParticleSystem.EmissionModule emissionModule;

    private void Awake()
    {
        if (auraParticle != null)
        {
            mainModule = auraParticle.main;
            emissionModule = auraParticle.emission;
        }
    }

    public void UpdateParticle(int curClick, Color curColor)
    { 
        if (auraParticle == null)
        {
            return;
        }

        mainModule.startColor = new ParticleSystem.MinMaxGradient(curColor); //색상 변경
        mainModule.startSize = 0.2f + (curClick * 0.01f); //입자 크기 조절

        emissionModule.rateOverTime = 5 + curClick; //양증가

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
