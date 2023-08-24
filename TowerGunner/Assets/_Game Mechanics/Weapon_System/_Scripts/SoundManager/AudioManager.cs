using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update

    private void OnEnable()
    {
        AudioSettings.Mobile.OnMuteStateChanged += onMusicVolChanged;
    }
    public void onMusicVolChanged(bool flag)
    {
        AudioSettings.Mobile.stopAudioOutputOnMute = flag;
    }

    public void onMusicVolChanged(float volume)
    {
        AudioSettings.Mobile.stopAudioOutputOnMute = volume > 0 ? false : true;
    }
}
