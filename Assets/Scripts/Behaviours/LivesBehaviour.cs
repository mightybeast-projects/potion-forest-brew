using System.Collections.Generic;
using UnityEngine;

public class LivesBehaviour : MonoBehaviour
{
    public int count;

    private List<Transform> _lives;

    public void Start()
    {
        _lives = new List<Transform>(GetComponentsInChildren<Transform>());
    }

    public void Decrement()
    {
        if (count == 0) return;

        count--;

        var currentLive = _lives[_lives.Count - 1];
        _lives.Remove(currentLive);
        Destroy(currentLive.gameObject);
    }
}
