using System;
using UnityEngine;

namespace _2D_Platformer
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Object/Weapon object", order = 0)]
    public class WeaponData : ScriptableObject
    {
        public string WeaponName = "Weapon Name";
        public int damage = 1;
        public float range = 1;
        public float fireRate = 1f;
        public GameManager bullet;
        public float bulletSpeed; 
    }
}