using Abstracts;
using Gameplay.Mechanics.Meter;
using Gameplay.Player;
using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;

namespace Gameplay.Shooting
{
    public sealed class FrontalLaserController : FrontalTurretController
    {

        private readonly LaserWeaponConfig _weaponConfig; 
        private readonly ProjectileConfig _projectileConfig;
        private readonly MeterWithCooldown _overheatMeter;
        private readonly ProjectileView _laserView;
        private float _reset;        
        private readonly PlayerView _playerView;


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

        {
           
            CooldownTimer.Start();
            
        }

        private void LaserBeamLength()
        {
            _laserView.transform.localScale = new Vector3(1, _weaponConfig.BeamLength, 1);
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
            
            
        }        
    }
}