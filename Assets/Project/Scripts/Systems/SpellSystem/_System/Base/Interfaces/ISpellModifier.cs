using System;

namespace Project.Scripts
{
    public interface ISpellModifier : IDisposable
    {
        void Apply(ISpell spell);
    }
}