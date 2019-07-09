using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndStamina : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;
    public int damageValue;

    public RectTransform healthbar;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_DAMAGE, PlayerHit);
        Messenger.AddListener(GameEvent.BONUS_KILL, IncreaseDefense);
    }

    // Start is called before the first frame update
    void Start()
    {
        maxHealth = 300;
        currentHealth = 300;
        damageValue = 105;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void PlayerHit()
    {
        currentHealth = currentHealth - damageValue;
        healthbar.sizeDelta = new Vector2(currentHealth, healthbar.sizeDelta.y);
        if (currentHealth <= 0)
        {
            Messenger.Broadcast(GameEvent.PLAYER_DEATH);
            healthbar.sizeDelta = new Vector2(maxHealth, healthbar.sizeDelta.y);
        }
    }

    void IncreaseDefense()
    {
        damageValue = 45;
    }
}
