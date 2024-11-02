using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QTEKey : MonoBehaviour
{
    private QTE qte;
    private string keyChar;

    public TextMeshProUGUI text;

    private void Start()
    {
        qte = GetComponent<QTE>();
        //Debug.Log("içerde");

        keyChar = qte.keyCode[qte.keyCodeIndex].ToString();
        //Debug.Log("içerde harf: "+ keyChar);
        
        text.text = keyChar;
        
    }
    
}
