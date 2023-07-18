using UnityEngine;

namespace AxisGames.ParticleSystem
{

    public class ParticleShooter : MonoBehaviour
    {
        [SerializeField] ParticleManager particleManager;
        [SerializeField] ParticleType particleType;
        [SerializeField] InputManager input;

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                particleManager.PlayParticle(particleType, input.GetPosition());
            }
        }
    }
}