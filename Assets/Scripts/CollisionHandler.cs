using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    public bool shield = false;
    [SerializeField] AudioClip successSFX;
    [SerializeField] AudioClip crashSFX;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] public ScoreTracker scoreTracker;
    [SerializeField] GameObject deathMessage;
    static public bool paused = false;
    
    AudioSource audioSource;

    bool isControllable = true;
    bool isCollidable = true;

    private void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Update() 
    {
        RespondToDebugKeys();
        if (paused)
        { StartCoroutine(FreezeTime()); }      
    }

    void RespondToDebugKeys()
    {
        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
        else if (Keyboard.current.cKey.wasPressedThisFrame)
        {
            isCollidable = !isCollidable;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!isControllable || !isCollidable) { return; }

        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("Everything is looking good!");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Collectible")
        {  
            CollectibleObject collectibleObject = other.GetComponent<CollectibleObject>();
            if (collectibleObject != null)
            {
                collectibleObject.Collect(this);
            }
        }
    }

    void StartSuccessSequence()
    {
        isControllable = false;
        audioSource.Stop();
        audioSource.PlayOneShot(successSFX);
        successParticles.Play();
        scoreTracker.countdown = false;
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void StartCrashSequence()
    {
        if (!shield)
        {
            deathMessage.SetActive(true);
            isControllable = false;
            audioSource.Stop();
            audioSource.PlayOneShot(crashSFX);
            crashParticles.Play();
            GetComponent<Movement>().enabled = false;
            Invoke("ReloadLevel", levelLoadDelay);
        }
        else
        { StartCoroutine(ShieldCountdown()); }
    }

    IEnumerator ShieldCountdown()
    {
        yield return new WaitForSecondsRealtime(0.25f);
        shield = false;
    }

    IEnumerator FreezeTime()
    {
        yield return new WaitForSecondsRealtime(2.5f);
        paused = false;
    }

    void LoadNextLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextScene = currentScene + 1;
        
        if (nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        
        SceneManager.LoadScene(nextScene);
    }

    void ReloadLevel()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

}
