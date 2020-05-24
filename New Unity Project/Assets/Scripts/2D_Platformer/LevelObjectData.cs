using System;
using UnityEngine;

namespace _2D_Platformer
{
    [CreateAssetMenu(fileName = "Data", menuName = "Object/LevelObject", order = 1)]
    public class LevelObjectData : ScriptableObject
    {
        public string Name = "new level object Name";
        public bool isStatic;
        public int Health = 1; 
    }
}