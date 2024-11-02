using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingBehindWoman : MonoBehaviour
{
    private GameObject woman;
    private GameManager gameManager;
    void Start()
    {
        woman = GameObject.FindWithTag("Woman");
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        StartCoroutine(WomanToggle(2));
    }
    
    void Update()
    {
    }

    private IEnumerator WomanToggle(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameManager.ToggleWoman();
        Destroy(gameObject);
    }
}
