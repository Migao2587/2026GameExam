using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMain : EnemyMain
{
    public GameObject stone;

    
    public override void AtkPlayer()
    {
        //Debug.Log("攻击啦!");
        Collider2D[] atkCollisders = Physics2D.OverlapCircleAll(transform.position, atkDistance, playerLayer);
        if (atkCollisders.Length > 0)
        {
            foreach (var col in atkCollisders)
            {
                if (!col.CompareTag("Player"))
                {
                    continue;
                }
                Transform playerTrans = col.transform;
                Vector2 flyDir = (playerTrans.position - transform.position).normalized;

                GameObject stoneSpawn = Instantiate(stone,transform.position,transform.rotation);
                if (stoneSpawn.TryGetComponent(out ThrowBase proj))
                {
                    proj.Initialize(flyDir, 6, atk);
                }
            }
        }
    }
}
