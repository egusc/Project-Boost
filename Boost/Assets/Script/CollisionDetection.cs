using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollsionDetection : MonoBehaviour
{

    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip victoryAudio;
    [SerializeField] ParticleSystem crashParticles;
    [SerializeField] ParticleSystem victoryParticles;

    AudioSource audio;
    bool isTransitioning = false;
    bool collisionEnabled = true;
    ParticleSystem particles;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    private void Update() {
        CheckCheatCode();
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning || !collisionEnabled) { return; };
            switch(other.gameObject.tag)
            {
                case "Friendly":
                    break;

                case "Finish":
                    StartVictorySequence();
                    break;

                default:
                        StartCrashSequence();
                    
                   break;
            }
    }

    public void StartCrashSequence()
    {
        isTransitioning = true;
        audio.Stop();
        crashParticles.Play();
        //TODO Add sound effect and particle effect on crash
        GetComponent<Movement>().StopParticles();
        GetComponent<Movement>().enabled = false;
        if(!audio.isPlaying)
        {
            audio.PlayOneShot(crashAudio);
        }
        Invoke("ReloadLevel", levelLoadDelay);
        
    }

    public void StartVictorySequence()
    {
        isTransitioning = true;
        audio.Stop();
        victoryParticles.Play();
        //TODO Add sound effect and particle effect on crash
        GetComponent<Movement>().StopParticles();
        GetComponent<Movement>().enabled = false;
        if(!audio.isPlaying)
        {
            audio.PlayOneShot(victoryAudio);
        }
        Invoke("NextLevel", levelLoadDelay);

    }

    public void ReloadLevel()
    {
        //Gets number of current scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    void CheckCheatCode()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            NextLevel();
        }
        if(Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }
    }

}
