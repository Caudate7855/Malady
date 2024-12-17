using System;

namespace Project.Scripts.Core
{
    public interface IApproachable
    {
        public event Action OnApproach;
        public void ApproachCharacter();
    }
}