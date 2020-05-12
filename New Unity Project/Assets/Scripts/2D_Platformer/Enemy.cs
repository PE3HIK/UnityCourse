using UnityEngine;

namespace _2D_Platformer
{
    public class Enemy : MonoBehaviour , IEnemy, IHitBox
    {
        [SerializeField] private int health = 1; 

        public void RegisterIEnemy()
        {
            GameManager manager = FindObjectOfType<GameManager>();
            manager.Enemies.Add(this);
        }
        public void Awake()
        {
            RegisterIEnemy();
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
            print("Противник мёртв");
        }
    }
   

    
}