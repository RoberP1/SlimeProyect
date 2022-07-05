using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour
{
    public Heart[] health;
    [SerializeField] private Slider diamondSlider;
    public bool canTakeDamage = true;
    public bool diamondSliderFull = false;
    public int MaxHealth;
    public int Health;
    public static Action OnDead;

    void Start()
    {
        health = GetComponentsInChildren<Heart>();
        OnDead += ResetHealth;
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
        if (diamondSliderFull)
        {
            Heal();
        }

        if (Health <= 0)
        {
            
            OnDead?.Invoke();
        }

    }
    public void InstaDead()
    {
        OnDead?.Invoke();
    }
    public void Heal()
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
        diamondSlider.value = 0;
        diamondSliderFull = false;
    }
    public void ResetHealth()
    {
        Health = MaxHealth;

        foreach (var heart in health)
        {
            heart.state = 2;
            heart.SetSprite();
        }

    }

    public void OnEnable()
    {
        Player.OnTakeDamage += LoseHealth;
        Player.OnInstaDead += InstaDead;
        Player.OnHitFinish += () => canTakeDamage = true;
        GameManager.OnDiamondSliderFull += Heal;
        GameManager.OnDiamondSliderFull += () => diamondSliderFull = true;
    }
    private void OnDisable()
    {
        Player.OnTakeDamage -= LoseHealth;
        GameManager.OnDiamondSliderFull -= Heal;
        Player.OnInstaDead -= InstaDead;
    }

}
