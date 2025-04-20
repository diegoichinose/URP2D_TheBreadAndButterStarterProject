using System.Collections.Generic;
using UnityEngine;

public class CoreSoundsLibrary : SoundLibrary
{   
    [SerializeField] private List<AudioClip> clickSounds;
    public void PlayClickSound() => PlaySound(clickSounds.GetRandom(), randomizePitch: true);

    [SerializeField] private List<AudioClip> successSounds;
    public void PlaySuccessSound() => PlaySound(successSounds.GetRandom(), randomizePitch: true);

    [SerializeField] private List<AudioClip> errorSounds;
    public void PlayErrorSound() => PlaySound(errorSounds.GetRandom(), randomizePitch: true);

    [SerializeField] private List<AudioClip> openMenuSounds;
    public void PlayOpenMenuSound() => PlaySound(openMenuSounds.GetRandom(), randomizePitch: true);

    [SerializeField] private List<AudioClip> closeMenuSounds;
    public void PlayCloseMenuSound() => PlaySound(closeMenuSounds.GetRandom(), randomizePitch: true);
    
    [SerializeField] private List<AudioClip> menuNavigationSounds;
    public void PlayMenuNavigationSound() => PlaySound(menuNavigationSounds.GetRandom(), randomizePitch: true);

    [SerializeField] private List<AudioClip> animatePositionSounds;
    public void PlayAnimatePositionSounds() => PlaySound(animatePositionSounds.GetRandom(), randomizePitch: true);

    [SerializeField] private List<AudioClip> longClickButtonHoldSounds;
    private AudioSource longClickButtonAudioSource;
    public void PlayLongClickButtonHoldSound() => longClickButtonAudioSource = PlaySound(longClickButtonHoldSounds.GetRandom(), randomizePitch: true);
    public void StopLongClickButtonHoldSound() 
    {
        if (longClickButtonAudioSource != null)
            loadingScreenAudioSource.Stop();
    }

    [SerializeField] private List<AudioClip> loadingScreenSounds;
    private AudioSource loadingScreenAudioSource;
    public void PlayLoadingScreenSound() => loadingScreenAudioSource = PlaySound(loadingScreenSounds.GetRandom(), randomizePitch: true);
    public void StopLoadingScreenSound() 
    {
        if (loadingScreenAudioSource != null)
            loadingScreenAudioSource.Stop();
    }
}