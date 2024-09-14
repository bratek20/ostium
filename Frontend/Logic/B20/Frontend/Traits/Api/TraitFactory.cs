using System;

namespace B20.Frontend.Element
{
    public interface TraitFactory
    {
        Trait Create(Type type);
    }
}