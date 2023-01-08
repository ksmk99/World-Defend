using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIRotator : MonoBehaviour
{
    private Transform cameraTransform;

    private void Start()
    {
        CinemachineCore.CameraUpdatedEvent.AddListener(RotateCountText);
        cameraTransform = Camera.main.transform;
    }

    private void RotateCountText(CinemachineBrain brain)
    {
        if (gameObject.activeInHierarchy)
        {
            transform.forward = cameraTransform.forward;
        }
    }
}
