using UnityEngine;

namespace GameAssets.GameSet.GameDevUtils.Managers
{


    public class SoundManager : MonoBehaviour
    {

        public static SoundManager Instance { get; private set; }
        [SerializeField] AudioSource bgSoundSource;
        [SerializeField] AudioSource sFXSoundSource;

        public AudioClip bgClip;
        public AudioClip buttonClip;
        public AudioClip InsectHit;
        public AudioClip bombHitClip;
        public AudioClip shoot;
        public AudioClip Explosion;
        public AudioClip MetalHit;
        public AudioClip RockHit;
        public AudioClip WoodenHit;
        public AudioClip DustSound;
        public AudioClip InsectDeathSound;


        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
        }

        void Start()
        {
            if (bgSoundSource.isPlaying)
                return;

            bgSoundSource.clip = bgClip;
            bgSoundSource.loop = true;
            bgSoundSource.Play();
        }

        public void EnableAudio(bool active)
        {
            if (active)
            {

                sFXSoundSource.enabled = true;
            }
            else
            {
                sFXSoundSource.enabled = false;

            }
        }
        public void SetBgSoundSetting(bool toggle)
        {
            bgSoundSource.mute = !toggle;
        }

        public void SetSfxSoundSetting(bool toggle)
        {
            sFXSoundSource.mute = !toggle;

        }

        public void PlayOneShot(SoundType audioType, float volume)
        {

            switch (audioType)
            {
                case SoundType.InsectHit:
                    PlayShootSound(InsectHit, volume);
                    break;
                case SoundType.MetalHit:
                    PlayShootSound(MetalHit, volume);
                    break;
                case SoundType.RockHit:
                    PlayShootSound(RockHit, volume);
                    break;
                case SoundType.WoodenHit:
                    PlayShootSound(WoodenHit, volume);
                    break;
                case SoundType.ExplosionSound:
                    PlayShootSound(Explosion, volume);
                    break;
                case SoundType.DustSound:
                    PlayShootSound(DustSound, volume);
                    break;
                default:
                    break;
            }


        }
        public void PlayShootSound(AudioClip clip, float volume)
        {

            sFXSoundSource.PlayOneShot(clip, volume);
        }


        public void PlayButtonSound() => sFXSoundSource.PlayOneShot(buttonClip, 1);



    }

    public enum SoundType
    {
        InsectHit,
        RockHit,
        MetalHit,
        WoodenHit,
        ExplosionSound,
        DustSound,
        InsectDeath
    }
}