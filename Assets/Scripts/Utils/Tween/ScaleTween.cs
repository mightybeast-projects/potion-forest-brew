using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScaleTweenExtension
{
    public static ScaleTween Scale(this TweenBuilder tween, MonoBehaviour obj)
        => new ScaleTween(tween.time, obj);
}

public class ScaleTween : ObjectTween
{
    public ScaleTween(float time, MonoBehaviour obj) : base(time, obj) { }

    public IEnumerable To(float endScale)
    {
        var speed = (endScale - obj.transform.localScale.x) / time;

        foreach (var step in Wait())
        {
            var scaleInc = speed * Time.deltaTime;
            obj.transform.localScale += new Vector3(scaleInc, scaleInc);
            yield return null;
        }
    }
}
