using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PendulumSwing : MonoBehaviour
{
    public float speed = 2f;      
    public float angle = 1f;     

    private Quaternion startRotation;

    void Start()
    {
        startRotation = transform.localRotation;
    }

    void Update()
    {
        float swing = Mathf.Sin(Time.time * speed) * angle;
        transform.localRotation = startRotation * Quaternion.Euler(0, 0, swing);
    }
}
