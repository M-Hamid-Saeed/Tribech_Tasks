using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character_Management;
namespace DarkVortex {
    public class ExplosionRange : MonoBehaviour
    {
        private void Start()
        {
            Destroy(this.gameObject, .5f);
        }
        private void OnTriggerStay(Collider other)
        {
           InsectHealth insect_health =  other.GetComponentInParent<InsectHealth>();

            if (insect_health)
            {
                insect_health.GetComponentInChildren<BoxCollider>().enabled = false;
                insect_health.Dead();
            }
            
        }
    }
}
