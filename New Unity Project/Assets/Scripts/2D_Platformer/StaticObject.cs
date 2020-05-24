using System;
using UnityEngine;

namespace _2D_Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]

    public class StaticObject : MonoBehaviour , IHitBox
    {
        [SerializeField] private LevelObjectData m_ObjectData;
        private int health = 1;
        private Rigidbody2D rigidbody2D;
        private void Start()
        {
            health = m_ObjectData.Health;
            rigidbody2D = GetComponent<Rigidbody2D>();
            rigidbody2D.bodyType = m_ObjectData.isStatic ? RigidbodyType2D.Static : RigidbodyType2D.Dynamic; 
        }

#if UNITY_EDITOR //этот код доступен только в редокторе
        [ContextMenu("Rename this")]
        private void Rename()
        {
            if (m_ObjectData != null)
            {
                gameObject.name = m_ObjectData.Name; 
            }
        }

        [ContextMenu("Move Right")]
        private void MoveRight()
        {
            Move(Vector2.right);
        }
        [ContextMenu("Move Left")]

        private void MoveLeft()
        {
            Move(Vector2.left);
        }
        
        [ContextMenu("Copu and Move Up")]
        private void MoveUp()
        {
            Instantiate(this); 
            Move(Vector2.up);
        }
        
        [ContextMenu("Move")]

        private void Move(Vector2 direction)
        {
            var collider = GetComponent<Collider2D>();
            var size = collider.bounds.size; 
            transform.Translate(direction*size);
        }
        #endif
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
            Destroy(gameObject);
        }
    }
}