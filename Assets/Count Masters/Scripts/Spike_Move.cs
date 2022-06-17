using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class Spike_Move : MonoBehaviour
{


    [Header(" Movement ")]
    [SerializeField] private Vector2 minMaxX;
    [SerializeField] private float patrolDuration;
    Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.position.With(x: minMaxX.x);
        targetPosition = transform.position.With(x: minMaxX.y);
        MoveToTargetPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void MoveToTargetPosition()
    {
        LeanTween.move(gameObject, targetPosition, patrolDuration).setOnComplete(SetNextTargetPosition);
    }

    private void SetNextTargetPosition()
    {
        if (targetPosition.x == minMaxX.x)
            targetPosition.x = minMaxX.y;
        else
            targetPosition.x = minMaxX.x;

        MoveToTargetPosition();
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        Vector3 p0 = transform.position;
        p0.x = minMaxX.x;

        Vector3 p1 = p0;
        p1.x = minMaxX.y;

        float cubeSize = 0.5f;

        Gizmos.DrawCube(p0, cubeSize * Vector3.one);
        Gizmos.DrawCube(p1, cubeSize * Vector3.one);
    }
}
