using System;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FitInTheHole
{
    public class FitInTheHole_Template : MonoBehaviour
    {
        [SerializeField] private Transform[] m_Cubes;
        [SerializeField] private Transform m_PlayerPosition;
        [SerializeField] private Transform[] m_PositionsVariants;

        private FitInTheHole_FigureTweener tweener;
        private int currentPosition = -1;

        public Transform CurrentTarget { get; private set; }

        public Transform[] GetFigure()
        {
            var figure = new Transform[m_Cubes.Length + 1];

            m_Cubes.CopyTo(figure, 0);
            figure[figure.Length - 1] = CurrentTarget;
            return figure;
        }

        public void SetupRamdomFigure()
        {
            int rand = Random.Range(0, m_PositionsVariants.Length);

            for (int i = 0; i < m_PositionsVariants.Length; i++)
            {
                if (i == rand)
                {
                    m_PositionsVariants[i].gameObject.SetActive(true);
                    CurrentTarget = m_PositionsVariants[i].transform;
                    continue;
                }

                m_PositionsVariants[i].gameObject.SetActive(false);
            }
        }
        
        private void Update()
        {
            if (tweener != null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MoveLeft();
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MoveRight();
            }
        }

        private void MoveLeft()
        {
            if (!IsMovementPossible(1))
            {
                return;
            }

            currentPosition += 1;
            tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
            tweener.Tween(m_PlayerPosition.position, m_PositionsVariants[currentPosition].position);
        }

        private void MoveRight()
        {
            if (!IsMovementPossible(-1))
            {
                return;
            }

            currentPosition -= 1;
            tweener = m_PlayerPosition.gameObject.AddComponent<FitInTheHole_FigureTweener>();
            tweener.Tween(m_PlayerPosition.position, m_PositionsVariants[currentPosition].position);
        }

        private bool IsMovementPossible(int dir)
        {
            return currentPosition + dir >= 0 && currentPosition + dir < m_PositionsVariants.Length;
        }
    }
}