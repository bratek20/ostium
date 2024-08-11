using B20.Events.Api;
using B20.Frontend.Element;
using B20.Frontend.Elements;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class HandVm: ElementVm<Hand>
    {
        public ElementListVm<CreatureCardVm> Cards { get; } = new ElementListVm<CreatureCardVm>(); 
        
        private EventPublisher eventPublisher;
        public HandVm(EventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }
        
        public bool Contains(CreatureCardId cardId)
        {
            return Model.GetCards().Exists(card => card.GetId().Equals(cardId));
        }
        

        protected override void OnUpdate()
        {
            var cardsVm = Model.GetCards()
                .ConvertAll(card =>
                {
                    var vm = new CreatureCardVm(eventPublisher);
                    vm.Update(card);
                    return vm;
                });
            
            Cards.Update(cardsVm);
        }
    }
}