using Abstracts;
using Gameplay.Damage;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Gameplay.Shooting
{
    public sealed class ProjectileLazerController : BaseController
    {
        private readonly ProjectileLazerConfig _config;
        private readonly ProjectileLazerView _view;
        private readonly LazerWeaponConfig _lazerWeaponConfig;

        private readonly Transform _position;

        private float _remainingLifeTime;

        public ProjectileLazerController(ProjectileLazerConfig config, ProjectileLazerView view, Transform position, UnitType unitType)
        {
            _config = config;
            _view = view;
            _position = position;
            _remainingLifeTime = _config.LifeTime;            

            AddGameObject(_view.gameObject);

            var damageModel = new DamageModel(config.DamageAmount, unitType);
            _view.Init(damageModel); 

            if (config.IsDestroyedOnHit) _view.CollisionEnter += Dispose;

            EntryPoint.SubscribeToUpdate(TickDown); 
        }

        protected override void OnDispose()
        {
            _view.CollisionEnter -= Dispose;
            EntryPoint.UnsubscribeFromUpdate(TickDown);
        }
        private void TickDown(float deltaTime)
        {
            if (_remainingLifeTime <= 0)
            {
                Dispose();
                return;
            }

            _view.transform.SetParent(_position, false);
           
            _view.transform.localScale =  new Vector3(_config.BeamWidth, _config.BeamLength, 0);            

            _remainingLifeTime += deltaTime;
        }
    }
}