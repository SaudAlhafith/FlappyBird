using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Defaults (optional)")]
    public AudioClip defaultMenuMusic;
    public AudioClip defaultGameMusic;

    [Header("Volumes")]
    [Range(0f, 1f)] public float musicVolume = 0.35f;
    [Range(0f, 1f)] public float sfxVolume   = 1.0f;

    AudioSource musicSource;
    AudioSource sfxSource;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource   = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true;
        musicSource.playOnAwake = false;
        musicSource.spatialBlend = 0f;  // 2D

        sfxSource.playOnAwake = false;
        sfxSource.spatialBlend = 0f;    // 2D
    }

    public void PlayMusic(AudioClip clip, bool loop = true, float fadeIn = 0.25f)
    {
        if (!clip) return;

        // Don’t restart if same clip already playing
        if (musicSource.clip == clip && musicSource.isPlaying) return;

        musicSource.loop = loop;
        StartCoroutine(FadeTo(clip, fadeIn));
    }

    public void StopMusic(float fadeOut = 0.25f)
    {
        if (!musicSource.isPlaying) return;
        StartCoroutine(FadeOut(fadeOut));
    }

    public void PlaySFX(AudioClip clip, float volume01 = 1f)
    {
        if (!clip) return;
        sfxSource.PlayOneShot(clip, volume01 * sfxVolume);
    }

    IEnumerator FadeTo(AudioClip next, float fade)
    {
        // fade out current
        for (float t = 0; t < fade; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(musicVolume, 0f, t / fade);
            yield return null;
        }
        musicSource.volume = 0f;

        // switch & fade in
        musicSource.clip = next;
        musicSource.Play();
        for (float t = 0; t < fade; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(0f, musicVolume, t / fade);
            yield return null;
        }
        musicSource.volume = musicVolume;
    }

    IEnumerator FadeOut(float fade)
    {
        float start = musicSource.volume;
        for (float t = 0; t < fade; t += Time.unscaledDeltaTime)
        {
            musicSource.volume = Mathf.Lerp(start, 0f, t / fade);
            yield return null;
        }
        musicSource.Stop();
        musicSource.volume = musicVolume;
    }
}
