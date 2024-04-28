using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[CreateAssetMenu()]
public class RewardData : ScriptableObject
{
    public float timeReward = 2f;
    public AnimationCurve timeRewardCurve;
    [Space]
    public float killReward = 100f;
    public AnimationCurve killRewardCurve;
    [Space]
    public float distanceReward = 100f;
    public AnimationCurve distanceRewardCurve;
    [Space]
    public float mobReward = 100f;
    public AnimationCurve mobRewardCurve;
    [Space]
    public float minBulletDistance = 3f;
    public float bulletDistanceReward = 100f;
    public AnimationCurve bulletDistanceRewardCurve;
    public float bulletVisionRadius = 5f;
    public LayerMask bulletVisionLayer;
}
