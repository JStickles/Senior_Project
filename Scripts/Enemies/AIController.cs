using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    
    

    GameObject player;
    CharacterController controller;
    Animator animator;
    StateManager state;
    AITypes artInt;

    public float vertical;
    public float horizontal;
    public bool runInput;
    public bool useInput;
    public bool rollInput;

    public bool attackInput;

    public bool lockOn;

    // Start is called before the first frame update
    void Start()
    {
        
        player = GameObject.FindGameObjectWithTag("Player");
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        vertical = 0;
        horizontal = 0;
        state = GetComponent<StateManager>();
        state.Init();
        artInt = GetComponent<AITypes>();
    }

    private void FixedUpdate()
    {
        GetInput();
        UpdateStates();
        state.FixedTick(Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        state.Tick(Time.deltaTime);
    }

    void GetInput()
    {
        vertical = artInt.GetVertical();
        horizontal = artInt.GetHorizontal();
        runInput = artInt.GetRun();
        attackInput = artInt.GetAttack();
        if (state.inAction)
        {
            attackInput = false;
        }
        lockOn = artInt.GetLock();
        if (state.lockOn)
        {
            lockOn = false;
        }
        rollInput = artInt.GetRoll();
    }
    // Update is called once per frame
    /*void Update()
    {
        float distanceToPlayer = (player.transform.position - transform.position).magnitude;
        if (distanceToPlayer < 10)
        {
            attackMode = true;
        }
        if (attackMode)
        {
            //attackMove();
            WaypointMove();
        }
        else
        {
            WaypointMove();
        }
        animator.SetFloat("vertical", vertical);
        animator.SetFloat("horizontal", horizontal);
    }*/

    void attackMove()
    {

    }
    /*
    void WaypointMove()
    {
        dir = transform.forward;
        dir *= speedAmount * Time.deltaTime;
        dir.y = 0;
        controller.Move(dir);
        distanceToTarget = (transform.position - target).magnitude;
        vertical = .4f;
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
    }*/

    void UpdateStates()
    {
        state.horizontal = horizontal;
        state.vertical = vertical;

        Vector3 v = vertical * this.gameObject.transform.forward;
        Vector3 h = horizontal * this.gameObject.transform.right;
        state.moveDirection = (v + h).normalized;
        float m = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
        state.moveAmount = Mathf.Clamp(m, 0, 1);

        if (runInput)
        {
            if (state.moveAmount > 0)
            {
                state.run = true;
            }
        }
        else
        {
            state.run = false;
        }
        state.roll = rollInput;

        state.attack = attackInput;

        if (lockOn)
        {
            state.lockOn = !state.lockOn;
            if (state.lockOnTarget == null)
            {
                state.lockOn = false;
            }
        }
    }
}
