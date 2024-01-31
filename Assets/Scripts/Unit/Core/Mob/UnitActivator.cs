using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UnitActivator : MonoBehaviour
{
    [SerializeField] private UnitView mobView;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerView>(out var player))
        {
            mobView.Activate(player);
            Destroy(gameObject);
        }
    }
}
