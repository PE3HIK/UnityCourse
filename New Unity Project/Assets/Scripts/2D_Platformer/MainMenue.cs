using System;
using UnityEngine;

namespace _2D_Platformer
{
    public class MainMenue : MonoBehaviour
    {
        private void Start()
        {
            GameManager.SetGameState(GameState.MainManu);
        }

        public void LoadLevel(string level)
        {
            SceneLoader.LoadLevel(level); 
        }
    }
}