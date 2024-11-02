using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityHFSM;

public class WalkState : StateBase
{ 
    #region Private
    
    private Rigidbody rigidbody;
    private Vector3 camDif;

    #endregion
    
    #region Public

    public float speed = 5;

    [Header("Running")] 
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    [Header("Camera")] 
    public GameObject camera;
    public float smoothTime = 0.125f;

    #endregion
    
    private StalkerHFSM hfsm;
    public WalkState(StalkerHFSM context,bool needsExitTime, bool isGhostState = false) : base(needsExitTime, isGhostState)
    {
        hfsm = context;
    }
    public override void OnEnter()
    {
        base.OnEnter();
        //camera
        if (camera == null)
        {
            camera = Camera.main.gameObject;
        }
        
        camDif = camera.transform.position - hfsm.transform.position;
        
        rigidbody = hfsm.GetComponent<Rigidbody>();
        
    }
    
    void OnLogic()
    {
        IsRunning = canRun && Input.GetKey(runningKey);

        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed,
            Input.GetAxis("Vertical") * targetMovingSpeed);

        rigidbody.velocity = hfsm.transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
    void CamPositioner()
    {
        Vector3 desiredPosition = hfsm.transform.position + camDif;

        Vector3 smoothedPosition = Vector3.Lerp(camera.transform.position, desiredPosition, smoothTime);
        camera.transform.position = smoothedPosition;

        camera.transform.LookAt(hfsm.transform);
    }
}
