using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class SquadController : MonoBehaviour
{
    [Header(" Managers ")]
    [SerializeField] private SquadFormation squadFormation;

    [Header(" Movement Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveCoefficient;
    [SerializeField] private float platformWidth;
    private float currentMoveSpeed;
    
    private Vector3 clickedPosition;
    private Vector3 initialPosition;
    private bool canControl;

    private void Awake()
    {
        SquadDetection.OnEnemiesDetected += SlowDown;
        SquadDetection.OnNoEnemiesDetected += SpeedUp;

        SquadDetection.OnFinishLineCrossed += FinishLineCrossedCallback;
    }

    private void OnDestroy()
    {
        SquadDetection.OnEnemiesDetected -= SlowDown;
        SquadDetection.OnNoEnemiesDetected -= SpeedUp;
    
        SquadDetection.OnFinishLineCrossed -= FinishLineCrossedCallback;
    }

    // Start is called before the first frame update
    void Start()
    {
        canControl = true;

        currentMoveSpeed = moveSpeed;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    
    void Update()
    {
        if (UIManager.IsGame())
            MoveForward();

        if (!UIManager.IsGame()) return;

        UpdateProgressBar();
    }

    public void StoreClickedPosition()
    {
        clickedPosition = transform.position;        
    }

    public void GetSlideValue(Vector2 slideInput)
    {
        if(!canControl)
            return;

        slideInput.x *= moveCoefficient;
        float targetX = clickedPosition.x + slideInput.x;

        float maxX = platformWidth / 2 - squadFormation.GetSquadRadius();

        targetX = Mathf.Clamp(targetX, -maxX, maxX);

        transform.position = transform.position.With(x: Mathf.Lerp(transform.position.x, targetX, 0.3f));
    }

    private void MoveForward()
    {
        transform.position += Vector3.forward * currentMoveSpeed * Time.deltaTime;
    }

    private void SpeedUp()
    {
        currentMoveSpeed = moveSpeed;
    }

    private void SlowDown()
    {
        currentMoveSpeed = moveSpeed / 4f;
    }

    private void FinishLineCrossedCallback()
    {
        PreventControl();
        transform.position = transform.position.With(x: 0);
    }

    private void PreventControl()
    {
        canControl = false;
    }

    private void UpdateProgressBar()
    {
        float initialDistanceToFinish = RoadManager.GetFinishPosition().z - initialPosition.z;
        float currentDistanceToFinish = RoadManager.GetFinishPosition().z - transform.position.z;
        float distanceLeftToFinish = initialDistanceToFinish - currentDistanceToFinish;

        float progress = distanceLeftToFinish / initialDistanceToFinish;
        UIManager.updateProgressBarDelegate?.Invoke(progress);
    }
}
