using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.TryGetComponent<PlayerView>(out var player))
        {
            var presenter = player.GetPresenter();
            presenter.TouchBorder();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.TryGetComponent<PlayerView>(out var player))
        {
            var presenter = player.GetPresenter();
            presenter.TouchBorder();
        }
    }
}
