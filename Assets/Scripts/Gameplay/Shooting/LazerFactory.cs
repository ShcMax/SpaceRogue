using Abstracts;
using Gameplay.Damage;
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
    public LazerFactory(ProjectileLazerConfig lazerConfig,  ProjectileLazerView view,
        Transform projectileSpawnTransform, UnitType unitType)
    {
        _config = lazerConfig;        
        _view = view;        
        _projectileSpawnTransform = projectileSpawnTransform;
        _unitType = unitType;
    }

    public ProjectileLazerController CreateLazer() => CreateLazer(_projectileSpawnTransform);
    public ProjectileLazerController CreateLazer(Transform position) => new(_config, CreateProjectileView(), position, _unitType);
    private ProjectileLazerView CreateProjectileView() => Object.Instantiate(_view);
    
}
