using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    bool lockOn;
    bool sprint;
    bool movable;

    GameObject lockOnTarget;

    public float lookSpeed;
    private float updateRotationAmount;
    private float speedAmount;
    public float speed;
    Vector3 dir;

    Animator animator;

    float animationVertical;
    float animationHorizontal;

    CharacterController controller;

    public Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        lockOn = false;
        movable = true;
        sprint = false;
        animator = GetComponent<Animator>();
        animationVertical = 0;
        animationHorizontal = 0;
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = Vector3.zero;
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(transform.position, Vector3.down * 100, Color.red);
        updateRotationAmount = 0;
        speedAmount = 0;
        animationVertical = 0;
        animationHorizontal = 0;
        dir = new Vector3(0, 0, 0);
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            sprint = true;
        }
        else
        {
            sprint = false;
        }
        if (Input.GetKey(KeyCode.W))
        {
            speedAmount = speed * Time.deltaTime;
            dir += Vector3.forward;
            animationVertical += .5f;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            speedAmount = speed * Time.deltaTime;
            dir += Vector3.back;
            animationVertical -= .5f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            speedAmount = speed * Time.deltaTime;
            dir += Vector3.left;
            animationHorizontal += -.5f;
        }
        if (Input.GetKey(KeyCode.D))
        {
            speedAmount = speed * Time.deltaTime;
            dir += Vector3.right;
            animationHorizontal += .5f;
        }
        if (sprint)
        {
            speedAmount = speedAmount * 2;
            animationVertical *= 2;
            animationHorizontal *= 2;
        }
        if (!movable)
        {
            
        }
        if (!lockOn)
        {
            float y = lookSpeed * Input.GetAxis("Mouse X");
            updateRotationAmount += y * Time.deltaTime;
        }
        else
        {
            Vector3 lookAt = lockOnTarget.transform.position - transform.position;
            this.transform.forward = lookAt;
        }
        
        dir.Normalize();
        transform.Rotate(0, updateRotationAmount, 0);
        //if (Physics.Raycast(ray, out hit, 1f))
        //{
        //    Debug.Log("Ray info: " + hit.point.y);
        //}
        dir = transform.TransformDirection(dir);
        dir *= speedAmount;
        controller.Move(dir);
        Vector3 pos = transform.position;
        //pos.y = 1.02f + hit.point.y;
        //transform.position = pos;
        animator.SetFloat("vertical", animationVertical);
        animator.SetFloat("horizontal", animationHorizontal);
    }
}
