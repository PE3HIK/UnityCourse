using System;
using System.Collections;
using UnityEngine;

namespace _2D_Platformer
{
    public class Test : MonoBehaviour
    {
        private void Start()
        {
            throw new NotImplementedException();
        }

        private IEnumerator WaitAndPtint(string txt)
        {
            yield return new WaitForSeconds(2f);
            Debug.Log(txt);
        }
    }
}