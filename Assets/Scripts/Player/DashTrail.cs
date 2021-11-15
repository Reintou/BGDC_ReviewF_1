using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DashTrail : MonoBehaviour
{
    public PlayerMovement movementScript;
    public Transform ghostParent;

    public Color trailColor;
    public Color fadeColor;
    public float ghostInterval = 0.5f;
    public float fadeTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        ghostParent = gameObject.transform;
    }

    public void StartGhost()
    {
        Sequence seq = DOTween.Sequence();

        for(int i = 0; i < ghostParent.childCount; i++)
        {
            Transform currentGhost = ghostParent.GetChild(i);
            SpriteRenderer currentGSR = currentGhost.GetComponent<SpriteRenderer>();
            seq.AppendCallback(() => currentGhost.transform.position = movementScript.transform.position);
            seq.AppendCallback(() => currentGSR.gameObject.transform.localScale = movementScript.gameObject.transform.localScale);
            seq.AppendCallback(() => currentGSR.sprite = movementScript.sr.sprite);
            seq.Append(currentGSR.material.DOColor(trailColor, 0f));
            seq.AppendCallback(() => FadeGhost(currentGSR));
            seq.AppendInterval(ghostInterval);
        }
    }

    public void FadeGhost(SpriteRenderer _currentGSR)
    {
        _currentGSR.material.DOKill();
        _currentGSR.material.DOColor(fadeColor, fadeTime);
    }
}
