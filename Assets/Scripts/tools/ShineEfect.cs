using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShineEfect : MonoBehaviour
{
    [SerializeField] private Image image1;
    [SerializeField] private Color color1S;
    [SerializeField] private Color color1E;
    [SerializeField] private Image image2;
    [SerializeField] private Color color2S;
    [SerializeField] private Color color2E;
    [SerializeField] private float tickTime = 0.1f;
    [SerializeField] private float allTime = 1f;

    private Coroutine ShineCoroutine;
    private bool Shining = false;

    private void OnEnable()
    {
        if(ShineCoroutine == null)
        {
            Shining = true;
            ShineCoroutine = StartCoroutine(StartShineCoroutine());
        }
    }

    private void OnDisable()
    {
        Shining = false;
        ShineCoroutine = null;
        StopAllCoroutines();
    }
    float curTime = 0f;
    private IEnumerator StartShineCoroutine()
    {
        
        curTime = 0f;

        while (Shining)
        {
            Color color1T = color1S;
            Color color2T = color2S;

            if (curTime > allTime)
            {
                curTime = 0f;
            }
            else if(curTime >= (allTime / 2f))
            {
                color1T = color1S;
                color2T = color2S;
            }
            else if(curTime < (allTime / 2f))
            {
                color1T = color1E;
                color2T = color2E;
            }

            image1.color = Color.Lerp(image1.color, color1T, (curTime / allTime) / 2f);
            image2.color = Color.Lerp(image2.color, color2T, (curTime / allTime) / 2f);
            curTime += tickTime;
            yield return new WaitForSeconds(tickTime);
            Debug.LogWarning("Coroutine still work");
        }

        ShineCoroutine = null;
    }
}
