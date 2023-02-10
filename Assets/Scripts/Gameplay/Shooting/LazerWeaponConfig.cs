using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Shooting
{

    [CreateAssetMenu(fileName = nameof(LazerWeaponConfig), menuName = "Configs/Weapons/" + nameof(LazerWeaponConfig))]
    public sealed class LazerWeaponConfig : SpecificWeaponConfig
    {
        [field: SerializeField] public ProjectileLazerConfig projectileLazerConfig;
        [field: SerializeField] public GameObject LazerPrefab { get; set; }

        [field: SerializeField] public float DamageFrequency = 0.5f; // ������������� ��������� �����
        [field: SerializeField, Min(0.1f)] public float WorkingTime { get; private set; } = 5f;  // ����� ������ ������
        [field: SerializeField, Min(0.1f)] public float DurationOfWork { get; private set; } = 5f; // ����� ������� ��������� �����        
        [field: SerializeField, Range(0, 1f)] public float Multiplier { get; private set; } // ��������� ������� �����������       
    }
}