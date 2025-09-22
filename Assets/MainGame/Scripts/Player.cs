using UnityEngine;

[AddComponentMenu("MainGame/Player")]

public class Player : MonoBehaviour, ICanTakeDamage
{
    public int maxHealth = 100;
    public bool isDead = false;

    private int currentHealth;
    private float timeDelay = 1f;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void TakeDamage(int damageAmount, Vector2 HitPoint, GameObject hitDirection)
    {
        if (isDead) return;
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            isDead = true;
            PlayerDown();
        }
    }
    private void PlayerDown()
    {
        
    }
}
