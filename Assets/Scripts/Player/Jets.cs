using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Jets : MonoBehaviour
{
    [SerializeField] ParticleSystem[] jetParticleSystems;
    [SerializeField] Light2D[] jetLights;

    public void ActivateJetParticles()
    {
        for (int i = 0; i < jetParticleSystems.Length; i++)
        {
            jetParticleSystems[i].Play();
            jetLights[i].intensity = 1;
        }
    }
    public void DeactivateJetParticles()
    {
        for (int i = 0; i < jetParticleSystems.Length; i++)
        {
            jetParticleSystems[i].Stop();
            jetLights[i].intensity = 0;
        }
    }
}
