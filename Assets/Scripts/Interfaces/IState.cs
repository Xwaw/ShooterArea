using Entities.Zombie;

namespace Interfaces
{
    public interface IState
    {
        void Enter(AIController controller);
        void Update();
        void Exit();
    }
}