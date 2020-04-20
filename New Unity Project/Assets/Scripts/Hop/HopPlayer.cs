using System;
using Hop;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HopPlayer: MonoBehaviour
    {
        [SerializeField] private AnimationCurve m_JumpCurve;
        [SerializeField] private float m_JumpHeigh = 1f;
        [SerializeField] private float m_JumpDistance = 2f;

        [SerializeField] private float m_BallSpeed = 1f;
        [SerializeField] private HopInput m_Input;
        [SerializeField] private HopTrack m_Track;

        private float iteration;
        private float startZ;

        private void Update()
        {
            Vector3 pos = transform.position;
            
            // смещение 
            pos.x = Mathf.Lerp(pos.x, m_Input.Strafe, Time.deltaTime * 5f);
            
            // прыжок 
            pos.y = m_JumpCurve.Evaluate(iteration) * m_JumpHeigh;
            
            //движение вперёд
            pos.z = startZ + iteration * m_JumpDistance;
            
            transform.position = pos;

            // увеличиваем счётчик прыжка
            iteration += Time.deltaTime * m_BallSpeed;

            if (iteration < 1f)
            {
                return;
            }

            iteration = 0f;
            startZ += m_JumpDistance;
            if (m_Track.ISBallOnPlatform(transform.position))
            {
                return;
            }

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }