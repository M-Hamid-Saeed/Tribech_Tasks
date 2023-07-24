using UnityEngine;

public class FireSound : MonoBehaviour
{
    [SerializeField] AudioSource source;
    [Space]
    [SerializeField] AudioClip ShootingSound;
    [SerializeField] AudioClip ReloadSound;
    [Space]
    [SerializeField, Range(0.3f, 1f)] float volume = 1;

    private void Awake()
    {
        source.playOnAwake = false;

    }

    public void PlayShoot()
    {
        source.clip = ShootingSound;
        if (source.isPlaying)
        {
            source.Stop();
            source.Play();
        }
    }
    
    public void PlayReload()
    {
        /*if (!source.isPlaying)*/ { source.PlayOneShot(ReloadSound, volume); }
    }
}
