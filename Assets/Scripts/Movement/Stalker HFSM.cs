using UnityEngine;
using UnityHFSM;
using System.Collections;
using System.Collections.Generic;

public class StalkerHFSM : MonoBehaviour
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


    void Awake()
    {
        if (camera == null)
        {
            camera = Camera.main.gameObject;
        }
        
        camDif = camera.transform.position - transform.position;
        
        rigidbody = GetComponent<Rigidbody>();
        
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity = new Vector2(Input.GetAxis("Horizontal") * targetMovingSpeed,
            Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }

    void CamPositioner()
    {
       
    }
    
    
}
