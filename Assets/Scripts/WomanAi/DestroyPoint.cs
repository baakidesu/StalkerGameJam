using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Pool;

public class DestroyPoint : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        LeanPool.Despawn(gameObject);
    }
}