using Abstracts;
using Gameplay.Damage;
using Gameplay.Mechanics.Meter;
using Scriptables.Modules;
using System.Reflection;
using UnityEngine;
using Utilities.Mathematics;

namespace Gameplay.Shooting
{
    public sealed class FrontalLazerController : FrontalTurretController
    {
        private readonly LazerWeaponConfig _weaponConfig;
        private readonly MeterWithCooldown _overheatMeter;

        private readonly ProjectileLazerConfig _projectileLazerConfig;       

        private float _durationOfWork;
        public FrontalLazerController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var lazerConfig = config.SpecificWeapon as LazerWeaponConfig;
            _weaponConfig = lazerConfig
            ? lazerConfig
            : throw new System.Exception("wrong config type was provided");

            _overheatMeter = new MeterWithCooldown(0.0f, _weaponConfig.DurationOfWork, _weaponConfig.WorkingTime);
            _overheatMeter.OnCooldownEnd += ResetLazer;  
        }

        public override void CommenceFiring()
        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown) return;
            FireLazer();
            AddHeat();
            CooldownTimer.Start();            
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
            _durationOfWork = _weaponConfig.WorkingTime + (_weaponConfig.DurationOfWork * _weaponConfig.Multiplier);
        }

        private void FireLazer()
        {           
            LazerFactory.CreateBeam();
        }               
    }
}