using Abstracts;
using Gameplay.Mechanics.Meter;
using Gameplay.Player;
using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;
using Gameplay.Mechanics.Timer;

namespace Gameplay.Shooting
{
    public sealed class FrontalLazerController : FrontalTurretController
    {

        private readonly MinigunWeaponConfig _weaponConfig;

        private readonly MeterWithCooldown _overheatMeter;

        private readonly ProjectileConfig _projectileConfig;

        private float _currentSprayAngle;

        private readonly ResourcePath _lazerPointPrefab = new(Constants.Prefabs.Stuff.Lazer);
        private readonly LazerWeaponConfig _lazerWeaponConfig;
        private readonly TurretModuleConfig config;

        public FrontalLazerController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var minigunConfig = config.SpecificWeapon as MinigunWeaponConfig;
            _weaponConfig = minigunConfig
                ? minigunConfig
                : throw new System.Exception("wrong config type was provided");

            _overheatMeter = new MeterWithCooldown(0.0f, _weaponConfig.TimeToOverheat, _weaponConfig.OverheatCoolDown);
            _overheatMeter.OnCooldownEnd += ResetSpray;
            _currentSprayAngle = _weaponConfig.SprayAngle;
        }

        protected override void OnDispose()
        {
            _overheatMeter.OnCooldownEnd -= ResetSpray;
            _overheatMeter.Dispose();
            base.OnDispose();
        }

        public override void CommenceFiring()
        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown) return;

            CreateLazer();
            AddHeat();
            CooldownTimer.Start();
        }

        private void AddHeat()
        {
            _overheatMeter.Fill(_weaponConfig.Cooldown);
            IncreaseSpray();
        }

        private void IncreaseSpray()
        {
            if (_currentSprayAngle >= _weaponConfig.MaxSprayAngle) return;
            var sprayIncrease = CountSprayIncrease();
            _currentSprayAngle += sprayIncrease;
        }

        private float CountSprayIncrease()
        {
            return (_weaponConfig.MaxSprayAngle - _weaponConfig.SprayAngle) / (_weaponConfig.TimeToOverheat * (1 / _weaponConfig.Cooldown));
        }

        private void ResetSpray()
        {
            _currentSprayAngle = _weaponConfig.SprayAngle;
        }

        public void CreateLazer()
        {
            Transform projectileSpawnTransform = _projectileConfig.Prefab.transform;

            var lasePointView = ResourceLoader.LoadPrefab(_lazerPointPrefab);
            var laserPoint = Object.Instantiate(lasePointView, projectileSpawnTransform.TransformDirection(0.6f * projectileSpawnTransform.localScale.y * Vector3.up), projectileSpawnTransform.rotation);
            laserPoint.transform.parent = projectileSpawnTransform;
            laserPoint.transform.localScale.Set(0.1f, _lazerWeaponConfig.BeamLength, 0);

            laserPoint.SetActive(false);

            CooldownTimer = new Timer(config.SpecificWeapon.Cooldown);

            AddGameObject(laserPoint);
        }
    }
}
