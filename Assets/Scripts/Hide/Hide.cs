using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Serialization;

public class Hide : MonoBehaviour
{
    public GameObject keyQSprite;
    public float easeTime = 0.5f;
    public ParticleSystem particleSystem;

    private bool onSite;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && onSite)
        {
            particleSystem.Play();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            keyQSprite.transform.DOScale(new Vector3(0.09402762f, 0.09402762f,0.09402762f), easeTime).SetEase(Ease.InOutBack);
           onSite = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    { 
        if (other.CompareTag("Player"))
        {
            keyQSprite.transform.DOScale(new Vector3(0f, 0f,0f), easeTime).SetEase(Ease.InOutBack);
            onSite = false;
        }
    }
}
