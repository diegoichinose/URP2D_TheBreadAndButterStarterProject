using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    public AudioSource audioSourceMusic;
    [SerializeField] private AudioSource audioSourceDefault;
    [SerializeField] private List<AudioSource> audioSourceEffectsPool;
    [SerializeField] private GameObject audioSourceEffectPrefab;
    [SerializeField] private Transform audioSourceEffectsParent;
    private float pitchRandomizerMin = 0.5f;
    private float pitchRandomizerMax = 1.5f;
    
    public CoreSoundsLibrary coreSounds;

    void Awake ()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy (gameObject);
    }

    public AudioSource PlaySound
    (
        AudioClip audioClip, 
        Vector3 position,
        bool setAsLocalPosition = false,
        AudioSource audioSource = null, 
        bool randomizePitch = false, 
        float volume = 1f, 
        float panning = 0f, 
        float pitchIncrement = 0f, 
        bool dontPlayIfAlreadyPlaying = false,
        bool useSpatialSound = false)
    {
        var audioSourceToUse = audioSource == null ? audioSourceEffectsPool.Find(x => x.isPlaying == false) : audioSource;
        if (audioSourceToUse == null)
        {
            audioSourceToUse = Instantiate(audioSourceEffectPrefab, Vector3.zero, Quaternion.identity, parent: audioSourceEffectsParent).GetComponent<AudioSource>();
            audioSourceEffectsPool.Add(audioSourceToUse);
        }

        if (dontPlayIfAlreadyPlaying && audioSourceToUse.isPlaying)
            return audioSourceToUse;

        audioSourceToUse.clip = audioClip;
        audioSourceToUse.volume = volume;
        audioSourceToUse.pitch = 1f + pitchIncrement;
        audioSourceToUse.panStereo = panning;
        audioSourceToUse.spatialBlend = useSpatialSound ? 1 : 0;

        if (setAsLocalPosition)
            audioSourceToUse.transform.localPosition = position;
        else 
            audioSourceToUse.transform.position = position;
        
        if (randomizePitch)
            audioSourceToUse.pitch = Random.Range(pitchRandomizerMin, pitchRandomizerMax);

        audioSourceToUse.Play();
        return audioSourceToUse;
    }

    // PLAYSOUND W/ NO POSITION SPECIFIED
    public AudioSource PlaySound(
        AudioClip audioClip, 
        AudioSource audioSource = null, 
        bool randomizePitch = false, 
        float volume = 1f, 
        float panning = 0f, 
        float pitchIncrement = 0f, 
        bool dontPlayIfAlreadyPlaying = false,
        bool useSpatialSound = false)
        => PlaySound(audioClip, Vector3.zero, setAsLocalPosition: true, audioSource, randomizePitch, volume, panning, pitchIncrement, dontPlayIfAlreadyPlaying, useSpatialSound);
    
    // PLAYSOUND THAT STOPS AUDIO AFTER TIME
    public void PlaySound(AudioClip audioClip, AudioSource audioSource, float stopTimer, bool randomizePitch = false)
    {
        PlaySound(audioClip, audioSource, randomizePitch);
        StartCoroutine(StopAudioSourceAfterTime(audioSource, stopTimer));
    }

	private IEnumerator StopAudioSourceAfterTime(AudioSource audioSource, float waitTime)
	{
		yield return new WaitForSecondsRealtime(waitTime); 
        audioSource.Stop();
	}

    // PLAYSOUND WHERE REPEATED CALL INCREASES PITCH 
    private float originalPitchValue = 1f;
    private float progressivePitch = 1f;
    private Coroutine resetProgressivePitch;
    public AudioSource PlaySound(AudioClip audioClip, float progressivelyIncrementPitchBy, float resetTimer, bool isResetTimerAdditive, Vector3 position)
    {
        var audioSourceToUse = audioSourceEffectsPool.Find(x => x.isPlaying == false);
        if (audioSourceToUse == null)
        {
            audioSourceToUse = Instantiate(audioSourceEffectPrefab, Vector3.zero, Quaternion.identity, parent: audioSourceEffectsParent).GetComponent<AudioSource>();
            audioSourceEffectsPool.Add(audioSourceToUse);
        }

        audioSourceToUse.transform.position = position;
        return PlaySound(audioClip, progressivelyIncrementPitchBy, resetTimer, isResetTimerAdditive, audioSourceToUse);
    }

    public AudioSource PlaySound(AudioClip audioClip, float progressivelyIncrementPitchBy, float resetTimer, bool isResetTimerAdditive, AudioSource audioSource = null)
    {
        var audioSourceToUse = audioSource == null ? audioSourceEffectsPool.Find(x => x.isPlaying == false) : audioSource;
        if (audioSourceToUse == null)
        {
            audioSourceToUse = Instantiate(audioSourceEffectPrefab, Vector3.zero, Quaternion.identity, parent: audioSourceEffectsParent).GetComponent<AudioSource>();
            audioSourceEffectsPool.Add(audioSourceToUse);
        }

        audioSourceToUse.clip = audioClip;
        audioSourceToUse.pitch = Mathf.Clamp(progressivePitch, 1, 5);
        audioSourceToUse.Play();
        progressivePitch += progressivelyIncrementPitchBy;

        if (isResetTimerAdditive)
        {
            if (resetProgressivePitch != null)
            {
                StopCoroutine(resetProgressivePitch);
                resetProgressivePitch = null;
            }

		    resetProgressivePitch = StartCoroutine(TryResetProgressivePitchAfterTime(waitTime: resetTimer));
        }
        else
        {
            if (resetProgressivePitch == null)
                resetProgressivePitch = StartCoroutine(TryResetProgressivePitchAfterTime(waitTime: resetTimer));
        }

        return audioSourceToUse;
    }
    
	private IEnumerator TryResetProgressivePitchAfterTime(float waitTime)
	{
		yield return new WaitForSecondsRealtime(waitTime); 
        progressivePitch = originalPitchValue;
        resetProgressivePitch = null;
	}
}