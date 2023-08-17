using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour, ICameraController
{
    [SerializeField]
    private CinemachineVirtualCamera cinemachineVirtualCamera;

    public GameObject Target
    {
        get => cinemachineVirtualCamera.Follow.gameObject;
        set => cinemachineVirtualCamera.Follow = value.transform;
    }
}
