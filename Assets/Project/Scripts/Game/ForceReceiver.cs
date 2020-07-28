using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceReceiver : MonoBehaviour
{
    public float deceleration = 5f;
    public float mass = 2f;

    private Vector3 intensity;
    private CharacterController characterController;

    void Start()
    {
        intensity = Vector3.zero;
        characterController = GetComponent<CharacterController>();
    }
    
    void Update()
    {
        if (intensity.magnitude > 0.2f)
        {
            characterController.Move(intensity * Time.deltaTime);
        }
        intensity = Vector3.Lerp(intensity, Vector3.zero, deceleration * Time.deltaTime);
    }

    public void AddForce(Vector3 direction, float force)
    {
        intensity += direction.normalized * force / mass;
    }
}
