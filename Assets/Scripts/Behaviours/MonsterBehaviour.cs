using UnityEngine;
using UnityEngine.Events;
using static TweenEngine;

public class MonsterBehaviour : MonoBehaviour
{
    public UnityEvent onHitPlayer;
    public UnityEvent onMonsterDeath;
    public Transform destination;
    public LivesBehaviour lives;
    public Sprite _flinchSprite;

    void Start()
    {
        var destinationPos = destination.position + (Vector3.up * 0.5f);

        TweenParallel(this,
            Sequence(
                For(5f).Wait(),
                Callback(() => GetComponent<SpriteRenderer>().sortingOrder++),
                For(5f).Wait(),
                Callback(() => GetComponent<SpriteRenderer>().sortingOrder++),
                For(5f).Wait(),
                Callback(() => GetComponent<SpriteRenderer>().sortingOrder++)
            ),
            Sequence(
                For(20f).Move(this).To(destinationPos),
                Callback(HitPlayer)
            ),
            Sequence(
                Callback(() => transform.localScale = Vector3.one * 0.3f),
                For(15f).Scale(this).To(8f)
            )
        );
    }

    public void HitWithPotion(PotionData potion)
    {
        for (int i = 0; i < potion.damageAmount; i++)
            lives.Decrement();
        
        if (lives.count <= 0)
        {
            onMonsterDeath.Invoke();
            StopAllCoroutines();
            FlinchAndDestroy();
        }
    }

    private void HitPlayer()
    {
        onMonsterDeath = null;
        onHitPlayer.Invoke();
        FlinchAndDestroy();
    }

    private void FlinchAndDestroy()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<SpriteRenderer>().sprite = _flinchSprite;

        Tween(this,
            Sequence(
                For(1f).Wait(),
                Callback(() => Destroy(gameObject))
            )
        );
    }
}
