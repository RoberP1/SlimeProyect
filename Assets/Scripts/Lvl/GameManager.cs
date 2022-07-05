using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    
    public static event Action OnDiamondSliderFull;
    [Header("Diamonds")]
    [SerializeField] private Slider diamondSlider;

    [Header("Letters")]
    [SerializeField] private Color color;
    [SerializeField] private Image[] letters;

    public static event Action OnPause;
    void Start()
    {
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
            OnPause?.Invoke();
        }
    }

    public void AddDiamond()
    {
        Debug.Log("Aggarro un diamante");

        if (diamondSlider.value < diamondSlider.maxValue)
        {
            diamondSlider.value++;
        }
        else
        {
            OnDiamondSliderFull?.Invoke();
        }
    }
    public void AddLetter(int id)
    {
        letters[id].color = color;
        PlayerPrefs.SetInt("Letter" + id, 1);
    }

    public void OnEnable()
    {
        Diamond.collectDiamond += AddDiamond;
        Letter.collectLetter += AddLetter;
    }
    private void OnDisable()
    {
        Diamond.collectDiamond -= AddDiamond;
        Letter.collectLetter -= AddLetter;
    }

}
