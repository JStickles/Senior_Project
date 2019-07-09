using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITypes : MonoBehaviour
{
    public string type;

    public GameObject[] waypoints;
    public Vector3 target;

    public float distanceToTarget;
    int targetWaypoint;

    public GameObject player;
    public float distanceToPlayer;

    bool run;
    bool roll;
    float vertical;
    float horizontal;
    bool attack;
    bool lockOn;

    public bool attackMode;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = waypoints[0].transform.position;
        transform.forward = (waypoints[1].transform.position - waypoints[0].transform.position).normalized;
        target = waypoints[1].transform.position;
        targetWaypoint = 1;
        attackMode = false;
    }

    // Update is called once per frame
    void Update()
    {
        distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer < 50)
        {
            attackMode = true;
        }
        if (attackMode == true)
        {
            AttackMove();
        }
        else
        {
            WaypointMove();
        }
    }

    public float GetVertical()
    {
        return vertical;
    }

    public float GetHorizontal()
    {
        return horizontal;
    }

    public bool GetRun()
    {
        return run;
    }

    public bool GetAttack()
    {
        return attack;
    }

    public bool GetLock()
    {
        return lockOn;
    }

    public bool GetRoll()
    {
        return roll;
    }

    void WaypointMove()
    {
        run = false;
        roll = false;
        vertical = 1f;
        horizontal = 0;
        attack = false;
        lockOn = false;

        distanceToTarget = (transform.position - target).magnitude;
        if (distanceToTarget < 1)
        {
            if (targetWaypoint == 1)
            {
                targetWaypoint = 0;
            }
            else
            {
                targetWaypoint = 1;
            }
            transform.forward = (waypoints[targetWaypoint].transform.position - transform.position).normalized;
            target = waypoints[targetWaypoint].transform.position;
        }

    }

    void AttackMove()
    {
        transform.forward = (player.transform.position - transform.position).normalized;
        if (distanceToPlayer > 30)
        {
            run = true;
            lockOn = true;
            roll = false;
            vertical = 1f;
            horizontal = 0;
            attack = false;
        }
        else if (distanceToPlayer > 20)
        {
            lockOn = true;
            run = false;
            roll = false;
            vertical = 1f;
            horizontal = 0;
            attack = false;
        }
        else if (distanceToPlayer <= 20 && distanceToPlayer >= 3)
        {
            vertical = .7f;
            lockOn = true;
            run = false;
            roll = false;
            horizontal = 0;
            attack = false;
        }
        else if (distanceToPlayer < 3)
        {
            attack = true;
            lockOn = true;
            run = false;
            roll = false;
            vertical = 0;
            horizontal = 0;
        }
    }
}
