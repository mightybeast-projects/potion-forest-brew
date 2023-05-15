using UnityEngine;

public class PotionSpawnerBehaviour : MonoBehaviour 
{
    [SerializeField] private ParticleSystem _creationParticles;
    [SerializeField] private Transform _potionSpawnPoint;
    [SerializeField] private GameObject _potionPrefab;
    [SerializeField] private MonsterSpawnerBehaviour _monsterSpawner;

    public void SpawnNewPotion(PotionData potion)
    {
        if (_monsterSpawner.monsterQueue.Count == 0) return;
        
        _creationParticles.Play();
        GameObject potionGO = 
            Instantiate(_potionPrefab, _potionSpawnPoint.position, Quaternion.identity);
        potionGO.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = potion.sprite;
        potionGO.GetComponent<PotionBehaviour>().Initialize(potion, _monsterSpawner.monsterQueue);
    }
}