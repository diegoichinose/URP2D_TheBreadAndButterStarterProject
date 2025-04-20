using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConfigMenuButtons : MonoBehaviour
{
    [SerializeField] private SettingsData _settingsData;
    [SerializeField] private ConfigMenuUI _configMenuUI;
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private GameObject ResolutionConfirmPopup;
    [SerializeField] private TMP_Text ResolutionConfirmTimer;
    [SerializeField] private TMP_Dropdown ResolutionsDropdown;
    [SerializeField] private TMP_Dropdown FullscreenDropdown;
    [SerializeField] private TMP_Dropdown GraphicsQualityDropdown;
    [SerializeField] private TMP_Text MasterVolumePercentage;
    [SerializeField] private TMP_Text MusicVolumePercentage;
    [SerializeField] private TMP_Text EffectsVolumePercentage;
    [SerializeField] private Slider MasterVolumeSlider;
    [SerializeField] private Slider MusicVolumeSlider;
    [SerializeField] private Slider EffectsVolumeSlider;
    [SerializeField] private Toggle DamageNumbersToggle;
    [SerializeField] private Toggle ScreenshakeToggle;
    [SerializeField] private Toggle MuteInBackground;
    private GameInput _playerInput;
    private int previousResolutionIndex;

    private Resolution[] resolutions;
    private double GetVolumePercentageForAudioMixer(float volume) => Math.Truncate((volume - _settingsData.AUDIO_MIXER_MIN) / (_settingsData.AUDIO_MIXER_MAX - _settingsData.AUDIO_MIXER_MIN) * 100);

    void Awake()
    {
        _playerInput = new GameInput();
    }
    void OnEnable()
    {
        _settingsData.OnSettingsDataReset += ResetToSettingsData;
    }

    void OnDisable()
    {
        _settingsData.OnSettingsDataReset -= ResetToSettingsData;
        StopAllCoroutines();
        
        if(_playerInput != null)
            _playerInput.Disable();
    }

    void Start()
    {
        GetResolutionOptionsFromMachine();
        SetVolumeSlidersMinMax();
        ResetToSettingsData();
    }

    private void SetVolumeSlidersMinMax()
    {
        MasterVolumeSlider.minValue     = _settingsData.AUDIO_MIXER_MIN;
        MasterVolumeSlider.maxValue     = _settingsData.AUDIO_MIXER_MAX;
        MusicVolumeSlider.minValue      = _settingsData.AUDIO_MIXER_MIN;
        MusicVolumeSlider.maxValue      = _settingsData.AUDIO_MIXER_MAX;
        EffectsVolumeSlider.minValue    = _settingsData.AUDIO_MIXER_MIN;
        EffectsVolumeSlider.maxValue    = _settingsData.AUDIO_MIXER_MAX;
    }

    public void ResetToSettingsData()
    {
        SetFullscreen((int) _settingsData.settings.fullscreenMode);
        SetGraphicsQuality((int) _settingsData.settings.graphicsQuality);
        SetMasterVolume(_settingsData.settings.masterVolume);
        SetMusicVolume(_settingsData.settings.musicVolume);
        SetEffectsVolume(_settingsData.settings.effectsVolume);
        SetDamageNumbers(_settingsData.settings.disableDamageNumbers);
        SetScreenshake(_settingsData.settings.disableScreenshake);
        SetMuteInBackground(_settingsData.settings.muteInBackground);
        previousResolutionIndex = ResolutionsDropdown.value;
    }

    public void OnResolutionSelect(int dropdownIndex)
    {
        SetResolution(dropdownIndex);
        OpenResolutionConfirmationPrompt();
        StartCoroutine(WaitAndRevertResolution());
        
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }

    private void SetResolution(int resolutionIndex)
    {
        _settingsData.settings.resolutionIndex = resolutionIndex;
        ResolutionsDropdown.value = resolutionIndex;
        ResolutionsDropdown.RefreshShownValue();

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, _settingsData.settings.fullscreenMode);

        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }

    private IEnumerator WaitAndRevertResolution()
    {
        int popUpTimerSeconds = 5;
        while (ResolutionConfirmPopup.activeSelf && popUpTimerSeconds > 0)
        {
            ResolutionConfirmTimer.text = popUpTimerSeconds.ToString();
		    yield return new WaitForSecondsRealtime(1);
            popUpTimerSeconds--;
        }

        ResolutionRevert();
    }

    public void ResolutionRevert(InputAction.CallbackContext context) => ResolutionRevert();
    public void ResolutionRevert()
    {
        SetResolution(previousResolutionIndex);
        CloseResolutionConfirmationPrompt();
    }
    
    public void ResolutionConfirm()
    {
        _settingsData.settings.resolutionIndex = ResolutionsDropdown.value;
        previousResolutionIndex = ResolutionsDropdown.value;
        CloseResolutionConfirmationPrompt();

        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
    }

    private void OpenResolutionConfirmationPrompt()
    {
        _playerInput.Enable();
        _playerInput.Menu.CloseMenu.performed += ResolutionRevert;
        ResolutionConfirmPopup.SetActive(true);
        _configMenuUI.canClose = false;
    }

    private void CloseResolutionConfirmationPrompt()
    {
        _playerInput.Menu.CloseMenu.performed -= ResolutionRevert;
        _playerInput.Disable();
        StopAllCoroutines();
        ResolutionConfirmPopup.SetActive(false);
        EventSystemManager.instance.InvokeAfterTime(() => _configMenuUI.canClose = true, time: 0.01f);
    }


    private void GetResolutionOptionsFromMachine()
    {
        resolutions = Screen.resolutions;
        ResolutionsDropdown.options.Clear();
        int index = 0;
        foreach (Resolution resolution in resolutions)
        {
            ResolutionsDropdown.options.Add(new TMP_Dropdown.OptionData() { text = resolution.ToString() });
            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
                ResolutionsDropdown.value = index;

            index++;
        }
        
        ResolutionConfirmPopup.SetActive(false);
    }

    public void SetFullscreen(int dropdownIndex)
    {
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        FullscreenDropdown.value = dropdownIndex;
        GraphicsQualityDropdown.RefreshShownValue();
        _settingsData.settings.fullscreenMode = (FullScreenMode) dropdownIndex;
        Screen.fullScreenMode = _settingsData.settings.fullscreenMode;
    } 

    public void SetGraphicsQuality(int dropdownIndex)
    {
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        GraphicsQualityDropdown.value = dropdownIndex;
        GraphicsQualityDropdown.RefreshShownValue();
        _settingsData.settings.graphicsQuality = (GraphicsQuality) dropdownIndex;
        QualitySettings.SetQualityLevel((int) _settingsData.settings.graphicsQuality);
    }

    public void SetDamageNumbers(bool isChecked) 
    {
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        DamageNumbersToggle.isOn = isChecked;
        _settingsData.settings.disableDamageNumbers = isChecked;
    }

    public void SetScreenshake(bool isChecked) 
    {
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        ScreenshakeToggle.isOn = isChecked;
        _settingsData.settings.disableScreenshake = isChecked;
    }

    public void SetMuteInBackground(bool isChecked) 
    {
        AudioManager.instance.coreSounds.PlayMenuNavigationSound();
        MuteInBackground.isOn = isChecked;
        _settingsData.settings.muteInBackground = isChecked;
    }

    public void SetMasterVolume(float volume)
    {
        MasterVolumeSlider.value = volume;
        MasterVolumePercentage.text =  GetVolumePercentageForAudioMixer(volume) + "%";

        _settingsData.settings.masterVolume = volume;
        SetVolume("MasterVolume", volume);
        AudioManager.instance.coreSounds.PlayClickSound();
    }

    public void SetMusicVolume(float volume)
    {
        MusicVolumeSlider.value = volume;
        MusicVolumePercentage.text =  GetVolumePercentageForAudioMixer(volume) + "%";

        _settingsData.settings.musicVolume = volume;
        SetVolume("MusicVolume", volume);
    }

    public void SetEffectsVolume(float volume)
    {
        EffectsVolumeSlider.value = volume;
        EffectsVolumePercentage.text =  GetVolumePercentageForAudioMixer(volume) + "%";
        
        _settingsData.settings.effectsVolume = volume;
        SetVolume("EffectsVolume", volume);
        AudioManager.instance.coreSounds.PlayClickSound();
    }

    public void SetVolume(string volumeMixerParameter, float volume)
    {
        if (volume <= _settingsData.AUDIO_MIXER_MIN)
            volume = _settingsData.AUDIO_MIXER_MIN;

        if (volume >= _settingsData.AUDIO_MIXER_MAX)
            volume = _settingsData.AUDIO_MIXER_MAX;
            
        _audioMixer.SetFloat(volumeMixerParameter, volume);
    }
}