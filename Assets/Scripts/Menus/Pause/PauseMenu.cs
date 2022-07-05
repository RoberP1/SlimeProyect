using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PauseMenu : MonoBehaviour
{
    Animator anim;
    int OnPauseId;
    void Start()
    {
        anim = GetComponent<Animator>();
        OnPauseId = Animator.StringToHash("OnPause");

    }
    private void OnEnable()
    {
        GameManager.OnPause += AnimationStart;
    }
    private void OnDisable()
    {
        GameManager.OnPause -= AnimationStart;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimationStart()
    {
        anim.SetBool(OnPauseId, true);
    }
    public void AnimationFinish()
    {
        anim.SetBool(OnPauseId, false);
    }
}
