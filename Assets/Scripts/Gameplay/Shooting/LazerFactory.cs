using Abstracts;
using Gameplay.Shooting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities.ResourceManagement;

public sealed class LazerFactory
{
    private readonly ProjectileLazerConfig _config;    
    private readonly ProjectileLazerView _view;  

    private readonly Transform _projectileSpawnTransform;

    private readonly UnitType _unitType;

    private GameObject _projectile;
    public LazerFactory(ProjectileLazerConfig lazerConfig,  ProjectileLazerView view,
        Transform projectileSpawnTransform, UnitType unitType)
    {
        _config = lazerConfig;        
        _view = view;        
        _projectileSpawnTransform = projectileSpawnTransform;
        _unitType = unitType;        
    }

    public ProjectileLazerController CreateLazer() => CreateLazer(Vector3.up);
    public ProjectileLazerController CreateLazer(Vector3 direction) => new(_config, CreateProjectileView(), _projectileSpawnTransform.parent.TransformDirection(direction), _unitType);
    private ProjectileLazerView CreateProjectileView() => Object.Instantiate(_view, _projectileSpawnTransform.transform.position, _projectileSpawnTransform.rotation);


    public void CreateBeam()
    {
        _projectile = _config.Prefab.gameObject;
        _projectile.transform.localScale = new Vector3(_config.BeamWidth, _config.BeamLength, 0);
        _projectile.transform.position = new Vector3(0, _config.BeamPosition);
        _projectile = Object.Instantiate(_projectile) as GameObject;
        _projectile.transform.SetParent(_projectileSpawnTransform, false);
    } 

    public void DestroyBeam()
    {
        GameObject.Destroy(_projectile);
    }
}
