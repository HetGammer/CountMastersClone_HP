using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JetSystems;
using UnityEngine.Events;

public class Runner : MonoBehaviour
{
    enum RunnerState {Running, Bonus, Stopped} 
    private RunnerState runnerState;

    [Header(" Events ")]
    public static UnityAction<Runner> OnRunnerDied;

    [Header(" Components ")]
    [SerializeField] private RunnerSkinManager skinManager;
    //[SerializeField] private Animator animator;
    [SerializeField] private Collider collider;
    //[SerializeField] private Renderer renderer;

    [Header(" Effects ")]
    [SerializeField] private ParticleSystem explodeParticles;

    [Header(" Target Settings ")]
    private bool targeted;

    [Header(" Detection ")]
    [SerializeField] private LayerMask obstaclesLayer;

    [Header(" Bonus Settings ")]
    private Vector3 targetPyramidLocalPosition;

    // Start is called before the first frame update
    void Start()
    {
        runnerState = RunnerState.Running;
    }

    // Update is called once per frame
    void Update()
    {
        ManageRunnerState();
    }

    private void ManageRunnerState()
    {
        switch(runnerState)
        {
            case RunnerState.Running:
                ManageRunningState();
            break;

            case RunnerState.Bonus:
                ManageBonusState();
            break;
        }
    }

    private void ManageRunningState()
    {
        if (!collider.enabled)
            return;

        DetectObstacles();
    }

    private void ManageBonusState()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPyramidLocalPosition, .025f);
    }

    private void DetectObstacles()
    {
        if (Physics.OverlapSphere(transform.position, 0.1f, obstaclesLayer).Length > 0)
            Explode();
    }

    public void StartRunning()
    {
        skinManager.StartRunning();
    }

    public void StopRunning()
    {
        skinManager.StopRunning();
    }

    public void SetAsTarget()
    {
        targeted = true;
    }

    public bool IsTargeted()
    {
        return targeted;
    }

    public void Explode()
    {
        if(VibrationManager.CanVibrate())
        {
            //Debug.Log("VibrationManager_Shaking");
            Taptic.Light();
        }

        collider.enabled = false;
        skinManager.DisableRenderer();

        if (transform.parent != null && transform.parent.childCount <= 1)
            UIManager.setGameoverDelegate?.Invoke();

        transform.parent = null;
        explodeParticles.Play();

        OnRunnerDied?.Invoke(this);

        Destroy(gameObject, 3);
    }

    public void Stop()
    {
        StopRunning();
        runnerState = RunnerState.Stopped;

        transform.SetParent(null);
    }

    public void AssignPyramidLocalPosition(Vector3 position)
    {
        GetComponent<Rigidbody>().isKinematic = true;

        targetPyramidLocalPosition = position;
        
        runnerState = RunnerState.Bonus;
    }

    public void SetSkin(int skinIndex)
    {
        skinManager.SetSkin(skinIndex);
    }

    public int GetSkinIndex()
    {
        return skinManager.GetSkinIndex();
    }

    public Color GetColor()
    {
        return skinManager.GetColor();// renderer.material.GetColor("_BaseColor");
    }

    public bool IsStopped()
    {
        return runnerState == RunnerState.Stopped;
    }
}
