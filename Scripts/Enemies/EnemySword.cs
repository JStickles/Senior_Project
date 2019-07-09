using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySword : MonoBehaviour
{
    AudioSource audioSource;

    private void Awake()
    {
        Messenger.AddListener(GameEvent.ENEMY_SWING, Swing);
        Messenger.AddListener(GameEvent.END_ENEMY_SWING, EndSwing);
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
        GameObject collider = other.gameObject;
        if (collider.tag == "Player")
        {
            audioSource.Play();
            Messenger.Broadcast(GameEvent.PLAYER_DAMAGE);
        }
    }

    private void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.ENEMY_SWING, Swing);
        Messenger.RemoveListener(GameEvent.END_ENEMY_SWING, EndSwing);
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
