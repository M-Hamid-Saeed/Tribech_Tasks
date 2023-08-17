using Character_Management;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffScreenIndicatorNew : MonoBehaviour
{
    public Camera gameCamera;
    [SerializeField] GameObject leftPointer;
    [SerializeField] GameObject rightPointer;

    private Dictionary<GameObject, float> enemyAngles = new Dictionary<GameObject, float>();

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Insects"))
        {
            GameObject enemy = other.gameObject;
            if (!enemyAngles.ContainsKey(enemy))
            {
                enemyAngles[enemy] = CalculateAngle(enemy.transform.position);
            }
            UpdateIndicators();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Insects"))
        {
            GameObject enemy = other.gameObject;
            if (enemyAngles.ContainsKey(enemy))
            {
                enemyAngles.Remove(enemy);
            }
            UpdateIndicators();
        }
    }

    private float CalculateAngle(Vector3 targetPosition)
    {
        Vector3 fromPos = gameCamera.transform.position;
        fromPos.z = 0f;
        fromPos.y = 0f;

        Vector3 dir = (targetPosition - fromPos).normalized;
        return Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
    }

    private void Start()
    {
        StartCoroutine(UpdateIndicatorsCoroutine());
    }

    private IEnumerator UpdateIndicatorsCoroutine()
    {
        while (true)
        {
            UpdateIndicators();
            yield return new WaitForSeconds(1f); // Adjust the interval as needed
        }
    }

    private void UpdateIndicators()
    {
        rightPointer.SetActive(false);
        leftPointer.SetActive(false);

        foreach (var kvp in enemyAngles)
        {
            GameObject enemy = kvp.Key;
    
            float angle = kvp.Value;

            // Check if the enemy and its health component are valid
            InsectHealth insectHealth = enemy.GetComponentInParent<InsectHealth>();
            if (enemy != null && insectHealth != null && insectHealth.currentHealth > 0)
            {
                if (angle > 100)
                {
                    leftPointer.SetActive(true);
                }
                else
                {
                    rightPointer.SetActive(true);
                }
            }
        }
    }
}
