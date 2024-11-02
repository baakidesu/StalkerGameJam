using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Saw : MonoBehaviour
{
    private GameObject[] dotsToGo;
    private bool calculateDistance = true;
    private Vector3 distanceBetweenSawAndFirst;
    public Vector3 lookBackPoint = new Vector3(99999, 99999, 99999);

    private int distanceCounter;
    private bool forward;
    public bool isLookingBack;
    private bool isPaused;
    private float pauseDuration;
    public float lookBackAmount;

    public bool canTurn = false;

    void Start()
    {
        lookBackAmount = 4f;
        lookBackPoint = new Vector3(99999, 99999, 99999);

        // Sadece "TruePoint" ve "FalsePoint" tag'li çocukları al
        dotsToGo = transform.Cast<Transform>()
            .Where(child => child.CompareTag("TruePoint") || child.CompareTag("FalsePoint"))
            .Select(child => child.gameObject)
            .ToArray();

        // dotsToGo dizisini başlangıçta parent objeden ayır
        foreach (var dot in dotsToGo)
        {
            dot.transform.SetParent(transform.parent);
        }
    }

    void FixedUpdate()
    {
        if (isLookingBack && !isPaused)
        {
            StartCoroutine(PauseMovement(lookBackAmount));
        }

        if (!isPaused)
        {
            GoToDots();
        }

        if (canTurn)
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
    }

    IEnumerator PauseMovement(float duration)
    {
        isPaused = true;
        pauseDuration = duration;
        yield return new WaitForSeconds(duration);
        isPaused = false;
    }

    public void GoToDots()
    {
        if (calculateDistance)
        {
            distanceBetweenSawAndFirst = (dotsToGo[distanceCounter].transform.position - transform.position).normalized;
            calculateDistance = false;
            if (canTurn)
            {
                Debug.LogError(RandomPoint());
                lookBackPoint = RandomPoint();
            }
        }

        float distance = Vector3.Distance(transform.position, dotsToGo[distanceCounter].transform.position);
        transform.position += distanceBetweenSawAndFirst * Time.deltaTime * 10;

        // Make the saw look at the target point
        transform.LookAt(dotsToGo[distanceCounter].transform.position);

        Vector3 RandomPoint()
        {
            Vector3 noktaA = transform.position;
            Vector3 noktaB = dotsToGo[distanceCounter].transform.position;

            float t = UnityEngine.Random.Range(0f, 1f);

            return Vector3.Lerp(noktaA, noktaB, t);
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
        // Sadece "TruePoint" ve "FalsePoint" tag'li çocukları çiz
        var filteredChildren = transform.Cast<Transform>()
            .Where(child => child.CompareTag("TruePoint") || child.CompareTag("FalsePoint"))
            .ToArray();

        for (int i = 0; i < filteredChildren.Length; i++)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(filteredChildren[i].position, 1);

            if (i < filteredChildren.Length - 1)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawLine(filteredChildren[i].transform.position, filteredChildren[i + 1].transform.position);
            }
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