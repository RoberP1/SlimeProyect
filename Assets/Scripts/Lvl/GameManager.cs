using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public enum States
    {
        Playing,
        Pause
    }
    public States state;

    public static event Action OnDiamondSliderFull;
    [Header("Diamonds")]
    [SerializeField] private Slider diamondSlider;

    [Header("Letters")]
    [SerializeField] private Color color;
    [SerializeField] private Image[] letters;

    [Header("CheckPoints")]
    [SerializeField] private List<Vector3> Checkpoits = new List<Vector3>();

    public static event Action OnPause;

    void Start()
    {
        ChangeState(States.Playing);
        Checkpoits.Add(GameObject.FindGameObjectWithTag("Player").transform.position);
        //PauseMenu.SetActive(false);
        diamondSlider.value = 0;
        for (int i = 0; i < letters.Length; i++)
        {
            if(PlayerPrefs.GetInt("Letter"+i,0) == 1)
                letters[i].color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("r"))
        {
            PlayerPrefs.DeleteAll();
        }
        if (Input.GetKeyDown("p"))
        {
            ChangeState(States.Pause);
        }

    }

    public void AddDiamond()
    {
        Debug.Log("Aggarro un diamante");

        if (diamondSlider.value < diamondSlider.maxValue)
        {
            diamondSlider.value++;
        }
        if (diamondSlider.value == diamondSlider.maxValue)
        {
            OnDiamondSliderFull?.Invoke();
        }
    }
    public void AddLetter(int id)
    {
        letters[id].color = color;
        //PlayerPrefs.SetInt("Letter" + id, 1);
    }
    private void AddCheckPoint(Vector3 checkpoint)
    {
        Checkpoits.Add(checkpoint);
    }
    private void Die()
    {
        diamondSlider.value = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //GameObject.FindWithTag("Player").transform.position = Checkpoits[Checkpoits.Count - 1];
    }
    private void Checkpoint()
    {
        FindObjectOfType<Player>().transform.position = Checkpoits[Checkpoits.Count - 1];
    }

    public void ChangeState(States newState)
    {
        state = newState;
        switch (state)
        {
            case States.Playing:
                Time.timeScale = 1;
                break;
            case States.Pause:
                OnPause?.Invoke();
                break;
            default:
                break;
        }
    }
    void Pause()
    {
        Time.timeScale = 0;
    }
    public void OnEnable()
    {
        Player.OnInstaDead += Checkpoint;
        Diamond.collectDiamond += AddDiamond;
        Letter.collectLetter += AddLetter;
        CheckPoint.OnCheckPoint += AddCheckPoint;
        HealthController.OnDead += Die;
        OnPause += Pause;
        PauseMenu.OnUnpause += ChangeState;
    }

    

    private void OnDisable()
    {
        Player.OnInstaDead -= Checkpoint;
        Diamond.collectDiamond -= AddDiamond;
        Letter.collectLetter -= AddLetter;
        CheckPoint.OnCheckPoint -= AddCheckPoint;
        HealthController.OnDead -= Die;
        PauseMenu.OnUnpause -= ChangeState;
        OnPause -= Pause;
    }


}
