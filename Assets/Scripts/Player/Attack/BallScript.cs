using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    public float decayTime = 2f;

    private void Start()
    {
        StartCoroutine(CheckHit());
    }

    IEnumerator CheckHit()
    {
        yield return new WaitForSeconds(decayTime);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("ResetButton"))
        {
            collision.gameObject.GetComponent<ResetEnemy>().StartReset();
            Destroy(gameObject);
        }
    }
}
