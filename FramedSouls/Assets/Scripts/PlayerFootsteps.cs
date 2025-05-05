using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource footStepsAudio;
    public float stepInterval = 0.5f;

    private CharacterController controller;
    private float stepTimer;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.velocity.magnitude>0.1f)
        {
            stepTimer += Time.deltaTime;

            if (stepTimer >= stepInterval)
            {
                footStepsAudio.Play();
                stepTimer = 0f;
            }
        }

        else
        {
            stepTimer = stepInterval;
        }
    }
}
