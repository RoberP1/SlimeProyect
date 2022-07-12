using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeLvl : MonoBehaviour
{
    private List<int> LetterIds = new List<int>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IPlayer>() != null)
        {
            foreach (var LetterId in LetterIds)
            {
                AddLetter(LetterId);
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
    public void AddLetter(int id)
    {
        PlayerPrefs.SetInt("Letter" + id, 1);
    }
    public void AddLetterId(int id)
    {
        LetterIds.Add(id);
    }
    
    private void OnEnable()
    {
        Letter.collectLetter += AddLetterId;
    }
    private void OnDisable()
    {
        Letter.collectLetter -= AddLetterId;
    }
}
