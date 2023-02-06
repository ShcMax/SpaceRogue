using System;
using UnityEngine;

namespace Gameplay.Shooting
{
    [CreateAssetMenu(fileName = nameof(LaserWeaponConfig), menuName = "Configs/Weapons/" + nameof(LaserWeaponConfig))]

    public sealed class LaserWeaponConfig: SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileConfig LaserProjectile { get; private set; }

        [field: SerializeField] public float DamageFrequency = 0.5f; // ������������� ��������� �����
        [field: SerializeField] public float WorkingTime { get; private set; } // ����� ������ ������
        [field: SerializeField, Min(0f)] public float DurationOfWork { get; private set; } // ����� ������� ��������� �����
        [field: SerializeField, Range (0 , 100f)] public float BeamLength { get; private set; } // ����� ����
        [field: SerializeField, Range(0 , 1f)] public float Multiplier { get; private set; } // ��������� ������� �����������     
    }
}
