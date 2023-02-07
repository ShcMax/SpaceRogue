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
    public sealed class FrontalLazerController : FrontalTurretController
    {

        private readonly LazerWeaponConfig _weaponConfig;        
        private readonly MeterWithCooldown _overheatMeter;
        private readonly ProjectileView _lazerView;
        private float _reset;       

        public FrontalLazerController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var laserConfig = config.SpecificWeapon as LazerWeaponConfig;
            _weaponConfig = laserConfig
                ? laserConfig
                : throw new System.Exception("wrong config type was provided");

            _overheatMeter = new MeterWithCooldown(0.0f, _weaponConfig.DurationOfWork, _weaponConfig.WorkingTime);
            _overheatMeter.OnCooldownEnd += ResetLazer;
        }

        public override void CommenceFiring()

        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown) return;
            CooldownTimer.Start();
            AddHeat();
            ResetLazer();
            CreateLazer();
            
        }        

        protected override void OnDispose()
        {
            _overheatMeter.OnCooldownEnd -= ResetLazer;
            _overheatMeter.Dispose();
            base.OnDispose();
        }

        private void AddHeat()
        {
            _overheatMeter.Fill(_weaponConfig.Cooldown);
        }

        private void ResetLazer()
        {
            _reset = _weaponConfig.DurationOfWork * _weaponConfig.Multiplier;
        }

        private void CreateLazer()
        {
            var beam = _weaponConfig.BeamLength;             
            ProjectileLazerFactory.CreateLazer(beam, _lazerView.gameObject, _weaponConfig.PlayerPrefab.transform);      
        }
        
    }
}