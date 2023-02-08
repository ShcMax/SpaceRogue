using Abstracts;
using Gameplay.Mechanics.Meter;
using Gameplay.Player;
using Scriptables.Modules;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;
using Random = System.Random;

namespace Gameplay.Shooting
{
    public sealed class FrontalLazerController : FrontalTurretController
    {
        private readonly LazerWeaponConfig _weaponConfig;  
        private readonly MeterWithCooldown _overheatMeter;    

        public FrontalLazerController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var lazerConfig = config.SpecificWeapon as LazerWeaponConfig;
            _weaponConfig = lazerConfig 
                ? lazerConfig 
                : throw new System.Exception("wrong config type was provided");

            _overheatMeter = new MeterWithCooldown(0.0f, _weaponConfig.DurationOfWork, _weaponConfig.WorkingTime * _weaponConfig.Multiplier);
        }

        protected override void OnDispose()
        {
            
            _overheatMeter.Dispose();
            base.OnDispose();
        }

        public override void CommenceFiring()
        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown) return;
            
                FireLazerProjectile();                      
            
            CooldownTimer.Start();
        }

        private void FireLazerProjectile()
        {           
            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);        
        }
    }   
}