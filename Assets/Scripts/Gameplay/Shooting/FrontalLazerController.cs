using Abstracts;
using Gameplay.Mechanics.Meter;
using Scriptables.Modules;
using UnityEngine;
using Utilities.Mathematics;
using Random = System.Random;

namespace Gameplay.Shooting
{
    public sealed class FrontalLazerController : FrontalTurretController
    {
        private readonly LazerWeaponConfig _weaponConfig;
        
        private readonly MeterWithCooldown _overheatMeter;
        
        private float _currentSprayAngle;

        public FrontalLazerController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType) : base(config, gunPointParentTransform, unitType)
        {
            var minigunConfig = config.SpecificWeapon as LazerWeaponConfig;
            _weaponConfig = minigunConfig 
                ? minigunConfig 
                : throw new System.Exception("wrong config type was provided");

            _overheatMeter = new MeterWithCooldown(0.0f, _weaponConfig.DurationOfWork, _weaponConfig.WorkingTime);
                       
        }

        protected override void OnDispose()
        {
            
            _overheatMeter.Dispose();
            base.OnDispose();
        }

        public override void CommenceFiring()
        {
            if (_overheatMeter.IsOnCooldown || IsOnCooldown) return;

            for(int i = 0; i < _weaponConfig.CountBeam; i++)
            {
                FireSingleProjectile();
            }           
            
            CooldownTimer.Start();
        }

        private void FireSingleProjectile()
        {           
            var projectile = ProjectileFactory.CreateProjectile();
            AddController(projectile);           
        }
    }   
}