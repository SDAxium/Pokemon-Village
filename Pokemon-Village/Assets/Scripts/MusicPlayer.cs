using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Networking;


public class MusicPlayer : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();
    private AudioClip m_CurrentClip;
    
    private int m_CurrentIndex = 0;
    private int m_MaxIndex = 1;
    private FileInfo[] m_SoundFiles;
    private readonly List<string> m_ValidExtensions = new List<string> { ".mp3", ".wav" };
    
    private string m_AbsolutePath = "./Audio";
        
    public async void Start()
    {
        string path = Path.Combine(Application.dataPath, "Audio", m_CurrentIndex + ".wav");

        
        //being able to test in unity
        if (Application.isEditor) m_AbsolutePath = "Assets/Audio";

        if (source == null)
        {
            source = gameObject.AddComponent<AudioSource>();
        }

        //PlayMusic();
        m_CurrentClip = await LoadClip(path);
        source.clip = m_CurrentClip;
        PlayCurrent();
        //ReloadSounds();
        //PlayCurrent();
    }

    private void Update()
    {
        
    }

    void PlayCurrent()
    {
        source.Play();
        if (m_CurrentIndex < m_MaxIndex)
        {
            m_CurrentIndex++;    
        }
        else
        {
            m_CurrentIndex = 0;
        }
        Invoke(nameof(PlayNext),source.clip.length);
    }

   private async void PlayNext()
    {
        string path = Path.Combine(Application.dataPath, "Audio", m_CurrentIndex + ".wav");
        m_CurrentClip = await LoadClip(path);
        source.clip = m_CurrentClip;
        PlayCurrent();
    }
    async Task<AudioClip> LoadClip(string path)
    {
        AudioClip clip = null;
        using (UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
        {
            www.SendWebRequest();

            try
            {
                while (!www.isDone) await Task.Delay(5);

                if (www.result == UnityWebRequest.Result.ConnectionError ||
                    www.result == UnityWebRequest.Result.ProtocolError)
                {
                    print($"{www.error}");
                }
                else
                {
                    clip = DownloadHandlerAudioClip.GetContent(www);
                }
            }
            catch (Exception err)
            {
                print($"{err.Message}, {err.StackTrace}");
                throw;
            }
        }

        return clip;
    }
    // void PlayCurrent()
    // {
    //     print(m_CurrentIndex);
    //     source.clip = clips[m_CurrentIndex];
    //     source.Play();
    // }
    //
    // void ReloadSounds()
    // {
    //     clips.Clear();
    //     // get all valid files
    //     var info = new DirectoryInfo(m_AbsolutePath);
    //     m_SoundFiles = info.GetFiles()
    //         .Where(f => IsValidFileType(f.Name))
    //         .ToArray();
    //
    //     // and load them
    //     foreach (var s in m_SoundFiles)
    //         StartCoroutine(LoadFile(s.FullName));
    // }
    //
    // bool IsValidFileType(string fileName)
    // {
    //     return m_ValidExtensions.Contains(Path.GetExtension(fileName));
    //     
    // }
    //
    // IEnumerator LoadFile(string path)
    // {
    //     
    //     print("loading " + path);
    //     
    //     using(UnityWebRequest www = UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.WAV))
    //     {
    //         yield return www.SendWebRequest();
    //         if (www.result == UnityWebRequest.Result.ConnectionError)
    //         {
    //             print(www.error);
    //         }
    //         else
    //         {
    //             AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
    //             while (clip.loadState != AudioDataLoadState.Loaded)
    //             {
    //                 yield return www;
    //             }
    //             print("done loading" + path);
    //             clip.name = Path.GetFileName(path);
    //             clips.Add(clip);
    //         }
    //     }
    // }
    
}
