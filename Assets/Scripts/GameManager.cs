using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Health")]
    [SerializeField] private int playerHealth;
    [SerializeField] private int maxPlayerHealth;
    [SerializeField] private Image[] hearts;

    [Header("Diamonds")]
    [SerializeField] private Slider diamondSlider;
    void Start()
    {
        diamondSlider.value = 0;
        playerHealth = maxPlayerHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddDiamond(GameObject diamond)
    {
        Debug.Log("Aggarro un diamante");
        
        if (diamondSlider.value < diamondSlider.maxValue)
        {
            diamondSlider.value++;
            Destroy(diamond.gameObject);
        }
        else if (playerHealth < maxPlayerHealth)
        {
            playerHealth++;
            diamondSlider.value = 0;
            Destroy(diamond.gameObject);
        }
    }
    public void OnEnable()
    {
        Collectable.collect += AddDiamond;
    }
    private void OnDisable()
    {
        Collectable.collect += AddDiamond;
    }

}
