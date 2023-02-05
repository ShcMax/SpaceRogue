using Abstracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Shooting
{
    public sealed class LaserFactory
    {
        private readonly ProjectileConfig _config;
        private readonly LaserWeaponConfig _laserConfig;
        private readonly ProjectileView _view;
        private readonly Transform _laserTransform;
        private readonly UnitType _unitType;

        public LaserFactory(ProjectileConfig projectileConfig, LaserWeaponConfig laserWeaponConfig, ProjectileView view, Transform laserTransform, UnitType unitType)
        {
            _config = projectileConfig;
            _laserConfig = laserWeaponConfig;
            _view = view;
            _laserTransform = laserTransform;
            _unitType = unitType;
        }

        public ProjectileLaserController CreateLaser() => CreateLaser();



        

    }
}