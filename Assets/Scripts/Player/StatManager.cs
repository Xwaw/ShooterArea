using Entities;
using Entities.Interfaces;
using Interfaces;

namespace Player
{
    public class StatManager : EntityData, IStats
    {
        public float Damage
        {
            get => damage;
            set => damage = value;
        }

        public float AttackSpeed
        {
            get => attackSpeed;
            set => attackSpeed = value;
        }

        public float Defence
        {
            get => defense;
            set => defense = value;
        }

        public float Speed
        {
            get => speed;
            set => speed = value;
        }

        public float JumpForce
        {
            get => jumpForce;
            set => jumpForce = value;
        }
    }
}
