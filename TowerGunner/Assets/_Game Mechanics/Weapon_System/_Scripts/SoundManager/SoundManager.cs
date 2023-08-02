using System.Collections;
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
            //sFXSoundSource.Stop();
            
            sFXSoundSource.PlayOneShot(clip, volume);
            /*if (!sFXSoundSource.isPlaying)
            {
                sFXSoundSource.PlayOneShot(clip, volume);
            }*/
        }
        public void PlayShootSound(float fireRate, AudioClip clip, float volume)
        {
            //sFXSoundSource.Stop();

            StartCoroutine(WaitForFireRate(fireRate, clip, volume));
            /*if (!sFXSoundSource.isPlaying)
            {
                sFXSoundSource.PlayOneShot(clip, volume);
            }*/
        }
        
        IEnumerator WaitForFireRate(float fireRate, AudioClip clip, float volume)
        {
            yield return new WaitForSeconds(fireRate+1f);
            sFXSoundSource.PlayOneShot(clip, volume);
        }

        public void PlayButtonSound() => sFXSoundSource.PlayOneShot(buttonClip, 1);



    }


}