using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCheckPointCamera : MonoBehaviour
{
    [SerializeField] int checkpoint;
    private CheckpointCamera CameraCheckpoint;
    private void Start()
    {
        CameraCheckpoint = FindObjectOfType<CheckpointCamera>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CameraCheckpoint.SetCameraSpace(checkpoint);
        }
    }
}
