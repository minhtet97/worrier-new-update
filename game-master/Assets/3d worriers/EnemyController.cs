using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK
}
public class EnemyController : MonoBehaviour
{
    private CharacterAnimation enemy_Anim;
    private NavMeshAgent NavAgent;

    private Transform playerTarget;

    public float move_Speed = 3.5f;

    public float attack_Distace = 1f;
    public float chase_Plater_After_Attack_Distance = 1f;

    private float wait_Before_Attack_Time = 3f;
    private float attack_Timer;

    private EnemyState enemy_State;

    public GameObject attackPoint;

    

    // Start is called before the first frame update
    void Awake()
    {
        enemy_Anim = GetComponent<CharacterAnimation>();
        NavAgent = GetComponent<NavMeshAgent>();

        playerTarget = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        enemy_State = EnemyState.CHASE;

        attack_Timer = wait_Before_Attack_Time;

    
    }

    // Update is called once per frame
    void Update()
    {
        if(enemy_State == EnemyState.CHASE)
        {
            ChasePlayer();
        }

        if(enemy_State == EnemyState.ATTACK)
        {
            AttackPlayer();
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {

        //Checks if other gameobject has a Tag of Player
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMove>().alive = false;
            Time.timeScale = 1;
        }
    }

    void ChasePlayer()
    {
        NavAgent.SetDestination(playerTarget.position);
        NavAgent.speed = move_Speed;

        if(NavAgent.velocity.sqrMagnitude == 0)
        {
            enemy_Anim.walk(false);
        }
        else
        {
            enemy_Anim.walk(true);
        }

        if(Vector3.Distance(transform.position, playerTarget.position) <= attack_Distace)
        {
            enemy_State = EnemyState.ATTACK;
        }
    }

    void AttackPlayer()
    {
        NavAgent.velocity = Vector3.zero;
        NavAgent.isStopped = true;

        enemy_Anim.walk(false);

        attack_Timer += Time.deltaTime;

        if(attack_Timer > wait_Before_Attack_Time)
        {
            if(Random.Range(0, 2) > 0)
            {
                enemy_Anim.Attack1();
            }
            else
            {
                enemy_Anim.Attack2();
            }

            attack_Timer = 0f;
        }

        if(Vector3.Distance(transform.position, playerTarget.position) > 
            attack_Distace + chase_Plater_After_Attack_Distance)
        {
            NavAgent.isStopped = false;
            enemy_State = EnemyState.CHASE;
        }
    }

    void Activate_AttackPoint()
    {
        attackPoint.SetActive(true);
    }
    void Deactivate_AttackPoint()
    {
        if (attackPoint.activeInHierarchy)
        {
            attackPoint.SetActive(false);
        }

    }
}
