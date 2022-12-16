using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    
    public float maxHealth = 1000.0f;
    public float damage = 20.0f;
    public float speed = 20.0f;
    public int coins = 0;
    public float health;
}
