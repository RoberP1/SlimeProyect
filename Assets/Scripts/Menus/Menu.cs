using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    int lvlsUnlocked;

    private void Start()
    {
        lvlsUnlocked= PlayerPrefs.GetInt("UnlockedLevel", 1);
        for (int i = 0; i < lvlsUnlocked; i++)
        {
            if(i < buttons.Length)
            buttons[i].interactable = true;
        }
    }
    public void ChangeLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
