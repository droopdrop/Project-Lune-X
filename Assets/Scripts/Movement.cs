using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour 
{
    Rigidbody rb;
    AudioSource audioSource;

    [SerializeField]float PrimaryThrust = 1000f;
    [SerializeField]float SideThrust = 300f;
    void Start()
    {   
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        rb.drag = 1;
    }
    
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * PrimaryThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }
       
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(SideThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-SideThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false; // Unfreezing rotation so the physics system can take over.
    }
}