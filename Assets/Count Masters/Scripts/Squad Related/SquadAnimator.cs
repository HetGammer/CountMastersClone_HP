using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;

public class SquadAnimator : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private Transform runnersParent;

    private void Awake()
    {
        UIManager.onGameSet += StartRunning;
        UIManager.onLevelCompleteSet += StopRunning;
        UIManager.onMenuSet += StopRunning;
        UIManager.onGameoverSet += StopRunning;
    }

    private void OnDestroy()
    {
        UIManager.onGameSet -= StartRunning;
        UIManager.onLevelCompleteSet -= StopRunning;
        UIManager.onMenuSet -= StopRunning;
        UIManager.onGameoverSet -= StopRunning;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartRunning()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Runner runner = runnersParent.GetChild(i).GetComponent<Runner>();
            runner.StartRunning();
        }
    }

    private void StopRunning(int none = 0)
    {
        StopRunning();
    }

    private void StopRunning()
    {
        for (int i = 0; i < runnersParent.childCount; i++)
        {
            Runner runner = runnersParent.GetChild(i).GetComponent<Runner>();
            runner.StopRunning();
        }
    }

    
}
