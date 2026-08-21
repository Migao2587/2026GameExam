using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    private CharacterBase character;


    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    //移动
    public void Walk(Vector2 Direction)
    {
        character.rb.velocity = character.speed * Direction.normalized;
        character.anim.SetFloat("speed", Direction.magnitude);
        character.anim.SetFloat("moveX", Direction.x);
        character.anim.SetFloat("moveY", Direction.y);
    }

}
