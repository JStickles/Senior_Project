using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float lookSpeed;

    bool lockOn;

    Vector3 offset;
    Quaternion rotation;
    float updateRotationAmount;

    // Start is called before the first frame update
    void Start()
    {
        offset = new Vector3(0, 0, 5);
        updateRotationAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        offset = 5 * player.transform.forward;
        transform.position = player.transform.position - offset + new Vector3(0, 4, 0);
        transform.forward = (player.transform.position - transform.position).normalized + new Vector3(0, .3f, 0);
    }
}
