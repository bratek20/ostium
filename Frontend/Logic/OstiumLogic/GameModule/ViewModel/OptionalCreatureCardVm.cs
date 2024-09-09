using B20.Frontend.Elements;
using GameModule.Api;

namespace Ostium.Logic
{
    public class OptionalCreatureCardVm: OptionalElementVm<CreatureCardVm, CreatureCard>
    {
        public OptionalCreatureCardVm(CreatureCardVm element) : base(element)
        {
        }
    }
}