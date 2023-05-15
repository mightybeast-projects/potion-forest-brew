using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MoveTweenExtension
{
    public static MoveTween Move(this TweenBuilder tween, MonoBehaviour obj)
        => new MoveTween(tween.time, obj);
}

public class MoveTween : ObjectTween
{
    public MoveTween(float time, MonoBehaviour obj) : base(time, obj) { }

    public IEnumerable To(Vector3 position)
    {
        var direction = DirectionTo(obj.gameObject.transform.position, position);
        var distance = Vector3.Distance(obj.gameObject.transform.position, position);

        return InDirection(direction, distance);
    }

    public IEnumerable By(Vector3 path)
    {
        var direction = Vector3.Normalize(path);
        var distance = Vector3.Magnitude(path);

        return InDirection(direction, distance);
    }

    public IEnumerable InDirection(Vector3 direction, float distance)
    {
        var speed = distance / time;

        foreach (var step in Wait())
        {
            obj.gameObject.transform.position += direction * speed * Time.deltaTime;

            yield return null;
        }
    }

    private static float DistanceTo(Vector3 source, Vector3 dest)
        => Vector3.Distance(source, dest);

    private static Vector3 DirectionTo(Vector3 source, Vector3 dest)
        => Vector3.Normalize(dest - source);
}
