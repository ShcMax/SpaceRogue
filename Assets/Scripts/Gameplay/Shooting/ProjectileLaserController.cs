using Abstracts;
using Gameplay.Damage;
using Gameplay.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Utilities.ResourceManagement;

namespace Gameplay.Shooting
{
    public sealed class ProjectileLaserController : BaseController
    {
        private readonly ProjectileConfig _config;
        private readonly LaserWeaponConfig _laserConfig;
        private readonly ProjectileView _laserView;
        private readonly PlayerView _playerView;
        private float _remainingLifeTime;
        private readonly ResourcePath _laserPrefabPath = new(Constants.Prefabs.Stuff.Crosshair);

        public  ProjectileLaserController(ProjectileConfig config, LaserWeaponConfig laserConfig, ProjectileView view, PlayerView playerView, UnitType unitType)
        {
            _config = config;
            _laserConfig = laserConfig;
            _laserView = view;
            _playerView = playerView;
            AddGameObject(_laserView.gameObject);

            _remainingLifeTime = config.LifeTime;

            var damageModel = new DamageModel(config.DamageAmount, unitType);
            _laserView.Init(damageModel);
            if (config.IsDestroyedOnHit) _laserView.CollisionEnter += Dispose;

            EntryPoint.SubscribeToUpdate(TickDown);

            AddLaser();
        }
        protected override void OnDispose()
        {
            _laserView.CollisionEnter -= Dispose;
            EntryPoint.UnsubscribeFromUpdate(TickDown);
        }

        private void TickDown(float deltaTime)
        {
            if (_remainingLifeTime <= 0)
            {
                Dispose();
                return;
            }

            var transform = _laserView.transform;
            transform.position += _playerView.transform.position;

            _remainingLifeTime -= deltaTime;
        }

        private void AddLaser()
        {
            var laserView = ResourceLoader.LoadPrefab(_laserPrefabPath);
            var viewTransform = _playerView.transform;
            var laser = UnityEngine.Object.Instantiate(
                laserView,
                viewTransform.position + _playerView.transform.TransformDirection(Vector3.up * (viewTransform.localScale.y + 15f)),
                viewTransform.rotation
            );
            laser.transform.parent = _playerView.transform;            
            AddGameObject(laser);
        }
    }
}