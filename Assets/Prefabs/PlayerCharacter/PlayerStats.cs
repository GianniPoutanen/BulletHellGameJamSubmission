using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public Slider playerHealth;
    public PlayerBehaviour player;

    private void Update()
    {
        playerHealth.value = player.currentHealth / player.maxHealth;
    }
}
