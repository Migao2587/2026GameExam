using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float killTime = 20f;
    private Coroutine destoryAuto;

    private void Start()
    {
        destoryAuto = StartCoroutine(AutoDestory());
    }
    //自动销毁
    private IEnumerator AutoDestory()
    { 
        yield return new WaitForSeconds(killTime);
        Destroy(gameObject);
    }
    //碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PropManger manager = collision.GetComponent<PropManger>();
        if (manager)
        {
            bool pickUp = manager.PickupItem(gameObject);

            if (pickUp)
            {
                if (destoryAuto != null)
                { 
                    StopCoroutine(destoryAuto);
                    destoryAuto = null;
                }
                RemoveItem();
            }
        }
    }

    public void RemoveItem()
    {
        Destroy(gameObject);
    }
}
