using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(LaserWeaponConfig), menuName = "Configs/Weapons/" + nameof(LaserWeaponConfig))]

    public sealed class LaserWeaponConfig: SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig LaserProjectile { get; private set; }

        [field: SerializeField] public float DamageFrequency = 0.5f; // Периодичность нанесения урона
        [field: SerializeField] public float WorkingTime { get; private set; } // Время работы лазера
        [field: SerializeField, Min(0f)] public float DurationOfWork { get; private set; } // Время которое отработал лазер
        [field: SerializeField, Range (0 , 100f)] public float BeamLength { get; private set; } // Длина луча
        [field: SerializeField, Range(0 , 1f)] public float Multiplier { get; private set; } // Множитель времени перезарядки     
    }
}
