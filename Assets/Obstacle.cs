using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerView>(out var player))
        {
            var presenter = player.GetPresenter();
            presenter.TouchBorder();
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.transform.TryGetComponent<PlayerView>(out var player))
        {
            var presenter = player.GetPresenter();
            presenter.TouchBorder();
        }
    }
}
