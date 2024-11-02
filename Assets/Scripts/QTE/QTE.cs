using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using DG.Tweening;


public class QTE : MonoBehaviour
{

    private float qteCooldown;
    public  float qteCooldownDuration;
    
    private bool qteActive;
    private bool qteSuccess;

    private bool qteFailShake;
    private bool qteSuccessShake;

    public int keyCodeIndex;

    [HideInInspector]
    public bool lostStateQTE;
    
    public Image qteImage;

     
    [HideInInspector]
    public KeyCode[] keyCode = { KeyCode.Y, KeyCode.H, KeyCode.U, KeyCode.B, KeyCode.N, KeyCode.J, KeyCode.K, KeyCode.M };    

     
     void Awake()
    {
        qteCooldown = qteCooldownDuration;

        keyCodeIndex = UnityEngine.Random.Range(0, keyCode.Length);
    }

    void Update()
    {
        if(qteActive)
        {
            TriggerQTE();
        }

        if (qteFailShake)
        {
            lostStateQTE = true;
            qteImage.transform.DOShakePosition(1f, 1).OnComplete(() => qteImage.gameObject.SetActive(false)
            );
        }else if (qteSuccessShake)
        {
            qteImage.transform.DOScale(new Vector3(0f, 0f, 0f), 1)
                .OnComplete(() => qteImage.gameObject.SetActive(false));
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("QTE");
            qteImage.gameObject.SetActive(true);
            qteActive = true;
        }
    }

    void TriggerQTE()
    {
        qteCooldown -= Time.deltaTime;

        
        if (qteCooldown <= 0f)
        {
            qteCooldown = qteCooldownDuration;
            if (qteSuccess)
            { 
                Debug.Log("QTE SUCCESS");
            }
            else
            {
                Debug.Log("QTE FAIL");
                qteFailShake = true;
                qteSuccess = false;
            }
            
            qteActive = false;
        }

       if (Input.GetKeyDown(keyCode[keyCodeIndex]))
        {
            qteSuccessShake = true;
            qteSuccess = true;
        }
    }
}
