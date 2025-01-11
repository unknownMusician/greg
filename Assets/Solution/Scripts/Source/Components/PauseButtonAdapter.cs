using Greg.Events;
using Greg.Global.Api;
using Greg.Utils;

namespace Greg.Components
{
    public sealed class PauseButtonAdapter : ButtonAdapter
    {
        protected override void OnClick()
        {
            EventContext.Bus.Invoke(new PauseButtonClickedEvent());
        }
    }
}