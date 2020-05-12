using UnityEngine;

namespace _2D_Platformer
{
    public class Player : MonoBehaviour, IPlayer, IHitBox
    {
        [SerializeField] private int health = 1; 
        public void RegisterPlayer()
        {
            GameManager manager = FindObjectOfType<GameManager>();
            if (manager.Player == null)
            {
                manager.Player = this; 
            }
            else
            {
                Destroy(this.gameObject);
            }
        }
        public void Awake()
        {
            RegisterPlayer();
        }

        public int Health
        {
            get => health;
            private set
            {
                health = value;
                if (health<=0f)
                {
                    Die();
                }
            }
        }
        public void Hit(int damage)
        {
            Health -= damage;
        }

        public void Die()
        {
            print("Игрок мёртв");
        }
    }
}