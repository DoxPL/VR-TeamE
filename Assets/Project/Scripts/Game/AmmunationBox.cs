using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmunationBox : MonoBehaviour
{
    [Header("UserInterface")]
    public GameObject container;
    public const float ROTATION_SPEED = 180f;

    [Header("Game")]
    public uint ammunation = 12;

    // Update is called once per frame
    void Update()
    {
        container.transform.Rotate(Vector3.up * ROTATION_SPEED * Time.deltaTime);
    }
}
