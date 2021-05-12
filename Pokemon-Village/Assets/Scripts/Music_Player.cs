using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
public class Music_Player : MonoBehaviour
{
    public AudioSource source;
    public List<AudioClip> clips = new List<AudioClip>();
    
    private int currentIndex = 0;
    private FileInfo[] soundFiles;
    private List<string> validExtensions = new List<string> { ".mp3", ".wav" };
    
    private string absolutePath = "./StreamingAssets";
        
    void Start()
    {
        //being able to test in unity
        if (Application.isEditor) absolutePath = "Assets/StreamingAssets";
 
        if (source == null) source = gameObject.AddComponent<AudioSource>();
 
        ReloadSounds();
        PlayCurrent();
    }
    
    void PlayCurrent()
    {
        source.clip = clips[currentIndex];
        source.Play();
    }
    
    void ReloadSounds()
    {
        clips.Clear();
        // get all valid files
        var info = new DirectoryInfo(absolutePath);
        soundFiles = info.GetFiles()
            .Where(f => IsValidFileType(f.Name))
            .ToArray();
 
        // and load them
        foreach (var s in soundFiles)
            StartCoroutine(LoadFile(s.FullName));
    }
    
    bool IsValidFileType(string fileName)
    {
        return validExtensions.Contains(Path.GetExtension(fileName));
        
    }
    
    IEnumerator LoadFile(string path)
    {
        WWW www = new WWW("file://" + path);
        print("loading " + path);
 
        AudioClip clip = www.GetAudioClip(false);
        while(!clip.isReadyToPlay)
            yield return www;
 
        print("done loading");
        clip.name = Path.GetFileName(path);
        clips.Add(clip);
    }
}
