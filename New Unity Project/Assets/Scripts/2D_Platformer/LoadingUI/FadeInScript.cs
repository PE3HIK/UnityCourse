using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace _2D_Platformer.LoadingUI
{
    public class FadeInScript : MonoBehaviour
    {
        private SpriteRenderer rend;

        private void Start()
        {
            rend = GetComponent<SpriteRenderer>();
            Color c = rend.material.color;
            c.a = 0f;
            rend.material.color = c;
        }

        IEnumerator imgFadeIn()
        {
            for (float f = 0.1f; f <= 1; f += 0.1f)
            {
                Color c = rend.material.color;
                c.a = f;
                rend.material.color = c;
                yield return new WaitForSeconds (0.05f);
            }
        }

        public void startFeading()
        {
            StartCoroutine("imgFadeIn");
        }
    }
}
