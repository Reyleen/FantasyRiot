using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*melee attack for Fighter and Lancer*/
public class MeleeAttack : MonoBehaviour
{
    public Transform attackPointU;
    public Transform attackPointU2;
    public Transform attackPointD;
    public Transform attackPointD2;
    public Transform attackPointL;
    public Transform attackPointL2;
    public Transform attackPointR;
    public Transform attackPointR2;
    public int damage;
    public LayerMask enemyLayers;

    // Update is called once per frame
    public void Attack(float x,float y)
    {
        if ( x>=0.7f && (y<=0.7f && y>=-0.7f))//every if check the direction of the attack. this is Right
        {
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointR.position, attackPointR2.position, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
                Debuffs d = enemy.GetComponent<Debuffs>();
                StartCoroutine(d.KnockUp(0.5f, 1f, enemy.transform.position));
            }
        }else if ((x < 0.7f && x>-0.7f) && y > 0.7f)//Up
        {
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointU.position, attackPointU2.position, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            }
        }else if (x <= -0.7f && (y <= 0.7f && y >= -0.7f))//Left
        {
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointL.position, attackPointL2.position, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            }
        }else if ((x < 0.7f && x > -0.7f) && y < -0.7f)//Right
        {
            Collider2D[] hitEnemies = Physics2D.OverlapAreaAll(attackPointD.position, attackPointD2.position, enemyLayers);

            foreach (Collider2D enemy in hitEnemies)
            {
                enemy.GetComponent<EnemyHealthManager>().HurtEnemy(damage);
            }
        }
        
    }
    
    private void OnDrawGizmosSelected()//Draw Gizmo for Unity
    {
        if (attackPointD == null || attackPointD2==null)
            return;
        Gizmos.DrawLine(attackPointD.position, attackPointD2.position);
        if (attackPointU == null || attackPointU2 == null)
            return;
        Gizmos.DrawLine(attackPointU.position, attackPointU2.position);
        if (attackPointL == null || attackPointL2 == null)
            return;
        Gizmos.DrawLine(attackPointL.position, attackPointL2.position);
        if (attackPointR == null || attackPointR2 == null)
            return;
        Gizmos.DrawLine(attackPointR.position, attackPointR2.position);
    }
}
