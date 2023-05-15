using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween
{
    public float time;

    public Tween(float time)
    {
        this.time = time;
    }

    public IEnumerable<float> Wait()
    {
        for (var currentTime = 0f; currentTime < time; currentTime += Time.deltaTime)
        {
            yield return currentTime;
        }
    }
}
