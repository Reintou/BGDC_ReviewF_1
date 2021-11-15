using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAttack : MonoBehaviour
{
    private PlayerMovement movementScript;
    public GameObject ballPrefab;
    public float throwForce;

    [Space]

    public Vector2 leftOffset, rightOffset;

    // Start is called before the first frame update
    void Start()
    {
        movementScript = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.C))
        {
            GameObject ball = Instantiate(ballPrefab, transform.position, Quaternion.identity);


            if (movementScript.transform.localScale.x == -1f)
            {
                ball.transform.position += new Vector3(leftOffset.x, leftOffset.y, 0f);
                ball.GetComponent<Rigidbody2D>().velocity += (Vector2)(transform.right * -1f) * throwForce;
            }
            else if (movementScript.transform.localScale.x == 1f)
            {
                ball.transform.position += new Vector3(rightOffset.x, rightOffset.y, 0f);
                ball.GetComponent<Rigidbody2D>().velocity += (Vector2)transform.right * throwForce;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, .25f);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, .25f);
    }
}
