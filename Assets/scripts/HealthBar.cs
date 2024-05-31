using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour

{
    public Slider healthSlider;
    public TextMeshProUGUI healthBarText;

    Damageable PlayerDamageable;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        PlayerDamageable = player.GetComponent<Damageable>();
    }
    void Start()
    {
        healthSlider.value = calculateSliderPercentage(PlayerDamageable.Health, PlayerDamageable.MaxHealth);
        healthBarText.text="Health "+ PlayerDamageable.Health + "/"+PlayerDamageable.MaxHealth;
    }
    private void OnEnable()
    {
        PlayerDamageable.healthChanged.AddListener(OnHealthChanged);
    }
    private void OnDisable()
    {
        PlayerDamageable.healthChanged.RemoveListener(OnHealthChanged);
    }
    private float calculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }
    private void OnHealthChanged(int newHealth,int maxHealth)
    {
        healthSlider.value = calculateSliderPercentage(newHealth,maxHealth);
        healthBarText.text = "Health " + newHealth + "/" + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
