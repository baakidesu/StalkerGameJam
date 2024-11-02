using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject woman;
    public GameObject womanBehind;

    private Saw saw;
    public bool isLookingBack = false;
    void Start()
    {
        saw = woman.GetComponent<Saw>();
    }

    void Update()
    {
        if (Vector3.Distance(woman.transform.position, saw.lookBackPoint) < 0.2f)
        {
            ToggleWoman();
            Debug.LogError(Vector3.Distance(woman.transform.position, saw.lookBackPoint));
            saw.lookBackPoint = new Vector3(9999, 9999, 9999);
        }
    }
    public void ToggleWoman()
    {
        if (woman != null)
        {
            if (!woman.active)
            {
                woman.SetActive(true);
                isLookingBack = true;

            }
            else
            {
                Instantiate(womanBehind, woman.transform.position, woman.transform.rotation);
                woman.SetActive(false);
                isLookingBack = !isLookingBack;
            }
        }
    }
}