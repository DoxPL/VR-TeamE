using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : MonoBehaviour
{
    [Header("UserInterface")]
    public GameObject container;
    public const float ROTATION_SPEED = 180f;

    [Header("Game")]
    public int health = 30;

    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime);
    }
}
