using Abstracts;
using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(ProjectileLazerConfig), menuName = "Configs/Projectiles/" + nameof(ProjectileLazerConfig))]

    public class ProjectileLazerConfig : ScriptableObject, IIdentityItem<string>
    {
        [field: SerializeField] public string Id { get; private set; } = Guid.NewGuid().ToString();
        [field: SerializeField] public ProjectileLazerView Prefab { get; private set; } 
        [field: SerializeField, Min(0.1f)] public float DamageAmount { get; private set; } = 1f;        
        [field: SerializeField, Min(0.1f)] public float LifeTime { get; private set; } = 10.0f;
        [field: SerializeField, Range(1f, 100f)] public float BeamLength { get; set; } // ƒлина луча
        [field: SerializeField, Range(0.01f, 0.5f)] public float BeamWidth { get; set; } // Ўирина луча
        [field: SerializeField] public bool IsDestroyedOnHit { get; private set; } = true;
    }
}