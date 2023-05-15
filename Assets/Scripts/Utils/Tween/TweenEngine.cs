using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenEngine : MonoBehaviour
{
    public static TweenBuilder For(float time)
    {
        return new TweenBuilder(time);
    }

    public static void TweenParallel(MonoBehaviour obj, params IEnumerable[] actions)
    {
        foreach (var action in actions)
        {
            Tween(obj, action);
        }
    }

    public static void Tween(MonoBehaviour obj, IEnumerable action)
    {
        obj.StartCoroutine(action.GetEnumerator());
    }

    public static IEnumerable Sequence(params IEnumerable[] actions)
    {
        foreach (var action in actions)
        {
            foreach (var step in action)
            {
                yield return step;
            }
        }
    }

    public static IEnumerable Callback(System.Action action)
    {
        yield return null;

        action();
    }
}

public class TweenBuilder : Tween
{
    public TweenBuilder(float time) : base(time) { }
}
