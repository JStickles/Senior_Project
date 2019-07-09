using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    float vertical;
    float horizontal;
    bool runInput;
    bool useInput;
    bool rollInput;

    bool attackInput;

    bool lockOn;

    StateManager state;
    public CameraMonitor cameraMonitor;


    
    // Start is called before the first frame update
    void Start()
    {
        state = GetComponent<StateManager>();
        state.Init();

        cameraMonitor = CameraMonitor.singleton;
        cameraMonitor.Init(this.transform);
    }

    private void FixedUpdate()
    {
        GetInput();
        UpdateStates();
        state.FixedTick(Time.deltaTime);
        cameraMonitor.Tick(Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        state.Tick(Time.deltaTime);
    }

    void GetInput()
    {
        vertical = 0;
        horizontal = 0;
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");
        runInput = Input.GetButton("RunInput");
        attackInput = Input.GetButton("AttackInput");
        lockOn = Input.GetButtonUp("LockOn");
        rollInput = Input.GetButtonDown("Roll");
    }

    void UpdateStates()
    {
        state.horizontal = horizontal;
        state.vertical = vertical;

        Vector3 v = vertical * cameraMonitor.transform.forward;
        Vector3 h = horizontal * cameraMonitor.transform.right;
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
            cameraMonitor.lockOnTarget = state.lockOnTarget.transform;
            cameraMonitor.lockOn = state.lockOn;
        }
    }
}
