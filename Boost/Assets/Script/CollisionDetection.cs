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

    AudioSource audio;
    bool isTransitioning = false;

    private void Start() {
        audio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision other) {
        if(isTransitioning) { return; };
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
        //TODO Add sound effect and particle effect on crash
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
        //TODO Add sound effect and particle effect on crash
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

}
