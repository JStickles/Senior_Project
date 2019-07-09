using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSword : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.PLAYER_SWING, Swing);
        Messenger.AddListener(GameEvent.END_PLAYER_SWING, EndSwing);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GetComponent<CapsuleCollider>().isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collision");
        GameObject collider = other.gameObject;
        Debug.Log(collider.tag);
        if (collider.tag == "Enemy")
        {
            audioSource.Play();
            GameObject.Destroy(collider);
            Messenger.Broadcast(GameEvent.ENEMY_KILL);
        }
        if (collider.tag == "Boss")
        {
            audioSource.Play();
            GameObject.Destroy(collider);
            Messenger.Broadcast(GameEvent.BOSS_KILL);
        }
        if (collider.tag == "BonusEnemy")
        {
            audioSource.Play();
            GameObject.Destroy(collider);
            Messenger.Broadcast(GameEvent.BONUS_KILL);
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.PLAYER_SWING, Swing);
        Messenger.RemoveListener(GameEvent.END_PLAYER_SWING, EndSwing);
    }

    void Swing()
    {
        GetComponent<CapsuleCollider>().isTrigger = true;
    }

    void EndSwing()
    {
        GetComponent<CapsuleCollider>().isTrigger = false;
    }
}
