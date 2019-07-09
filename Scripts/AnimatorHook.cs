using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorHook : MonoBehaviour
{
    Animator animator;
    StateManager state;

    public void Init(StateManager states)
    {
        state = states;
        animator = states.animator;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnAnimatorMove()
    {
        if (!state.inAction)
        {
            return;
        }

        state.rigid.drag = 0;
        float multiplier = 1;

        Vector3 delta = animator.deltaPosition;
        delta.y = 0;
        Vector3 velocity = (delta * multiplier) / Time.deltaTime;
        state.rigid.velocity = velocity;
    }
}
