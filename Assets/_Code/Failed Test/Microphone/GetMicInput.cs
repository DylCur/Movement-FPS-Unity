using UnityEngine;

public class MicInputScript : MonoBehaviour
{
    // Adjust this threshold according to your requirements
    public float volumeThreshold = 10f;

    private AudioSource audioSource;

    void Start()
    {
        // Create an AudioSource component and attach it to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Request microphone access
        if (Microphone.devices.Length > 0)
        {
            string device = Microphone.devices[0];
            audioSource.clip = Microphone.Start(device, true, 1, AudioSettings.outputSampleRate);
        }
        else
        {
            Debug.LogError("No microphone found!");
        }
    }

    void Update()
    {
        // Get the microphone data
        float[] samples = new float[128];
        audioSource.GetOutputData(samples, 0);

        // Calculate the RMS (Root Mean Square) to get the volume level
        float rms = 0f;
        foreach (float sample in samples)
        {
            rms += sample * sample;
        }
        rms = Mathf.Sqrt(rms / samples.Length);

        // Convert RMS to decibels
        float decibels = 20f * Mathf.Log10(rms);

        // Check if volume exceeds the threshold
        if (decibels > volumeThreshold)
        {
            Debug.Log("Hello!");
        }
        else
        {
            Debug.Log("Volume: " + decibels.ToString("F2") + " dB");
        }
    }

    void OnApplicationQuit()
    {
        // Stop the microphone when the application is closed
        Microphone.End(null);
    }
}
