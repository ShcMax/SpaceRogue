using Abstracts;
using Newtonsoft.Json.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.ResourceManagement;
using Gameplay.Mechanics.Timer;
using Scriptables.Modules;

namespace Gameplay.Shooting
{
    public sealed class ProjectileLazerFactory: BaseController
    {
        public bool IsOnCooldown => CooldownTimer.InProgress;

        protected Timer CooldownTimer;

        private readonly TurretModuleConfig config;
        private readonly LazerWeaponConfig _laserWeaponConfig;
        private readonly ProjectileConfig _projectile;
        private readonly ProjectileView _projectileView;
        private Transform _lazerPosition;
        private readonly ResourcePath _lazerPointPrefab = new(Constants.Prefabs.Stuff.Lazer);
        private UnitType _unitType;


        public ProjectileLazerFactory(LazerWeaponConfig projectileConfig, ProjectileView view,
            Transform projectileSpawnTransform, UnitType unitType)
        {
            _laserWeaponConfig = projectileConfig;
            _projectileView = view;
            _lazerPosition = projectileSpawnTransform;
            _unitType = unitType;

            
        }
        public void CreateLazer(Transform projectileSpawnTransform)
        {
            var lasePointView = ResourceLoader.LoadPrefab(_lazerPointPrefab);
            var laserPoint = Object.Instantiate(lasePointView, projectileSpawnTransform.TransformDirection(0.6f * projectileSpawnTransform.localScale.y * Vector3.up), projectileSpawnTransform.rotation);
            laserPoint.transform.parent = projectileSpawnTransform;
            laserPoint.transform.localScale.Set(0.1f, _laserWeaponConfig.BeamLength, 0);
            laserPoint.SetActive(false);

            CooldownTimer = new Timer(config.SpecificWeapon.Cooldown);

            AddGameObject(laserPoint);
        }
    }    
}