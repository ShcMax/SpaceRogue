using Abstracts;
using Gameplay.Mechanics.Meter;
using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Mathematics;

namespace Gameplay.Shooting
{
    public sealed class FrontalLaserController : FrontalTurretController
    {
<<<<<<< Updated upstream
        private readonly LaserWeaponConfig _weaponConfig; 
        private readonly ProjectileConfig _projectileConfig;
=======
        private readonly LaserWeaponConfig _weaponConfig;
        private readonly MeterWithCooldown _overheatMeter;
        private float _reset;
>>>>>>> Stashed changes

        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var laserConfig = config.SpecificWeapon as LaserWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("wrong config type was provided");

            _overheatMeter = new MeterWithCooldown(0.0f, _weaponConfig.DurationOfWork, _weaponConfig.WorkingTime);
            _overheatMeter.OnCooldownEnd += ResetLaser;
        }

        public override void CommenceFiring()
<<<<<<< Updated upstream
        {           
            CooldownTimer.Start();
        }

        private void LaserBeamLength()
        {

=======
        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown) return;

            FireLaser();
            AddHeat();
            CooldownTimer.Start();
>>>>>>> Stashed changes
        }

        protected override void OnDispose()
        {
            _overheatMeter.OnCooldownEnd -= ResetLaser;
            _overheatMeter.Dispose();
            base.OnDispose();
        }

        private void AddHeat()
        {
            _overheatMeter.Fill(_weaponConfig.Cooldown);
        }

        private void ResetLaser()
        {
            _reset = _weaponConfig.DurationOfWork * _weaponConfig.Multiplier;
        }

        private void FireLaser()
        {
            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);
        }
    }
}