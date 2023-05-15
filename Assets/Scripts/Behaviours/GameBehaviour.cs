using UnityEngine;
using UnityEngine.Events;
using static TweenEngine;

public class GameBehaviour : MonoBehaviour
{
    public LivesBehaviour lives;

    public GameObject lightning;

    public GameObject gameOverScreen;

    public UnityEvent onGameOver;

    public void OnMonsterHitPlayer()
    {
        lives.Decrement();

        if (lives.count == 0)
        {
            GameOver();
            return;
        }

        Tween(this, Sequence(
            Callback(() => lightning.SetActive(true)),
            For(1f).Wait(),
            Callback(() => lightning.SetActive(false))
        ));
    }

    public void GameOver()
    {
        onGameOver.Invoke();
    }
}
