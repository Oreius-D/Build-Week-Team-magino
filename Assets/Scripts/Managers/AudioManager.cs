using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    private AudioSource audioSource;
    [SerializeField] private SoundData[] audioClipsInspector;
    private Dictionary<SoundID, AudioClip[]> audioDatabase;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
        audioDatabase= audioClipsInspector.ToDictionary(data=> data.SoundID, data=> data.Clips);
    }

    public void PlayMusic(SoundID id)
    {
        if (audioDatabase.TryGetValue(id,out AudioClip[] clips))
        {
            audioSource.clip = GetRandomClip(clips);
            audioSource.Play();
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
        if(audioClips==null||audioClips.Length==0)return null;

        int index = Random.Range(0, audioClips.Length);
        return audioClips[index];
    }
}
