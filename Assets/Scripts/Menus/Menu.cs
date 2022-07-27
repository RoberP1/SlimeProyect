using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string levelChange;

    string Level;
    [SerializeField] GameObject Levels, MenuPrincipal;

    public void EscenaJuego()
    {
        SceneManager.LoadScene(levelChange);
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape"))
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void ShowLevels()
    {
        Levels.SetActive(true);
        MenuPrincipal.SetActive(false);
    }

    public void ShowMenu()
    {
        Levels.SetActive(false);
        MenuPrincipal.SetActive(true);
    }

    public void ChangeLevel(string Level)
    {
        SceneManager.LoadScene(Level);
    }
}
