using UnityEngine;
using UnityEngine.Audio;

public class OnEnableApplyCurrentSettings : MonoBehaviour
{
    [SerializeField] private SettingsData _settingsData;
    [SerializeField] private AudioMixer _audioMixer;
    
    void Start()
    {
        // NOTE: _settingsData SHOULD BE FILLED WITH THE LOCAL SAVE DATA ON AWAKE
        ApplyCurrentSettingsData();
    }

    private void ApplyCurrentSettingsData()
    {
        Screen.fullScreenMode = _settingsData.settings.fullscreenMode;
        QualitySettings.SetQualityLevel((int) _settingsData.settings.graphicsQuality);
        SetVolume("MasterVolume", _settingsData.settings.masterVolume);
        SetVolume("MusicVolume", _settingsData.settings.musicVolume);
        SetVolume("EffectsVolume", _settingsData.settings.effectsVolume);
    }

    private void SetVolume(string volumeMixerParameter, float volume)
    {
        if (volume <= _settingsData.AUDIO_MIXER_MIN)
            volume = _settingsData.AUDIO_MIXER_MIN;

        if (volume >= _settingsData.AUDIO_MIXER_MAX)
            volume = _settingsData.AUDIO_MIXER_MAX;
            
        _audioMixer.SetFloat(volumeMixerParameter, volume);
    }
}