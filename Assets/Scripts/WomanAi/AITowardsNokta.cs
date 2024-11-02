using UnityEngine;

public class AITowardsNokta : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        GameObject[] noktalar = GameObject.FindGameObjectsWithTag("nokta");

        if (noktalar.Length == 0)
        {
            return;
        }

        GameObject nearestNokta = noktalar[0];
        float nearestDistance = Vector3.Distance(transform.position, nearestNokta.transform.position);

        foreach (GameObject nokta in noktalar)
        {
            float distance = Vector3.Distance(transform.position, nokta.transform.position);
            if (distance < nearestDistance)
            {
                nearestNokta = nokta;
                nearestDistance = distance;
            }
        }

        transform.position = Vector3.MoveTowards(transform.position, nearestNokta.transform.position, speed * Time.deltaTime);
    }
}