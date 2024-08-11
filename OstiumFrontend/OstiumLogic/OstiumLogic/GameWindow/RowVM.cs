using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using B20.Frontend.Traits;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class RowVM: ElementVm<Optional<CreatureCard>>
    {
        public RowType Type { get; }
        public CreatureCardVm Card { get; }
        
        public bool HasCard => Model.IsPresent();
        
        public bool Contains(CreatureCardId cardId)
        {
            return Model.Map(card => card.GetId().Equals(cardId)).OrElse(false);
        }
        
        public RowVM(RowType type, EventPublisher publisher)
        {
            AddTrait(new Clickable(publisher));
            AddTrait(new WithRect());

            Type = type;
            Card = new CreatureCardVm(publisher);
        }
        
        protected override void OnUpdate()
        {
            Model.Let(card =>
            {
                Card.Update(card);
            });
        }
    }
}