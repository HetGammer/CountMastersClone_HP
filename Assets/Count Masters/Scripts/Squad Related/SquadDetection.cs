using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;
using UnityEngine.Events;

public class SquadDetection : MonoBehaviour
{
    [Header(" Events ")]
    public static UnityAction OnEnemiesDetected;
    public static UnityAction OnNoEnemiesDetected;
    public static UnityAction<int> OnRunnersAdded;
    public static UnityAction OnFinishLineCrossed;

    [Header(" Managers ")]
    [SerializeField] private SquadFormation squadFormation;
    [SerializeField] private Runner runner;

    [Header(" Settings ")]
    [SerializeField] private LayerMask doorLayer;
    [SerializeField] private LayerMask finishLayer;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private LayerMask enemiesLayer;
    [SerializeField] private float enemiesDetectionRadius;
    private bool previousEnemiesDetected;
    private bool finishLineDetected;
   

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (UIManager.IsGame())
        {
            if(!finishLineDetected)
                DetectFinishLine();

            DetectDoors();
            DetectionObstacles();
            DetectEnemies();
        }
    }

    

    private void DetectDoors()
    {
        Collider[] detectedDoors = Physics.OverlapSphere(transform.position, squadFormation.GetSquadRadius(), doorLayer);

        if (detectedDoors.Length <= 0) return;

        Collider collidedDoorCollider = detectedDoors[0];
        Door collidedDoor = collidedDoorCollider.GetComponentInParent<Door>();

        //int countRunner=0;
        //Debug.Log("transform.childCount =" + transform.childCount);
        //for(int i=0;i< transform.childCount; i++)
        //{
        //    if(transform.GetChild(i).tag == "Player")
        //    {
        //        countRunner += 1;
        //    }
        //    Debug.Log("transform.child["+i+"] =" + transform.GetChild(i).name);
        //    Debug.Log("Player countRunner=" + countRunner);

        //}
        Debug.Log("squadFormation.transform.childCount =" + squadFormation.transform.childCount);

        int runnersAmountToAdd = collidedDoor.GetRunnersAmountToAdd(collidedDoorCollider, squadFormation.transform.childCount);
        //int runnersAmountToAdd = collidedDoor.GetRunnersAmountToAdd(collidedDoorCollider, transform.childCount);

        squadFormation.AddRunners(runnersAmountToAdd);

        if (VibrationManager.CanVibrate())
            Taptic.Light();

        OnRunnersAdded?.Invoke(runnersAmountToAdd);        
    }

    private void DetectFinishLine()
    {
        if (Physics.OverlapSphere(transform.position, 1, finishLayer).Length > 0)
        {
            FindObjectOfType<FinishLine>().PlayConfettiParticles();
            SetLevelComplete();

            finishLineDetected = true;
        }
    }

    private void DetectionObstacles()
    {
        if (Physics.OverlapSphere(transform.position, 0.5f, obstacleLayer).Length > 0)
            runner.Explode();           
    } 

    private void DetectEnemies()
    {
        bool enemiesDetected = Physics.OverlapSphere(transform.position, enemiesDetectionRadius, enemiesLayer).Length > 0;
        
        if(enemiesDetected && !previousEnemiesDetected)
            OnEnemiesDetected?.Invoke();
        else if(!enemiesDetected && previousEnemiesDetected)
            OnNoEnemiesDetected?.Invoke();

        previousEnemiesDetected = enemiesDetected;
    }
     
    private void SetLevelComplete()
    {
        //UIManager.setLevelCompleteDelegate?.Invoke(3);        

        // Instead of setting the level complete
        // Trigger an event
        
        OnFinishLineCrossed?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, enemiesDetectionRadius);   
    }  


}
