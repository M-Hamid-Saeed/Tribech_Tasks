using UnityEngine;

public class AI_AnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;

    public void Idle()
    {
        animator.SetTrigger("Idle");
    }

    public void ShootingAnimation(bool state)
    {
        animator.SetBool("shoot", state);
    }

    public void StartDancing()
    {
        ShootingAnimation(false);
        animator.SetTrigger("Dance");
    }
}
