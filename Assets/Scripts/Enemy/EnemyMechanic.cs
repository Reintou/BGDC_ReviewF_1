﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMechanic : MonoBehaviour
{
    public float knockbackForce = 3f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var _collision = collision.gameObject;
        if(_collision.CompareTag("Player"))
        {
            var collrb = _collision.GetComponent<Rigidbody2D>();
            StartCoroutine(KnockbackPlayer(_collision));
            Vector2 diff = (transform.position - _collision.transform.position).normalized;
            if(diff.x * knockbackForce > -2.3f && diff.x * knockbackForce < 2.3f)
            {
                collrb.AddForce(new Vector2(diff.x, -4f) * knockbackForce, ForceMode2D.Impulse);
            }
            else
            {
                collrb.AddForce(new Vector2(diff.x, -3f) * knockbackForce, ForceMode2D.Impulse);
            }
        }
    }

    IEnumerator KnockbackPlayer(GameObject _collision)
    {
        _collision.GetComponent<PlayerMovement>().canMove = false;
        _collision.GetComponent<PlayerCollision>().onGround = false;
        yield return new WaitForSeconds(0.5f);
        _collision.GetComponent<PlayerMovement>().canMove = true;
    }
}
