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

        public FrontalLaserController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var laserConfig = config.SpecificWeapon as LaserWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("wrong config type was provided");               
        }

        public override void CommenceFiring()
        {           

            var projectile = ProjectileFactory.CreateProjectile();            

            CooldownTimer.Start();
        }
    }
}