using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Drawing;

using Unity.VisualScripting.Antlr3.Runtime.Tree;
using System;



#if UNITY_EDITOR
using UnityEditor;

#endif

public class Saw : MonoBehaviour
{
    private GameObject[] dotsToGo;
    private bool calculateDistance = true;
    private Vector3 distanceBetweenSawAndFirst;

    private int distanceCounter;
    private bool forward;

    public bool canTurn = false;

    void Start()
    {
        dotsToGo = new GameObject[transform.childCount];
        for (int i = 0; i < dotsToGo.Length; i++)
        {
            dotsToGo[i] = transform.GetChild(0).gameObject;
            dotsToGo[i].transform.SetParent(transform.parent);
        }
    }

    void FixedUpdate()
    {
        GoToDots();
        if(canTurn)
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
    }

    void GoToDots()
    {
        if (calculateDistance)
        {
            distanceBetweenSawAndFirst = (dotsToGo[distanceCounter].transform.position - transform.position).normalized;
            calculateDistance = false;
            if (canTurn)
            {
                Debug.LogError(RandomPoint());
            }
        }

        float distance = Vector3.Distance(transform.position, dotsToGo[distanceCounter].transform.position);
        transform.position += distanceBetweenSawAndFirst * Time.deltaTime * 10;

        Vector3 RandomPoint()
        {
            Vector3 noktaA= transform.position;
            Vector3 noktaB = dotsToGo[distanceCounter].transform.position;
            float randomX = UnityEngine.Random.Range(Mathf.Min(noktaA.x, noktaB.x), Mathf.Max(noktaA.x, noktaB.x));
            float randomY = UnityEngine.Random.Range(Mathf.Min(noktaA.y, noktaB.y), Mathf.Max(noktaA.y, noktaB.y));
            float randomZ = UnityEngine.Random.Range(Mathf.Min(noktaA.z, noktaB.z), Mathf.Max(noktaA.z, noktaB.z));

            return new Vector3(randomX, randomY, randomZ);
        }
        if (distance < 0.5f)
        {
            calculateDistance = true;

            if (dotsToGo[distanceCounter].CompareTag("TruePoint"))
            {
                canTurn = true;
            }
            else if (dotsToGo[distanceCounter].CompareTag("FalsePoint"))
            {
                canTurn = false;
            }

            if (distanceCounter == dotsToGo.Length - 1)
            {
                forward = false;
            }
            else if (distanceCounter == 0)
            {
                forward = true;
            }

            if (forward)
            {
                distanceCounter++;
            }
            else
            {
                distanceCounter--;
            }
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Gizmos.color = UnityEngine.Color.red;
            Gizmos.DrawSphere(transform.GetChild(i).position, 1);
        }

        for (int i = 0; i < transform.childCount - 1; i++)
        {
            Gizmos.color = UnityEngine.Color.blue;
            Gizmos.DrawLine(transform.GetChild(i).transform.position, transform.GetChild(i + 1).transform.position);
        }
    }
#endif
}

#if UNITY_EDITOR
[CustomEditor(typeof(Saw))]
[System.Serializable]

class SawEditor : Editor
{
    private int PointCount = 1;  

    public override void OnInspectorGUI()
    {
        Saw script = (Saw)target;

        if (GUILayout.Button("Create True Point", GUILayout.MinHeight(50), GUILayout.Width(150), GUILayout.Height(50)))
        {
            CreatePoint(script, "TruePoint");
        }

        if (GUILayout.Button("Create False Point", GUILayout.MinHeight(50), GUILayout.Width(150), GUILayout.Height(50)))
        {
            CreatePoint(script, "FalsePoint");
        }
    }

    private void CreatePoint(Saw script, string tag)
    {
        GameObject newGameObject = new GameObject();
        newGameObject.transform.parent = script.transform;
        newGameObject.transform.position = script.transform.position;

        if (tag == "TruePoint")
        {
            newGameObject.name = "TruePoint " + PointCount;
            PointCount++;
        }
        else if (tag == "FalsePoint")
        {
            newGameObject.name = "FalsePoint " + PointCount;
            PointCount++;
        }

        newGameObject.tag = tag;
    }
}
#endif