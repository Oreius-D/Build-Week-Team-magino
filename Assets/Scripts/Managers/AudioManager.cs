using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;
    [SerializeField] public SoundData[] audioClipsInspector;
    private Dictionary<SoundID, AudioClip[]> audioDatabase;
    private Coroutine songsRoutine;
    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        audioDatabase = audioClipsInspector.ToDictionary(data => data.SoundID, data => data.Clips);
    }
    private void Start()
    {
        PlayMusic(SoundID.Music);
    }
    public void PlayMusic(SoundID id)
    {
        if (audioDatabase.TryGetValue(id, out AudioClip[] clips))
        {
            if (songsRoutine == null)
            {
                songsRoutine = StartCoroutine(NextSong(clips));
            }
            else
            {
                StopCoroutine(songsRoutine);
                songsRoutine = null;
                songsRoutine = StartCoroutine(NextSong(clips));
            }
        }
        else
        {
            Debug.LogWarning($"non ci sono Clips con L'Id: {id} ");
        }
    }
    public void PlaySound(SoundID id)
    {
        if (audioDatabase.TryGetValue(id, out AudioClip[] clips))
        {
            audioSource.PlayOneShot(GetRandomClip(clips));
        }
        else
        {
            Debug.LogWarning($"non ci sono Clips con L'Id: {id} ");
        }
    }
    public void PlaySound(AudioSource source, SoundID id)
    {
        if (audioDatabase.TryGetValue(id, out AudioClip[] clips))
        {
            source.clip = GetRandomClip(clips);
            source.Play();
        }
        else
        {
            Debug.LogWarning($"non ci sono Clips con L'Id: {id} ");
        }
    }
    private AudioClip GetRandomClip(AudioClip[] audioClips)
    {
        if (audioClips == null || audioClips.Length == 0) return null;

        int index = Random.Range(0, audioClips.Length);
        return audioClips[index];
    }

    private IEnumerator NextSong(AudioClip[] clips)
    {
        while (true)
        {
            audioSource.clip = GetRandomClip(clips);
            audioSource.Play();
            yield return new WaitForSeconds(audioSource.clip.length);
        }

    }
}