using Character_Management;
using UnityEngine;
namespace DarkVortex
{
    public class ExplosionRange : MonoBehaviour
    {
        private void Start()
        {
            Destroy(this.gameObject, .1f);
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("Insects"))
            {
                InsectHealth insect_health = other.gameObject.GetComponentInParent<InsectHealth>();

                insect_health.GetComponentInChildren<BoxCollider>().enabled = false;
                insect_health.Dead();
                insect_health.AddScore();
            }
        }

    }
}
