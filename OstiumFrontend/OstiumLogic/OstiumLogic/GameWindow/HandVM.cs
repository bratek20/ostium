using B20.Events.Api;
using B20.Frontend.Elements;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class HandVM: ElementVM<Hand>
    {
        public ElementListVM<CreatureCardVM> Cards { get; } = new ElementListVM<CreatureCardVM>(); 
        
        private EventPublisher eventPublisher;
        public HandVM(EventPublisher eventPublisher)
        {
            this.eventPublisher = eventPublisher;
        }

        protected override void OnUpdate()
        {
            var cardsVM = Model.GetCards()
                .ConvertAll(card =>
                {
                    var vm = new CreatureCardVM(eventPublisher);
                    vm.Update(card);
                    return vm;
                });
            
            Cards.Update(cardsVM);
        }
    }
}