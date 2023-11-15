using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audio;
    [SerializeField] float thrust = 1000f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip thrusterAudio;
    [SerializeField] ParticleSystem mainThrusterParticles;
     [SerializeField] ParticleSystem leftThrusterParticles;
      [SerializeField] ParticleSystem rightThrusterParticles;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    // THRUST //
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }

    }

    private void StartThrusting()
    {
        rb.AddRelativeForce(Vector3.up * thrust * Time.deltaTime);
        if (!audio.isPlaying)
        {
            audio.PlayOneShot(thrusterAudio);
        }
        if (!mainThrusterParticles.isPlaying)
        {
            mainThrusterParticles.Play();
        }
    }

    private void StopThrusting()
    {
        audio.Stop();
        mainThrusterParticles.Stop();
    }

    // ROTATION // 
    void ProcessRotation()
    {
         if(Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }
        else if(Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotation();
        }
    }

    private void StopRotation()
    {
        leftThrusterParticles.Stop();
        rightThrusterParticles.Stop();
    }

    private void RotateRight()
    {
        ApplyRotation(-rotationThrust);
        rightThrusterParticles.Stop();
        if (!leftThrusterParticles.isPlaying)
        {

            leftThrusterParticles.Play();
        }
    }

    private void RotateLeft()
    {
        ApplyRotation(rotationThrust);
        leftThrusterParticles.Stop();
        if (!rightThrusterParticles.isPlaying)
        {

            rightThrusterParticles.Play();
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; //unfreezing roation so physics system can take over
    }

    
}
