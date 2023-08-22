using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable 
{
    void Damage(float damage, ContactPoint hitPoint);
    void PlayParticle_Sound(Vector3 collisionPoint);
}
