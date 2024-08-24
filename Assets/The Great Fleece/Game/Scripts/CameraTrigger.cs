using System;
using System.Collections;
using System.Collections.Generic;
using The_Great_Fleece.Game.Scripts;
using UnityEngine;

public class CameraTrigger : MonoBehaviour
{
    [SerializeField] private CameraTransformSettings _cameraSettings;
    private Transform cameraTransform;

    public void OnEnable()
    {
        cameraTransform = Camera.main.transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Player>() == null)
            return;
        var t = _cameraSettings.GetCameraTransformForTrigger(gameObject);
        //Debug.LogError(t.position);
        cameraTransform.position = t.position;
        cameraTransform.rotation = t.rotation;
    }
}
