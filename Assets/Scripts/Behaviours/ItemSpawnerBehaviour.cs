using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using static TweenEngine;
using System;
using Random = UnityEngine.Random;

public class ItemSpawnerBehaviour : MonoBehaviour
{
    [MinMaxSlider(0, 10)]
    public Vector2 itemThrowDelay;

    public GameObject itemSpawn;

    public ItemBehaviour[] itemPrefabs;

    private Transform[] _pads;

    public GameObject padsContainer;

    void Start()
    {
        _pads = padsContainer.GetComponentsInChildren<Transform>();

        RepeatWithDelay(itemThrowDelay, ThrowItem);
    }

    void ThrowItem()
    {
        var destination = GetEmptyPad();

        if (destination == null)
        {
            return;
        }

        var itemPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        var itemPosition = itemSpawn.transform.position;

        var item = Instantiate(itemPrefab, itemPosition, Quaternion.identity);

        item.destination = destination;
        item.gameObject.transform.SetParent(destination);
        item.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) == 1;
    }

    Transform GetEmptyPad() => Array.Find(_pads,
        (pad) => pad.childCount == 0
    );

    void RepeatWithDelay(Vector2 delay, Action action)
    {
        Tween(this, Sequence(
            For(Random.Range(delay.x, delay.y)).Wait(),
            Callback(action),
            Callback(() => RepeatWithDelay(delay, action))
        ));
    }
}
