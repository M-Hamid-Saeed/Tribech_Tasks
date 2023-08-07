using DarkVortex;
using GameAssets.GameSet.GameDevUtils.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DarkVortex {
    public class Missile : Explosion_Base
    {
        [SerializeField] float speed;
        private bool canShoot;
        private Rigidbody rb;
        [SerializeField] SoundType missleLaunchSound;
 
        private bool canMove = false;
        private Vector3 direction;
        // Start is called before the first frame update
        private void Awake()
        {
            GameController.onGameplay += onGameStart;
        }
        void Start()
        {
            MaxDamageTaken = 1;
            currentHealth = 1;
            rb = GetComponent<Rigidbody>();
            SoundManager.Instance.PlayOneShot(missleLaunchSound, volume);
        }

        // Update is called once per frame
        void Update()
        {
           // if (canMove)
           // {
                
                rb.velocity = direction * (speed * Time.fixedDeltaTime);
                transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
           // }
        }

        private void OnCollisionEnter(Collision collision)
        {
            Damage(1);
        }

        private void onGameStart()
        {
            canMove = false;
        }

        public void SetDirection(Vector3 direction)
        {
            this.direction = direction;
        }
    }
}
