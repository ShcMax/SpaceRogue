using Abstracts;
<<<<<<< Updated upstream
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
=======
using UnityEngine;
using UnityEngine.UIElements;
>>>>>>> Stashed changes

namespace Gameplay.Shooting
{
    public sealed class LaserFactory
    {
        private readonly ProjectileConfig _config;
<<<<<<< Updated upstream
        private readonly LaserWeaponConfig _laserConfig;
        private readonly ProjectileView _view;
        private readonly Transform _laserTransform;
        private readonly UnitType _unitType;

        public LaserFactory(ProjectileConfig projectileConfig, LaserWeaponConfig laserWeaponConfig, ProjectileView view, Transform laserTransform, UnitType unitType)
=======
        private readonly ProjectileView _view;
        private readonly LaserWeaponConfig _laserConfig;

        private readonly Transform _projectileSpawnTransform;
        private readonly UnitType _unitType;

        public LaserFactory(ProjectileConfig projectileConfig, LaserWeaponConfig laserWeaponConfig ,ProjectileView view,
            Transform projectileSpawnTransform, UnitType unitType)
>>>>>>> Stashed changes
        {
            _config = projectileConfig;
            _laserConfig = laserWeaponConfig;
            _view = view;
<<<<<<< Updated upstream
            _laserTransform = laserTransform;
            _unitType = unitType;
        }

        public ProjectileLaserController CreateLaser() => CreateLaser();



        

=======
            _projectileSpawnTransform = projectileSpawnTransform;
            _unitType = unitType;
        }

        public ProjectileController CreateProjectile() => CreateProjectile(Vector3.up);
        public ProjectileController CreateProjectile(Vector3 direction) => new(_config, CreateProjectileView(), _projectileSpawnTransform.parent.TransformVector(direction), _unitType);
        private ProjectileView CreateProjectileView() => Object.Instantiate(_view, _projectileSpawnTransform.position, Quaternion.LookRotation(Vector2.up));
>>>>>>> Stashed changes
    }
}