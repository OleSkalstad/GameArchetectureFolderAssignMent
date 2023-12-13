using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BedRock : MonoBehaviour
{
    public AudioClip bamboohit;
    AudioSource audioSource;
    float[] audioData;
    [SerializeField, Header("3D sound")]
    float maxDistance = 200.0f;
    // ... Other variables remain the same

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Spatialization settings
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1.0f;
        audioSource.spatializePostEffects = true;
        audioSource.maxDistance = maxDistance; // Use the serialized maxDistance value

        // Load audio file
        string audioFilePath = ReadAudioFilePathFromFile();
        if (!string.IsNullOrEmpty(audioFilePath))
        {
            audioData = LoadAudioData(audioFilePath);
            if (audioData != null && audioData.Length > 0)
            {
                // Create AudioClip and set audioData
                audioSource.clip = AudioClip.Create("LoadedClip", audioData.Length, 1, 44100, false);
                audioSource.clip = AudioClip.Create("LoadedClip", audioData.Length, 1, 44100, false);
                audioSource.clip.SetData(audioData, 0);
            }
        }
    }

    // Other methods remain the same

    float[] LoadAudioData(string filePath)
    {
        try
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            int headerOffset = 44;
            float[] audioData = new float[(fileBytes.Length - headerOffset) / 2];

            for (int i = 0; i < audioData.Length; i++)
            {
                audioData[i] = (short)(fileBytes[headerOffset + i * 2] | (fileBytes[headerOffset + i * 2 + 1] << 8)) / 32768.0f;

            }
            return audioData;
        }
        catch (System.Exception ex)
        {
            Debug.LogError($"Failed to load audio data: {ex.Message}");
            return null;
        }
    }

    public void PlaySound()
    {
        if (audioData != null && audioData.Length > 0)
        {
            audioSource = GetComponent<AudioSource>();
            audioSource.clip = AudioClip.Create("LoadedClip", audioData.Length, 1, 44100, false);
            audioSource.clip.SetData(audioData, 0);
            audioSource.PlayOneShot(bamboohit, 0.7f);
        }
    }

    string ReadAudioFilePathFromFile()
    {
        string p1 = Application.dataPath;
        p1 = p1.Replace("/Assets", "/Assets/Sounds/BambooSound.wav");
        return p1;
    }
}

