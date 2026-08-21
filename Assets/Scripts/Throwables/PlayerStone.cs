using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : ThrowBase
{
    protected override void OnTriggerEnter2D(Collider2D obj)
    {
        if (obj.CompareTag("Mouster"))
        {
            //Debug.Log("击中啦！");
            if (obj.TryGetComponent<EnemyMain>(out EnemyMain hp))
            {
                hp.Hurt(damage, dir);
                DestorySelf();
            }
        }
    }
}
