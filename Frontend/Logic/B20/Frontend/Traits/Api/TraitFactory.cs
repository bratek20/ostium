using System;

namespace B20.Frontend.UiElements
{
    public interface TraitFactory
    {
        Trait Create(Type type);
        T Create<T>() where T : Trait;
    }
}