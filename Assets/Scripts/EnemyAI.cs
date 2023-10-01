using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace CB
{
    public abstract class EnemyAI : MonoBehaviour
    {
        [SerializeField] protected Transform target;

        [SerializeField] protected int attackDamage = 35;
        [SerializeField] protected float attackSuccessRange = 3.5f;

        protected EnemyStats enemyStats;
        protected NavMeshAgent agent;
        protected Animator animator;

        protected bool isAttacking = false;
        
        private void Awake()
        {
            enemyStats = GetComponent<EnemyStats>();
            agent = GetComponent<NavMeshAgent>();
            animator = GetComponentInChildren<Animator>();
        }

        public void Init(int damage, float speed, Vector3 position)
        {
            transform.position = position;
            attackDamage = damage;
            agent.speed = speed;
        }

        private void Update()
        {
            if (!enemyStats.isAlive)
            {
                PauseFollow();
                return;
            }

            FindTarget();

            if(agent.remainingDistance <= agent.stoppingDistance && !isAttacking)
            {
                Attack();
            }
        }

        public void OnAttackEnter()
        {
            isAttacking = true;
            PauseFollow();
        }

        public void OnAttackPerformed()
        {
            if(Vector3.Distance(target.position, transform.position) <= attackSuccessRange)
            {
                PlayerStats playerStats = target.GetComponent<PlayerStats>();
                playerStats.TakeDamage(attackDamage);

                Debug.Log("Zombie hit player");
            }
        }

        public void OnAttackExit()
        {
            isAttacking = false;
            ResumeFollow();
        }

        public void PauseFollow()
        {
            agent.isStopped = true;
        }

        public void ResumeFollow()
        {
            agent.isStopped = false;
        }

        protected void FindTarget()
        {
            target = GameObject.FindWithTag("Player").transform;

            agent.SetDestination(target.position);
        }

        protected abstract void Attack();
    }
}