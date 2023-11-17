using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    const float TAU = (float) Math.PI * 2;
    Vector3 startingPosition;
    [SerializeField] Vector3 movementVector;
    float movementFactor;
    [SerializeField] float period = 0f;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(period == 0) //Always use epsilon as weird things can happen with comparing floats
        {
            float cycles = Time.time / period; //continually growing over time

            float rawSinWave = Mathf.Sin(cycles * TAU); //Going from -1 to 1

            movementFactor = (rawSinWave + 1f) / 2f; //Recalculated to go from 0 to 1

            Vector3 offset = movementVector * movementFactor;
            transform.position = startingPosition + offset;
        }
    }
}
