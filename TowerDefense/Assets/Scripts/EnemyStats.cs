using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "Scriptable Objects/EnemyStats")]
public class EnemyStats : ScriptableObject
{
    public float maxHealth;
    public float damage;
    public float speed;
    public float attackRate = 4.0f;
    public int worth = 10;
}
