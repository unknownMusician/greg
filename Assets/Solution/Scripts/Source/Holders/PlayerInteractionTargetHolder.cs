using AreYouFruits.Nullability;
using Greg.Components;

namespace Greg.Holders
{
    public sealed class PlayerInteractionTargetHolder
    {
        public Optional<InteractionTargetComponent> Value { get; set; } = Optional.None();
    }
}