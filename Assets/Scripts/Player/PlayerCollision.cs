using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [Header("Layer")]
    [SerializeField]
    private LayerMask groundLayer;

    [Space]

    [Header("Checks")]
    [HideInInspector]
    public bool onGround;
    [HideInInspector]
    public bool isKnockback = false;

    [Space]

    [Header("Collision")]
    [SerializeField]
    private float checkerRadius = 0.25f;
    public Vector2 bottomOffset;
    [HideInInspector]
    public Vector2 posBottomOffset, negBottomOffset;

    private void Start()
    {
        negBottomOffset = new Vector2(bottomOffset.x * -1f, bottomOffset.y);
        posBottomOffset = new Vector2(bottomOffset.x, bottomOffset.y);
    }

    void FixedUpdate()
    {
        if(!isKnockback)
            onGround = Physics2D.OverlapCircle((Vector2)transform.position + bottomOffset, checkerRadius, groundLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + bottomOffset, checkerRadius);
    }

    public void StartKnockback()
    {
        StartCoroutine(KnockbackPlayer());
    }

    IEnumerator KnockbackPlayer()
    {
        GetComponent<PlayerMovement>().canMove = false;
        onGround = false;
        isKnockback = true;
        yield return new WaitForSeconds(0.5f);
        GetComponent<PlayerMovement>().canMove = true;
        isKnockback = false;
    }
}
