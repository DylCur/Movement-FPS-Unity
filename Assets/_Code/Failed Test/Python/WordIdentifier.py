import sounddevice as sd
import numpy as np
import keyboard
import os
import speech_recognition as sr
from SpeechGetter import record_audio_pyaudio;

def record_microphone():
    # Set the sampling frequency and recording duration
    fs = 44100  # You can adjust this based on your requirements
    duration = 5  # Recording duration in seconds

    # Record audio from the microphone
    print("Recording... Press Enter to stop.")
    audio_data = sd.rec(int(fs * duration), samplerate=fs, channels=1, dtype=np.int16)
    sd.wait()

    return audio_data, fs

def audio_to_text(audio_data, sample_rate):
    recognizer = sr.Recognizer()
    audio_data = audio_data.flatten()  # Convert to mono if needed
    audio = sr.AudioData(audio_data.tobytes(), sample_rate, 2)  # 2 channels for stereo, 1 for mono
    try:
        text = recognizer.recognize_google(audio)
        return text
    except sr.UnknownValueError:
        return "Speech Recognition could not understand audio"
    except sr.RequestError as e:
        return f"Could not request results from Google Speech Recognition service; {e}"

def main():

    record_audio_pyaudio()

    # while True:
    #     # Record microphone input
    #     audio_data, sample_rate = record_microphone()

    #     # Clear the screen
    #     os.system('cls' if os.name == 'nt' else 'clear')

    #     # Convert the audio data to text
    #     text = audio_to_text(audio_data, sample_rate)

    #     # Count the number of words
    #     word_count = len(text.split())

    #     # Print the recognized text and word count
    #     print("Recognized Text:")
    #     print(text)
    #     print(f"\nNumber of Words: {word_count}")

    #     # Wait for Enter key press to continue
    #     print("\nPress Enter to record again or Ctrl+C to exit...")
    #     keyboard.wait("enter")


main()


