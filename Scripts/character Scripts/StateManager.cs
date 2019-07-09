using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    public float horizontal;
    public float vertical;
    public Vector3 moveDirection;
    public float moveAmount;
    public bool attack;
    public bool use;

    public float moveSpeed = 2;
    public float runSpeed = 3.5f;
    public float rotateSpeed = 5;
    public float distanceToGround = .5f;

    public bool run;
    public bool onGround;
    public bool lockOn;
    public bool inAction;
    public bool canMove;
    public bool roll;

    public EnemyTarget lockOnTarget;

    float actionDelay;

    public GameObject model;
    public Animator animator;
    public Rigidbody rigid;
    public AnimatorHook hook;

    public float delta;
    public LayerMask ignoreLayers;

    public void Init()
    {
        animator = model.GetComponent<Animator>();
        animator.applyRootMotion = false;
        rigid = GetComponent<Rigidbody>();
        rigid.drag = 4;
        rigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;

        hook = model.AddComponent<AnimatorHook>();
        hook.Init(this);

        gameObject.layer = 8;
        ignoreLayers = ~(1 << 9);
        animator.SetBool("onGround", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Tick(float d)
    {
        delta = d;
        onGround = OnGround();
        animator.SetBool("onGround", onGround);
    }

    public void FixedTick(float d)
    {
        delta = d;

        
        /*if (canMove == false)
        {
            return;
        }*/
        DetectAction();
        if (inAction)
        {
            animator.applyRootMotion = true;
            actionDelay += delta;
            if (actionDelay > 1.5f)
            {
                inAction = false;
                actionDelay = 0;
                if (this.gameObject.tag == "Player")
                {
                    Messenger.Broadcast(GameEvent.END_PLAYER_SWING);
                }
                else if (this.gameObject.tag == "Enemy")
                {
                    Messenger.Broadcast(GameEvent.END_ENEMY_SWING);
                }
            }
            return;
        }

        HandleRollAnimations();

        animator.applyRootMotion = false;

        if (moveAmount > 0 || onGround == false)
        {
            //rigid.drag = 0;
        }
        else
        {
            //rigid.drag = 4;
        }

        float targetSpeed = moveSpeed;
        if (run)
        {
            targetSpeed = runSpeed;
        }

        if (onGround)
        {
            rigid.velocity = moveDirection * (targetSpeed * moveAmount);
        }

        if (run)
        {
            lockOn = false;
        }
        Vector3 targetDir;
        if (!lockOn)
        {
            targetDir = moveDirection;
        }
        else
        {
            targetDir = lockOnTarget.transform.position - transform.position;
        }
        targetDir.y = 0;
        if (targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }
        Quaternion rotation = Quaternion.LookRotation(targetDir);
        Quaternion targetRoation = Quaternion.Slerp(transform.rotation, rotation, delta * moveAmount * rotateSpeed);
        if (this.gameObject.tag == "Player")
        {
            Debug.Log("transform.rotation = " + transform.rotation);
            //Debug.Log("targetRoation = " + targetRoation);
            //Debug.Log("rotation = " + rotation);
            Debug.Log("targetDir = " + targetDir);
        }
        transform.rotation = targetRoation;
        animator.SetBool("lockon", lockOn);
        if (!lockOn)
        {
            HandleMovementAnimations();
        }
        else
        {
            HandleLockOnAnimations(moveDirection);
        }

        
    }

    void HandleMovementAnimations()
    {
        animator.SetBool("run", run);
        animator.SetFloat("vertical", moveAmount, .4f, delta);
    }

    void HandleLockOnAnimations(Vector3 moveDirection)
    {
        Vector3 relativeDirection = transform.InverseTransformDirection(moveDirection);
        animator.SetFloat("vertical", relativeDirection.z, .2f, delta);
        animator.SetFloat("horizontal", relativeDirection.x, .2f, delta);
    }

    void HandleRollAnimations()
    {
        if (!roll)
        {
            return;
        }
        float verticalRoll = vertical;
        float horizontalRoll = horizontal;
        if (!lockOn)
        {
            if (moveAmount > .3f)
            {
                verticalRoll = 1;
            }
            else
            {
                verticalRoll = 0;
            }
            
            horizontalRoll = 0;
        }
        else
        {
            if (Mathf.Abs(verticalRoll) < .3f)
            {
                verticalRoll = 0;
            }
            if (Mathf.Abs(horizontalRoll) < .3f)
            {
                horizontalRoll = 0;
            }
        }

        animator.SetFloat("vertical", verticalRoll);
        animator.SetFloat("horizontal", horizontalRoll);
        Debug.Log(horizontalRoll);
        inAction = true;
        canMove = false;
        animator.CrossFade("Rolls", .4f);
    }

    public bool OnGround()
    {
        bool r = false;

        Vector3 origin = transform.position + (Vector3.up * distanceToGround);
        Vector3 dir = -Vector3.up;
        float distance = distanceToGround + .3f;
        RaycastHit hit;
        if (Physics.Raycast(origin, dir, out hit, distance, ignoreLayers))
        {
            r = true;
            Vector3 targetPosition = hit.point;
            transform.position = targetPosition;
        }
        return r;
    }

    public void DetectAction()
    {
        if (attack == false)
        {
            return;
        }
        else
        {
            string targetAnimation;
            targetAnimation = "oh_attack_1";
            inAction = true;
            canMove = false;
            animator.CrossFade(targetAnimation, .2f);
            if (this.gameObject.tag == "Player")
            {
                Messenger.Broadcast(GameEvent.PLAYER_SWING);
            }
            else if (this.gameObject.tag == "Enemy")
            {
                Messenger.Broadcast(GameEvent.ENEMY_SWING);
            }
            //rigid.velocity = Vector3.zero;
        }
    }
}
