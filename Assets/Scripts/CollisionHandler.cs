using System;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{

    AudioSource audioSource;
    [SerializeField]float delayInSeconds = 3f;
    
    void OnCollisionEnter(Collision other) 
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        switch(other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Start!");
                if (SceneManager.GetActiveScene().buildIndex == 0)
                {
                    Debug.Log("Welcome to Level 1!");
                }
                else if (SceneManager.GetActiveScene().buildIndex == 1)
                {
                    Debug.Log("Welcome to Level 2!");
                }
                else
                {
                    Debug.Log("Scene not loaded yet.");
                }
                break;

            case "Finish":
                if (currentSceneIndex == 1)
                {
                    StartResetSequence();
                }
                else
                {
                    StartSuccessSequence();
                }
                break;

            default:
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        // todo add SFX upon touchdown
        // todo add particle effects upon touchdown
        GetComponent<Movement>().enabled = false;
        audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        Invoke("LoadNextLevel", delayInSeconds);

    }
    void StartResetSequence()
    {
        // todo add SFX upon crash
        // todo add particle effects upon crash
        GetComponent<Movement>().enabled = false;
        audioSource = GetComponent<AudioSource>();
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
        Invoke("ResetLevels", delayInSeconds);
    }
    void StartCrashSequence()
    {
        // todo add SFX upon crash
        // todo add particle effects upon crash
        GetComponent<Movement>().enabled = false;
        audioSource = GetComponent<AudioSource>();
        Invoke("ReloadLevel", delayInSeconds);
        if(audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ResetLevels()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        currentSceneIndex = currentSceneIndex - 1;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
