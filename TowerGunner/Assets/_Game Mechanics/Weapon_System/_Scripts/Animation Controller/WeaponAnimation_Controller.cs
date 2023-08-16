using GameAssets.GameSet.GameDevUtils.Managers;
using UnityEngine;

public class WeaponAnimation_Controller : MonoBehaviour
{
    [SerializeField] Animator animator;
    private GunType currentGunType;
    public AudioClip machineGunClip;
    public AudioClip simpleGunClip;

    private void Awake()
    {
       GunsUpgradeManager.onGunUpGrade += CurrentSelectedGun;
    }
    private void CurrentSelectedGun(GunType gunType)
    {
        currentGunType = gunType;
        if (currentGunType == GunType.MachineGun)
        {
            if (SoundManager.Instance)
            {
                Debug.Log(machineGunClip == null);
                SoundManager.Instance.shoot = machineGunClip;
                Debug.Log("MACHINE GUN  " + SoundManager.Instance.shoot);
            }


        }
        else
        {
            if (SoundManager.Instance)
            {
                SoundManager.Instance.shoot = simpleGunClip;
                Debug.Log("SIMPLE GUN  " + SoundManager.Instance.shoot );
            }
        }
        Debug.Log(gunType);
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
