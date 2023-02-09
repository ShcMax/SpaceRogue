using Gameplay.Damage;
using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class ProjectileLazerView : MonoBehaviour, IDamagingView
    {
        public event Action CollisionEnter = () => { };
        public DamageModel DamageModel { get; private set; }

        public void Init(DamageModel damageModel)
        {
            DamageModel = damageModel;
        }
        public void OnTriggerStay2D(Collider2D collision)
        {
            CollisionEnter();
        }
    }
}