using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHide : MonoBehaviour
{
    public Vector3 closestHidingSpot;
    public bool canHide = false;
    void Start()
    {

    }

    void Update()
    {
        if (canHide && Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log("hide");
            transform.position = closestHidingSpot;
        }
    }
    private void OnTriggerEnter(Collider other)
    {      
        foreach (Transform child in other.transform)
        {
            if (child.CompareTag("HideLocation"))
            {
                closestHidingSpot = child.transform.position;
                break;
            }
        }
        canHide = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("HidePlace"))
        {
            canHide = false;
        }
        
    }
}
