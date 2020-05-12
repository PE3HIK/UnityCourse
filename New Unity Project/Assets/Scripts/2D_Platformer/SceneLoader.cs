using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _2D_Platformer
{
    public class SceneLoader : MonoBehaviour
    {
        private static string nextLevel;

        public static void LoadLevel(string level)
        {
            nextLevel = level;
            SceneManager.LoadScene("Loading");
        }

        private IEnumerator Start()
        {
            GameManager.SetGameState(GameState.Loading);
            
            yield return new WaitForSeconds(1f);

            if (String.IsNullOrEmpty(nextLevel) )
            {
                SceneManager.LoadScene("MainMany"); 
                yield break; 
            }

            AsyncOperation loading = null;
            loading = SceneManager.LoadSceneAsync(nextLevel, LoadSceneMode.Additive);

            while (!loading.isDone)
            {
                yield return null; // подождать конца кадра
            }

            nextLevel = null;
            SceneManager.UnloadSceneAsync("Loading"); 
        }
    }
}