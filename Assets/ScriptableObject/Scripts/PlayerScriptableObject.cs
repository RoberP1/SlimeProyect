using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerScriptableObject", menuName = "ScriptableObjects/PlayerScriptableObject")]
public class PlayerScriptableObject : ScriptableObject
{

    [Header("Movement")]
    [SerializeField] public float speed;


    [Header("Jump")]
    [SerializeField] public float jumpForce;
    [SerializeField] public float rayCastDistance;
    [SerializeField] public int maxJump;
    [SerializeField] public int jump;

    [SerializeField] public LayerMask graund;

    [Header("Dash")]
    [SerializeField] public float dashImpulse;
    [SerializeField] public float dashCD;

    [Header("Audio")]
    [SerializeField] public AudioClip jumpClip;
    [SerializeField] public AudioClip takeDamgeClip;
    [SerializeField] public AudioClip dashClip;
    
}
