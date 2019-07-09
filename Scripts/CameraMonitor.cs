using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMonitor : MonoBehaviour
{
    public static CameraMonitor singleton;

    public float followSpeed = 9;
    public float mouseSpeed = 2;
    public float controllerSpeed = 7;

    public Transform target;
    public Transform lockOnTarget;

    public Transform pivot;
    public Transform camTrans;

    float turnSmoothing = .1f;
    public float minAngle = -35;
    public float maxAngle = 35;
    float smoothX;
    float smoothY;
    float smoothXvelocity;
    float smoothYvelocity;

    public bool lockOn;
    public float lookAngle;
    public float tiltAngle;

    private void Awake()
    {
        singleton = this;
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
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        float controllerHorizontal = Input.GetAxis("RightAxis X");
        float controllerVertical = Input.GetAxis("RightAxis Y");

        float targetSpeed = mouseSpeed;

        if (controllerHorizontal != 0 || controllerVertical != 0)
        {
            horizontal = controllerHorizontal;
            vertical = controllerVertical;
            targetSpeed = controllerSpeed;
        }
        FollowTarget();
        HandleRotations(d, vertical, horizontal, targetSpeed);
    }

    public void Init(Transform t)
    {
        target = t;

        camTrans = Camera.main.transform;
        pivot = camTrans.parent;
    }

    void FollowTarget()
    {
        float speed = Time.deltaTime * followSpeed;
        Vector3 targetPosition = Vector3.Lerp(transform.position, target.position, Time.deltaTime * speed);
        transform.position = targetPosition;
    }

    void HandleRotations(float d, float vertical, float horizontal, float targetSpeed)
    {
        if (turnSmoothing > 0)
        {
            smoothX = Mathf.SmoothDamp(smoothX, horizontal, ref smoothXvelocity, turnSmoothing);
            smoothY = Mathf.SmoothDamp(smoothY, vertical, ref smoothYvelocity, turnSmoothing);
        }
        else
        {
            smoothX = horizontal;
            smoothY = vertical;
        }

        tiltAngle -= smoothY * targetSpeed;
        tiltAngle = Mathf.Clamp(tiltAngle, minAngle, maxAngle);
        pivot.localRotation = Quaternion.Euler(tiltAngle, 0, 0);

        if (lockOn && lockOnTarget != null)
        {
            Vector3 targetDirection = lockOnTarget.position - transform.position;
            targetDirection.Normalize();
            //targetDirection.y = 0;

            if (targetDirection == Vector3.zero)
            {
                targetDirection = transform.forward;
            }
            Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, d * 9);
            lookAngle = transform.eulerAngles.y;

            return;
        }

        lookAngle += smoothX * targetSpeed;
        transform.rotation = Quaternion.Euler(0, lookAngle, 0);
    }
}
