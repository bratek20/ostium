using B20.Events.Api;
using B20.Ext;
using B20.Frontend.Element;
using GameComponents.Api;
using GameSetup.Api;

namespace Ostium.Logic
{
    public class RowVM: PanelVM<Optional<CreatureCard>>
    {
        public RowType Type { get; }
        public CreatureCardVM Card { get; }

        public bool HasCard => Model.IsPresent();
        
        public bool Contains(CreatureCardId cardId)
        {
            return Model.Map(card => card.GetId().Equals(cardId)).OrElse(false);
        }
        
        public RowVM(RowType type, EventPublisher publisher): base(publisher)
        {
            Clickable = true;
            
            Type = type;
            Card = new CreatureCardVM(publisher);
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