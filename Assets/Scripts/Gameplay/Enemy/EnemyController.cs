using Abstracts;
using Gameplay.Enemy.Behaviour;
using Gameplay.Health;
using Gameplay.Movement;
using Gameplay.Player;
using Gameplay.Shooting;
using Scriptables;
using Scriptables.Enemy;
using Scriptables.Health;
using Scriptables.Modules;
using System.Collections.Generic;
using UI.Game;
using UnityEngine;
using Utilities.Mathematics;
using Utilities.ResourceManagement;

namespace Gameplay.Enemy
{
    public sealed class EnemyController : BaseController
    {
        private readonly EnemyView _view;
        private readonly EnemyConfig _config;
        private readonly FrontalTurretController _turret;
        private readonly EnemyBehaviourController _behaviourController;
        private readonly EnemyHealthController _enemyHealthController;
        private readonly PlayerController _playerController;
        private readonly System.Random _random = new();

        private readonly ResourcePath _enemyHealthStatusBarCanvasPath = 
            new(Constants.Prefabs.Canvas.Game.EnemyHealthStatusBarCanvas);
        private readonly ResourcePath _enemyHealthShieldStatusBarCanvasPath = 
            new(Constants.Prefabs.Canvas.Game.EnemyHealthShieldStatusBarCanvas);

        public EnemyController(EnemyConfig config, EnemyView view, PlayerController playerController)
        {
            _playerController = playerController;
            _config = config;
            _view = view;
            AddGameObject(_view.gameObject);
            _turret = WeaponFactory.CreateFrontalTurret(PickTurret(_config.TurretConfigs, _random), _view.transform);
            AddController(_turret);
            
            var movementModel = new MovementModel(_config.Movement);
            _behaviourController = new EnemyBehaviourController(movementModel, _view, _turret, _playerController, _config.Behaviour);
            AddController(_behaviourController);

            _enemyHealthController = AddEnemyHealthController(_config.Health, _config.Shield);
        }

        private EnemyHealthController AddEnemyHealthController(HealthConfig healthConfig, ShieldConfig shieldConfig)
        {
            var enemyHealthController = shieldConfig is null
                ? new EnemyHealthController(healthConfig, 
                AddHealthStatusBarView(GameUIController.EnemyHealthBars), _view, _config.HealthBarOffset)
                : new EnemyHealthController(healthConfig, shieldConfig, 
                AddHealthShieldStatusBarView(GameUIController.EnemyHealthBars), _view, _config.HealthBarOffset);
            
            enemyHealthController.SubscribeToOnDestroy(Dispose);
            AddController(_enemyHealthController);
            return enemyHealthController;
        }

        private HealthStatusBarView AddHealthStatusBarView(Transform transform)
        {
            var enemyStatusBarView = ResourceLoader.LoadPrefabAsChild<HealthStatusBarView>
                (_enemyHealthStatusBarCanvasPath, transform);
            AddGameObject(enemyStatusBarView.gameObject);
            return enemyStatusBarView;
        }
        
        private HealthShieldStatusBarView AddHealthShieldStatusBarView(Transform transform)
        {
            var enemyStatusBarView = ResourceLoader.LoadPrefabAsChild<HealthShieldStatusBarView>(_enemyHealthShieldStatusBarCanvasPath, transform);
            AddGameObject(enemyStatusBarView.gameObject);
            return enemyStatusBarView;
        }

        private TurretModuleConfig PickTurret(List<WeightConfig<TurretModuleConfig>> weaponConfigs, System.Random random) =>
            RandomPicker.PickOneElementByWeights(weaponConfigs, random);
    }
}