using System;
using System.Collections.Generic;
using UnityEngine;
using static TweenEngine;

public class PotionBehaviour : MonoBehaviour
{
    [SerializeField] private ParticleSystem _blastParticle;

    private PotionData _potion;
    private Queue<GameObject> _monsterQueue;
    private GameObject _target;
    private Vector3 _launchDestination;

    public void Initialize(PotionData potion, Queue<GameObject> _monsterQueue)
    {
        _potion = potion;
        _target = _monsterQueue.Peek();
        _launchDestination = transform.localPosition + 
            new Vector3(_target.transform.position.x, 10f, 0);
    }

    private void Start()
    {
        LaunchPotion();
    }
    
    private void LaunchPotion()
    {
        Tween(this,
            Sequence(
                For(0.4f).Move(this).To(_launchDestination),
                Callback(() => LandPotion())
            )
        );
    }

    private void LandPotion()
    {
        TweenParallel(this,
            For(4f - _target.transform.localScale.x / 2.5f).Scale(this).To(0.01f),
            Sequence(
                For(4f - _target.transform.localScale.x / 2.5f).Move(this).To(_target.transform.position),
                Callback(() => HitMonster()),
                For(1f).Wait(),
                Callback(() => Destroy(gameObject))
            )
        );
    }

    private void HitMonster()
    {
        try 
        {
            _blastParticle.Play();
            _target.GetComponent<MonsterBehaviour>().HitWithPotion(_potion);
        }
        catch (Exception) { }
    }
}