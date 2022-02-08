using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EndScreenScript : MonoBehaviour
{
    public Image panel;
    public RawImage title;
    public AnimationCurve panelCurve, titleAlphaCurve, titleScaleCurve;
    public float duration;

    void Start()
    {
        //StartAppearing(3);   
    }

    public void StartAppearing(float delay)
    {
        StartCoroutine("Appear", delay);
    }

    IEnumerator Appear(float delay)
    {
        yield return new WaitForSeconds(delay);

        float startTime = Time.time;
        float currentTime, panelAlpha, titleAlpha, titleScale;

        while(Time.time - startTime < duration)
        {
            currentTime = Time.time - startTime;
            panelAlpha = panelCurve.Evaluate(Mathf.Min(currentTime / duration, 1));
            titleAlpha = titleAlphaCurve.Evaluate(Mathf.Min(currentTime / duration, 1));
            titleScale = titleScaleCurve.Evaluate(Mathf.Min(currentTime / duration, 1));

            panel.color = new Color(0, 0, 0, panelAlpha);
            title.color = new Color(1, 1, 1, titleAlpha);
            title.transform.localScale = new Vector3(titleScale/2, titleScale/2, titleScale/2);

            yield return null;
        }
    }
}
