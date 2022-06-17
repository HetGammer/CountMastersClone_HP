using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{
    enum State{Idle, Running, Attacking, Dead}
    private State state;

    [Header(" Events ")]
    public static UnityAction<Vector3, Color> OnEnemyDied; 

    [Header(" Detection ")]
    [SerializeField] private LayerMask runnersLayer;
    [SerializeField] private float detectionDistance;
    private Runner targetRunner;

    [Header(" Movement ")]
    [SerializeField] private float moveSpeed;
    private float attackTimer;

    [Header(" Animation ")]
    [SerializeField] private Animator animator;
    [SerializeField] private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ManageEnemyState();

        if (targetRunner == null)
            FindTargetRunner();
        else
            AttackRunner();
    }

    private void ManageEnemyState()
    {
        switch(state)
        {
            case State.Idle:
                FindTargetRunner();
                break;

            case State.Running:
                GoTowardsTarget();
                break;
            
            case State.Attacking:
                AttackRunner();
                break;

            case State.Dead:
                break;
        }
    }

    private void FindTargetRunner()
    {
        Collider[] detectedRunners = Physics.OverlapSphere(transform.position, detectionDistance, runnersLayer);

        if (detectedRunners.Length <= 0) return;

        for (int i = 0; i < detectedRunners.Length; i++)
        {
            Runner currentRunner = detectedRunners[i].GetComponent<Runner>();
            if (currentRunner.IsTargeted()) continue;

            currentRunner.SetAsTarget();
            targetRunner = currentRunner;
            StartMoving();
            break;
        }


    }

    private void GoTowardsTarget()
    {
        //transform.position = Vector3.MoveTowards(transform.position, targetRunner.transform.position, moveSpeed * Time.deltaTime);
        
        GetComponent<Rigidbody>().velocity = (targetRunner.transform.position - transform.position).normalized * moveSpeed;

        transform.forward = (targetRunner.transform.position - transform.position).normalized;

        if(Vector3.Distance(transform.position, targetRunner.transform.position) < 1f)
            SetAttackingState();        
    }

    private void SetAttackingState()
    {
        state = State.Attacking;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void AttackRunner()
    {
        attackTimer += Time.deltaTime;

        if(attackTimer >= 1f)
        {
            targetRunner.Explode();
            Explode();

            state = State.Dead;
        }
    }

    private void StartMoving()
    {
        animator.SetInteger("State", 1);
        transform.parent = null;

        state = State.Running;
    }


    private void Explode()
    {
        OnEnemyDied?.Invoke(transform.position, renderer.material.GetColor("_BaseColor"));

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionDistance);
    }

    
}
