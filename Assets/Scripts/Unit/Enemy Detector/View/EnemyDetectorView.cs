using UnityEngine;

public class EnemyDetectorView : MonoBehaviour, IEnemyDetectorView
{
    public void Disable()
    {
        gameObject.SetActive(false);
    }

    public void SetRotation(Quaternion rotation)
    {
        gameObject.SetActive(true);
        transform.rotation = rotation;
    }
}
