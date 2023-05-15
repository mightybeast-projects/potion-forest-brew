using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectTween : Tween
{
    public MonoBehaviour obj;

    public ObjectTween(float time, MonoBehaviour obj) : base(time)
    {
        this.obj = obj;
    }
}
