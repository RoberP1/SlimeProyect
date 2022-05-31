using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CheckpointCamera : MonoBehaviour
{
    [SerializeField] private GameObject[] colliders;
    [SerializeField] private CinemachineConfiner camera;
    void Start()
    {
        camera = GetComponent<CinemachineConfiner>();
        camera.m_BoundingShape2D = colliders[0].GetComponent<Collider2D>();
    }

    public void SetCameraSpace(int checkpoint)
    {
        camera.m_BoundingShape2D = colliders[checkpoint].GetComponent<Collider2D>();
    }
}
