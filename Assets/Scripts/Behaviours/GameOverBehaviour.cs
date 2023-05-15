using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TweenEngine;
using UnityEngine.SceneManagement;

public class GameOverBehaviour : MonoBehaviour
{
    void Start()
    {
        Tween(this,
            Sequence(
                For(2).Wait(),
                For(16).Move(this).To(new Vector3(0, 16)),
                For(2).Wait(),
                Callback(() =>
                {
                    SceneManager.LoadScene(0);
                })
            )
        );
    }
}
