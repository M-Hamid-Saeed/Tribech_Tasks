using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour {
    [Header("Dash Ability")]
    [SerializeField] private float dashSpeed = 5f;
    [SerializeField] private float dashDuration = 1f;
    private bool isDashing = false;
    public GameObject playerVisual;
    private float dashEndTime;
    private PlayerController playerController;
    public Animator animator;
    public Rigidbody playerRB;
    [Header("Dashing Particle Section")]
    private Transform dashParticlesContainer;
    private ParticleSystem[] dashParticles;


    private void Awake() {
        playerController = GetComponent<PlayerController>();
    }

    private void Start()
    {
        // find the dash particle container and intialize the dash particle array with its childrens
        dashParticlesContainer = transform.Find("Particles").Find("DashParticles");
        Debug.Log(dashParticlesContainer.name + dashParticlesContainer.childCount);
        dashParticles = dashParticlesContainer.GetComponentsInChildren<ParticleSystem>();
        Debug.Log($"Component length = {dashParticles.Length}");
    }

    private void Update() {



        if (isDashing && Time.time >= dashEndTime) {
            playerController.animator.SetBool("isDashing", false);
            EndDash();
        }
    }
    public void OnDashPressed()
    {
        if (!isDashing)
        {
            playerController.animator.SetBool("isDashing", true);
            StartDash();
        }
    }

    private void StartDash() {
        isDashing = true;
        dashEndTime = Time.time + dashDuration;

        Vector3 dashDirection = playerVisual.transform.forward;
        Debug.Log(dashDirection);
        playerRB.AddForce(dashDirection * dashSpeed, ForceMode.Impulse);
        PlayDashParticles();
    }

    private void EndDash() {
        isDashing = false;
    }

    private void PlayDashParticles()
    {
        dashParticlesContainer.forward = playerVisual.transform.forward;
        foreach (var particles in dashParticles)
            particles.Play();
    }
}
