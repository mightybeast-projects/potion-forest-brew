using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.Events;
using static TweenEngine;
using System;
using Random = UnityEngine.Random;

public class MonsterSpawnerBehaviour : MonoBehaviour
{
    [MinMaxSlider(0, 10)]
    public Vector2 monsterSpawnDelay;
    public UnityEvent onMonsterHitPlayer;
    public GameObject monsterSpawn;
    public GameObject monsterLane;
    public GameObject monsterDestination;
    public MonsterBehaviour[] monsterPrefabs;
    public ScoreManager scoreManager;
    public Queue<GameObject> monsterQueue;

    void Start()
    {
        monsterQueue = new Queue<GameObject>();
        RepeatWithDelay(monsterSpawnDelay, SpawnMonster);
    }

    void SpawnMonster()
    {
        var monsterPrefab = monsterPrefabs[Random.Range(0, monsterPrefabs.Length)];
        var spawnHalfWidth = monsterSpawn.transform.localScale.x / 2;
        var monsterPosition = monsterSpawn.transform.position
            + new Vector3(Random.Range(-spawnHalfWidth, spawnHalfWidth), 0, 0);

        var monster = Instantiate(monsterPrefab, monsterPosition, Quaternion.identity);

        monster.destination = monsterDestination.transform;
        monster.gameObject.transform.SetParent(monsterLane.transform);
        monster.GetComponent<SpriteRenderer>().flipX = Random.Range(0, 2) == 1;
        AddMonsterEventListeners(monster);
        monsterQueue.Enqueue(monster.gameObject);
    }

    private void AddMonsterEventListeners(MonsterBehaviour monster)
    {
        monster.onHitPlayer.AddListener(() => 
        { 
            DequeueFirstMonster();
            onMonsterHitPlayer.Invoke();
        });
        monster.onMonsterDeath.AddListener(() => {
            DequeueFirstMonster();
            scoreManager.IncreaseScore();
        });
    }

    void RepeatWithDelay(Vector2 delay, Action action)
    {
        Tween(this, Sequence(
            For(Random.Range(delay.x, delay.y)).Wait(),
            Callback(action),
            Callback(() => RepeatWithDelay(delay, action))
        ));
    }

    private void DequeueFirstMonster()
    {
        GameObject firstMonster = monsterQueue.Peek();
        LivesBehaviour firstMonsterLives = 
            firstMonster.GetComponentInChildren<LivesBehaviour>();
        monsterQueue.Dequeue();
    }
}
