using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class MobActivator : MonoBehaviour
{
    [SerializeField] private MobView mobView;

    private bool isFirstTime;

    private void Awake()
    {
        isFirstTime = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isFirstTime)
        {
            return;
        }

        if (other.TryGetComponent<PlayerView>(out var player))
        {
            isFirstTime = false;
            mobView.Activate(player);
        }
    }
}
