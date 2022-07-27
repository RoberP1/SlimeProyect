using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
public class PauseMenu : MonoBehaviour
{
    public static event Action<GameManager.States> OnUnpause;
    Animator anim;
    int OnPauseId;
    public Slider Vol;
    void Start()
    {
        Vol.value = PlayerPrefs.GetFloat("vol", 1);
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
        OnUnpause?.Invoke(GameManager.States.Playing);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void ChangeVolume()
    {
        AudioListener.volume = Vol.value;
        PlayerPrefs.SetFloat("vol", Vol.value);
    }
}
