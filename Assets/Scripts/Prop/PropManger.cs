using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManger : MonoBehaviour
{
    private CharacterBase player;

    private void Awake()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        { 
            player = playerObj.GetComponent<CharacterBase>();
        }
    }


    public bool PickupItem(GameObject obj)
    {
        switch (obj.tag)
        {
            case "coin":
                PickupCoin();
                return true;
            case "healther":
                PickupHealth();
                return true;
            default:
                return false;
        }
    }

    private void PickupCoin()
    {
        player.money++;
    }
    private void PickupHealth()
    {
        player.hp += player.maxHp * 0.2f;
        if (player.hp >= player.maxHp)
        {
            player.hp = player.maxHp;
        }
    }
}
