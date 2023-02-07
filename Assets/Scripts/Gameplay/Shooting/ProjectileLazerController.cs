using Abstracts;
using Gameplay.Damage;
using Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Shooting
{
    public sealed class ProjectileLaserController : BaseController
    {
        private readonly ProjectileConfig _projectileConfig;
        private readonly LazerWeaponConfig _laserWeaponConfig;
        private readonly ProjectileView _projectileView;
        private readonly PlayerView _playerView;        
        private float _remainingLifeTime;        


        ProjectileLaserController(ProjectileConfig projectileConfig, LazerWeaponConfig laserWeaponConfig, ProjectileView projectileView, UnitType unitType)
        {
            _projectileConfig = projectileConfig;
            _laserWeaponConfig = laserWeaponConfig;
            _projectileView = projectileView;
            _playerView = _laserWeaponConfig.PlayerPrefab;
            _remainingLifeTime = _projectileConfig.LifeTime;

            AddGameObject(_projectileView.gameObject);
            AddGameObject(_playerView.gameObject);

            var damageModel = new DamageModel(_projectileConfig.DamageAmount, unitType);
            _projectileView.Init(damageModel);

            if (_projectileConfig.IsDestroyedOnHit) _projectileView.CollisionEnter += Dispose;           
        }

        private void TickDown(float deltaTime)
        {
            if (_remainingLifeTime <= 0)
            {
                Dispose();
                return;
            }
            var transform = _projectileView.transform;
            transform.position += _playerView.transform.position + Vector3.up;
            transform.localScale.Set(0.1f, _laserWeaponConfig.BeamLength, 0);

            _remainingLifeTime -= deltaTime;            
        }
    }
}
