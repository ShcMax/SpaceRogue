using Abstracts;
using Scriptables.Modules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.ResourceManagement;
using Gameplay.Mechanics.Timer;

namespace Gameplay.Shooting
{
    public abstract class FrontalTurretLazerController : BaseController
    {
        public bool IsOnCooldown => CooldownTimer.InProgress;

        protected Timer CooldownTimer;

        protected readonly TurretModuleConfig Config;
        protected readonly LazerFactory LazerFactory;

        private readonly ResourcePath _lazerPrefab = new(Constants.Prefabs.Stuff.Lazer);

        public FrontalTurretLazerController(TurretModuleConfig config, Transform gunPointParentTransform, UnitType unitType)
        {
            Config = config;
            var gunPointView = ResourceLoader.LoadPrefab(_lazerPrefab);

            var lazerPoint = Object.Instantiate(
                gunPointView,
                gunPointParentTransform.position + gunPointParentTransform.TransformDirection(
                    0.6f * gunPointParentTransform.localScale.y * Vector3.up),
                gunPointParentTransform.rotation
            );
            lazerPoint.transform.parent = gunPointParentTransform;

            LazerFactory = new LazerFactory(Config.ProjectileConfig, Config.ProjectileConfig.Prefab,
                lazerPoint.transform, unitType);

            CooldownTimer = new Timer(config.SpecificWeapon.Cooldown);

            AddGameObject(lazerPoint);
        }

        protected override void OnDispose()
        {
            CooldownTimer.Dispose();
        }

        public abstract void CommenceFiring();
    }
}