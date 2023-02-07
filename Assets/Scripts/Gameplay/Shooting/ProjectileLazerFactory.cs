using Abstracts;
using Newtonsoft.Json.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.Shooting
{
    public sealed class ProjectileLazerFactory
    {
        private readonly LazerWeaponConfig _laserWeaponConfig;
        private readonly ProjectileView _projectileView;
        private Transform _lazerPosition;
        private UnitType _unitType;


        public ProjectileLazerFactory(LazerWeaponConfig lazerWeaponConfig ,ProjectileView projectileView, Transform lazerPosition, UnitType unitType)
        {
            _laserWeaponConfig = lazerWeaponConfig;
            _projectileView = projectileView;
            _lazerPosition = lazerPosition;
            _unitType = unitType;
        }
        public void CreateLazer(float beam, GameObject lazer, Transform parent)
        {
            var playerPosition = _laserWeaponConfig.PlayerPrefab.transform;
            Vector3 position = new Vector3(playerPosition.position.x, playerPosition.position.y + _laserWeaponConfig.BeamLength / 2, 0);
            var _laser = GameObject.Instantiate(lazer, position,parent.rotation) as GameObject;
            _laser.transform.parent = parent;
            _laser.transform.localScale = new Vector3(0.1f, _laserWeaponConfig.BeamLength, 0);
        }
    }    
}