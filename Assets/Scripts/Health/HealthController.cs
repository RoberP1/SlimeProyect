using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Heart[] health;
    public bool canTakeDamage = true;
    public int MaxHealth;
    public int Health;


    void Start()
    {
        health = GetComponentsInChildren<Heart>();
    }
    
    public void LoseHealth()
    {
        if (!canTakeDamage) return;
        int i = 0;
        for (i = health.Length-1; i >=0; i--)
        {
            if (health[i].state >0)
            {
                break;
            }
        }
        Health--;
        health[i].state--;
        health[i].SetSprite();
        canTakeDamage = false;

    }
    public void Heal(GameObject diamond,Slider slider)
    {
        if (Health >= MaxHealth) return;
        Health++;
        int i = 0;
        for (i = 0; i < health.Length; i++)
        {
            if (health[i].state < 2)
            {
                break;
            }
        }
        health[i].state++;
        health[i].SetSprite();
        slider.value = 0;
        Destroy(diamond.gameObject);
    }

    public void OnEnable()
    {
        Player.OnTakeDamage += LoseHealth;
        Player.OnHitFinish += () => canTakeDamage = true;
        GameManager.OnDiamondSliderFull += Heal;
    }
    private void OnDisable()
    {
        Player.OnTakeDamage -= LoseHealth;
    }

}
