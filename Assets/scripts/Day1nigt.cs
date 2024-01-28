using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.Rendering.Universal;
using UnityEngine.U2D;

public class Day1nigt : MonoBehaviour
{
    public float cycleDuration;
    public Sprite daySprite;
    public Sprite nightSprite;
    public SpriteRenderer fon;

    public GameObject lightMoon;


    public Light2D globalLight;
    public float durationFade;

    public bool isDay = true;

    void Start()
    {
        StartCoroutine(DayNight());
    }
    void Update()
    {

    }
    IEnumerator DayNight()
    {
        while (true)
        {
            yield return new WaitForSeconds(cycleDuration);

            if (isDay )
            {
                yield return Transition(1, 0);
                fon.sprite = nightSprite;
                yield return Transition(0, 0.2f);
                lightMoon.SetActive(true);

            }
            else
            {
                lightMoon.SetActive(false);
                yield return Transition(0.2f, 0);
                fon.sprite = daySprite;
                yield return Transition(0, 1f);
            }
            isDay = !isDay;
        }
    }

    IEnumerator Transition(float startValue, float endValue)
    {
        float time = 0;
        while (time < durationFade)
        {
            globalLight.intensity = Mathf.Lerp(startValue, endValue, time / durationFade);
            time += Time.deltaTime;
            yield return null;
        }
        globalLight.intensity = endValue;
    }
}
