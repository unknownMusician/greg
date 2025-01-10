using AreYouFruits.Events;

namespace Greg.Global
{
    public interface IHandlerOrderer
    {
        public void Order(GroupGraphOrderer orderer);
    }
}
