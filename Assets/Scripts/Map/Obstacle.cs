using Unit.Bullet;
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
        else if(collision.transform.TryGetComponent<BulletView>(out var bullet))
        {
            bullet.GetPresenter().Collide(gameObject);
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
