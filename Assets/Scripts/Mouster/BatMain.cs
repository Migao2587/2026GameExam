using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMain : EnemyMain
{
    [HideInInspector]public bool EnableCrash;
    public void BatCrash()
    {
        rb.velocity = direction * speed * 1f;
        EnableCrash = true;
        gameObject.tag = "Untagged";
    }
    public void Stop()
    {
        rb.velocity = Vector2.zero;
        EnableCrash = false;
        gameObject.tag = "Mouster";
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!EnableCrash)
        {
            return;
        }

        //if (collision.gameObject.layer != playerLayer)
        //{
        //    Debug.Log("图层不对");
        //    return;
        //}
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        CharacterBase hp = collision.GetComponent<CharacterBase>();
        hp.TakeDamage(atk);
    }
}
