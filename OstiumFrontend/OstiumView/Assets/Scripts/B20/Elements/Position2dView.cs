using B20.Frontend.Elements;
using B20.Types;

namespace B20.View
{
    public class Position2dView: ElementView<Position2dVm>
    {
        protected override void OnViewModelUpdate()
        {
            transform.position = TypesConverter.Convert(ViewModel.Model);
        }
    }
}