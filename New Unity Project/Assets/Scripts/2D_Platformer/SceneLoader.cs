using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace _2D_Platformer
{
    public class SceneLoader : MonoBehaviour
    {
        [SerializeField] private GameObject fade; 
        public float loadTime = 10f; 
        private static string nextLevel;

        public static void LoadLevel(string level)
        {
            nextLevel = level;
            SceneManager.LoadScene("Loading");
        }

        private IEnumerator Start()
        {
            GameManager.SetGameState(GameState.Loading);

            yield return new WaitForSeconds(loadTime);

            if (String.IsNullOrEmpty(nextLevel) )
            {
                fade.SetActive(true);
                yield return new WaitForSeconds (0.7f); 
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