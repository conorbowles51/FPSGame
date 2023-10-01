using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CB
{
    public class ZombieAI : EnemyAI
    {
        protected override void Attack()
        {
            OnAttackEnter();
            animator.SetTrigger("Attack");
        }
    }
}