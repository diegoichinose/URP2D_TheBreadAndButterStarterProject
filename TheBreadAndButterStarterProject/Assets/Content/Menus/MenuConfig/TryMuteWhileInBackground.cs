using UnityEngine;
using UnityEngine.Audio;

public class TryMuteWhileInBackground : MonoBehaviour
{
    [SerializeField] private SettingsData _settingsData;
    [SerializeField] private AudioMixer _audioMixer;
    
    void OnApplicationFocus(bool hasFocus) => TryMuteGameWhileInBackground(hasFocus);
    private void TryMuteGameWhileInBackground(bool hasFocus)
    {
        if (!_settingsData.settings.muteInBackground)
            return;

        if (hasFocus)
            _audioMixer.SetFloat("MasterVolume", _settingsData.settings.masterVolume);
        else
            _audioMixer.SetFloat("MasterVolume", _settingsData.AUDIO_MIXER_MIN);
    }
}
