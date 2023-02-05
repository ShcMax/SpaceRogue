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
        private readonly LaserWeaponConfig _weaponConfig; 
        private readonly ProjectileConfig _projectileConfig;

        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var laserConfig = config.SpecificWeapon as LaserWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("wrong config type was provided");               
        }

        public override void CommenceFiring()
        {           
            CooldownTimer.Start();
        }

        private void LaserBeamLength()
        {

        }
    }
}