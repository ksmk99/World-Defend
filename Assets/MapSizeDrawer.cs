using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapSizeDrawer : MonoBehaviour
{
    public Vector3 Size;
    public Color Color = Color.red;

    private void Start()
    {
        
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color;
        Gizmos.DrawWireCube(transform.position, Size);  
    }
}
