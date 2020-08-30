using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;

public class CameraSettings : NetworkBehaviour
{
    public GameObject CameraMountPoint;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponentInChildren<Camera>();
        if (isLocalPlayer)
        {
            
            //Transform cameraTransform = Camera.main.gameObject.transform;  //Find main camera which is part of the scene instead of the prefab
            //cameraTransform.parent = CameraMountPoint.transform;  //Make the camera a child of the mount point
            //cameraTransform.position = CameraMountPoint.transform.position;  //Set position/rotation same as the mount point
            //cameraTransform.rotation = CameraMountPoint.transform.rotation;
            //cameraTransform.position.y = 1.35;
        }
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            if (!_camera.enabled)
                _camera.enabled = true;
        }
        else
        {
            _camera.enabled = false;
        }
    }
}
