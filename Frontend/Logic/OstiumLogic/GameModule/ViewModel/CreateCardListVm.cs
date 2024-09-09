using System;
using B20.Architecture.Contexts.Api;
using B20.Frontend.Elements;
using GameModule.Api;

namespace Ostium.Logic
{
    public class CreateCardListVm: ElementListVm<CreatureCardVm, CreatureCard>
    {
        public CreateCardListVm(Context c) : base(() => c.Get<CreatureCardVm>())
        {
            
        }
    }
}