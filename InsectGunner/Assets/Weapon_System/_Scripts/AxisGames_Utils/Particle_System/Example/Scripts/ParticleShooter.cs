using UnityEngine;

namespace AxisGames.ParticleSystem
{

    public class ParticleShooter : MonoBehaviour
    {
        [SerializeField] ParticleManager particleManager;
        [SerializeField] ParticleType particleType;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                particleManager.PlayParticle(particleType, MouseWorldInput.GetPosition());
            }
        }
    }
}