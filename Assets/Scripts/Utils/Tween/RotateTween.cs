using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RotateTweenExtension
{
    public static RotateTween Rotate(this TweenBuilder tween, MonoBehaviour obj)
        => new RotateTween(tween.time, obj);
}

public class RotateTween : ObjectTween
{
    public RotateTween(float time, MonoBehaviour obj) : base(time, obj) { }

    public IEnumerable By(float angle)
    {
        var transform = obj.transform;
        var currentAngle = transform.eulerAngles;
        var startRotation = currentAngle.y;
        var endRotation = startRotation + angle;

        foreach (var step in Wait())
        {
            var rotationValue = Mathf.Lerp(startRotation, endRotation, step / time) % 360;

            transform.eulerAngles = new Vector3(currentAngle.x, currentAngle.y, -rotationValue);

            yield return null;
        }
    }
}
