using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetSystems;

public class SquadFormation : MonoBehaviour
{
    [Header(" Components ")]
    [SerializeField] private GameObject groupAmountBubble;    
    [SerializeField] private TextMeshPro squadAmountText;
    [SerializeField] private BonusRunnersParent bonusRunnersParent;

    [Header(" Formation Settings ")]
    [Range(0f, 1f)]
    [SerializeField] private float radiusFactor;
    [Range(0f, 1f)]
    [SerializeField] private float angleFactor;

    [Header(" Settings ")]
    [SerializeField] private Runner runnerPrefab;

    private void Awake()
    {
        SquadDetection.OnFinishLineCrossed += EnableBonusParentScript;
    }

    private void OnDestroy()
    {
        SquadDetection.OnFinishLineCrossed -= EnableBonusParentScript;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FermatSpiralPlacement();
        squadAmountText.text = transform.childCount.ToString();
    }

    private void FermatSpiralPlacement()
    {
        float goldenAngle = 137.5f * angleFactor;  

        for (int i = 0; i < transform.childCount; i++)
        {
            float x = radiusFactor * Mathf.Sqrt(i+1) * Mathf.Cos(Mathf.Deg2Rad * goldenAngle * (i+1));
            float z = radiusFactor * Mathf.Sqrt(i+1) * Mathf.Sin(Mathf.Deg2Rad * goldenAngle * (i+1));

            Vector3 runnerLocalPosition = new Vector3(x, 0, z);
            Vector3 runnerTargetWorldPosition = transform.TransformPoint(runnerLocalPosition);

            Transform currentRunner = transform.GetChild(i);

            Vector3 velocityDirection = runnerTargetWorldPosition - currentRunner.position;

            Rigidbody runnerRig = currentRunner.GetComponent<Rigidbody>();

            runnerRig.velocity = Vector3.Lerp(runnerRig.velocity, velocityDirection, 0.1f).With(y: runnerRig.velocity.y);

            //transform.GetChild(i).localPosition = Vector3.Lerp(transform.GetChild(i).localPosition, runnerLocalPosition, 0.1f);
        }
    }

    public float GetSquadRadius()
    {
        return radiusFactor * Mathf.Sqrt(transform.childCount);
    }

    public void AddRunners(int amount)
    {        
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = GetFermatPosition(transform.childCount);

            Runner runnerInstance = Instantiate(runnerPrefab, spawnPosition, Quaternion.identity, transform);

            runnerInstance.SetSkin(transform.GetChild(0).GetComponent<Runner>().GetSkinIndex());
            runnerInstance.StartRunning();
            
            runnerInstance.name = "Runner_" + runnerInstance.transform.GetSiblingIndex();
        }
    }

    public void AddInitialRunners(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 spawnPosition = GetFermatPosition(transform.childCount);

            Runner runnerInstance = Instantiate(runnerPrefab, spawnPosition, Quaternion.identity, transform);

            runnerInstance.SetSkin(transform.GetChild(0).GetComponent<Runner>().GetSkinIndex());
            //runnerInstance.StartRunning();

            runnerInstance.name = "Runner_" + runnerInstance.transform.GetSiblingIndex();
        }
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

    private void EnableBonusParentScript()
    {
        groupAmountBubble.SetActive(false);

        bonusRunnersParent.enabled = true;
        this.enabled = false;
    }
}
