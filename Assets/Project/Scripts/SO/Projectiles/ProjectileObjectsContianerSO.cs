using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Project.Scripts
{
    [CreateAssetMenu(menuName = "SO/Projectiles/ProjectileObjectsContainerSO", fileName = nameof(ProjectileObjectsContianerSO))]
    public class ProjectileObjectsContianerSO : ScriptableObjectInstaller<ProjectileObjectsContianerSO>
    {
        [SerializeField] private List<Projectile> Projectiles;

        public Projectile GetProjectileByType(ProjectileType type)
        {
            foreach (var projectile in Projectiles)
            {
                if (projectile.ProjectileType == type)
                {
                    return projectile;
                }   
            }
            
            throw new Exception("No projectile found with type " + type);
        }
    }

    [Serializable]
    public class Projectile
    {
        public ProjectileType ProjectileType;
        public GameObject ProjectilePrefab;
    }
}