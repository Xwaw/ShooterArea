using Interfaces;
using UnityEngine;

namespace Entities.Zombie.States
{
    public class CrawlState : IState
    {
        private AIController _aiController;
        private float _speed;
        public void Enter(AIController controller)
        {
            _aiController = controller;
            _speed = _aiController.Stats.Speed / 5f;
            Debug.Log("Zombie Crawl state entered");
        }

        public void Update()
        {
            _aiController.Movement.Move(_aiController.Follow.GetDirection(), _speed);
            _aiController.Attack.TryAttack(_aiController);
        }

        public void Exit()
        {
            Debug.Log("Zombie Crawl state exit");
        }
    }
}