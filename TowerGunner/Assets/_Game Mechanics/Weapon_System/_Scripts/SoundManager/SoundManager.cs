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
        public AudioClip eatInsect;
        public AudioClip bombHitClip;
        public AudioClip shoot;
        public AudioClip progression;
        public AudioClip cash;


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

        public void PlayOneShot(AudioClip clip, float volume)
        {
            if(!sFXSoundSource.isPlaying)
                sFXSoundSource.PlayOneShot(clip, volume);
        }

        public void PlayButtonSound() => sFXSoundSource.PlayOneShot(buttonClip, 1);



    }


}