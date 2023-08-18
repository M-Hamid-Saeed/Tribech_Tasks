using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine;

public class WeaponAnimation_Controller : MonoBehaviour
{
    [SerializeField] Animator animator;
    private GunType currentGunType;
    public AudioClip machineGunClip;
    public AudioClip shotGun;
    public AudioClip simpleGunClip;
    public AudioClip bulletShellSound;
    public AudioClip LMGSound;

    private void Awake()
    {
        GunsUpgradeManager.onGunUpGrade += CurrentSelectedGun;
    }
    private void CurrentSelectedGun(GunType gunType)
    {
        currentGunType = gunType;
        switch (currentGunType)
        {
            case GunType.MachineGun:

                SoundManager.Instance.shoot = machineGunClip;
                SoundManager.Instance.bulletShellSound = bulletShellSound;
                break;

            case GunType.ShotGun:

                SoundManager.Instance.shoot = shotGun;
                SoundManager.Instance.bulletShellSound = null;
                break;
            case GunType.LMG:

                SoundManager.Instance.shoot = LMGSound;
                SoundManager.Instance.bulletShellSound = null;
                break;

            default:

                SoundManager.Instance.shoot = simpleGunClip;
                SoundManager.Instance.bulletShellSound = null;
                break;
        }





    }
    public void ShootingAnimation(bool state)
    {
        if (currentGunType == GunType.MachineGun && state)
        {
            ReferenceManager.Instance?.machineGunAnimation.DOPlay();
            ReferenceManager.Instance?.machineGunShakeAnimation.DOPlay();
        }
        else
            animator.SetBool("isFiring", state);
    }
    private void OnDestroy()
    {
        /*  machineGunClip = null;
          simpleGunClip = null;*/

    }
}
