using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GroupSpawner : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private int amount;
    [SerializeField] private Transform parent;

    [Header(" Positioning ")]
    private float angleFactor = 1f;
    private float radiusFactor = .324f;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = GetFermatPosition(i);
            Instantiate(objectToSpawn, spawnPosition, Quaternion.identity, parent);        
        }
    }

    private void Update()
    {
        if (parent.childCount <= 0)
            Destroy(gameObject);
    }

        private Vector3 GetFermatPosition(int index)
    {
        float goldenAngle = 137.5f * angleFactor;  

        float x = radiusFactor * Mathf.Sqrt(index + 1) * Mathf.Cos(Mathf.Deg2Rad * goldenAngle * (index + 1));
        float z = radiusFactor * Mathf.Sqrt(index + 1) * Mathf.Sin(Mathf.Deg2Rad * goldenAngle * (index + 1));

        Vector3 runnerLocalPosition = new Vector3(x, 0, z);
        Vector3 runnerTargetWorldPosition = transform.TransformPoint(runnerLocalPosition);

        return runnerTargetWorldPosition;
    }
}
