using UnityEngine;

public class MapSizeDrawer : MonoBehaviour
{
    public Vector3 Size;
    public Color Color = Color.red;

    public void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Gizmos.DrawWireCube(transform.position, Size);
    }
}
