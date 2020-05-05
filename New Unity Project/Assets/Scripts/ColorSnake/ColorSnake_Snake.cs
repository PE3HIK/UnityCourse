using System;
using System.Collections;
using System.Collections.Generic;
using ColorSnake;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ColorSnake_Snake : MonoBehaviour
{
    [SerializeField] private ColorSnake_GameController m_GameController;
    [SerializeField] private SpriteRenderer m_SpriteRenderer;
    
    [SerializeField] private Text m_ScoreText;
    [SerializeField] private Text m_FinishText1;
    [SerializeField] private Text m_FinishText2;

    private int currentType;
    private Vector3 position; 
    public int counter = 0;

    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        var colorType = m_GameController.Types.GetRandomColorType();
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.color;
    }

    // Update is called once per frame
    private  void Update()
    {
        position = transform.position;

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        position.x = m_GameController.Camera.ScreenToWorldPoint(Input.mousePosition).x;
        position.x = Mathf.Clamp(position.x, m_GameController.Bounds.Left, m_GameController.Bounds.Right); // Mathf.Clamp - ограничивает позицию в пределах

        transform.position = position; 
    }

    
    private void SetupColor(int id)
    {
        var colorType = m_GameController.Types.GetColrType(id);
        currentType = colorType.Id;
        m_SpriteRenderer.color = colorType.color;  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        var obstacle = other.gameObject.GetComponent<ColorSnake_Obstacles>();

        if (obstacle == null)
        {
            return;
        }

        if (obstacle.ObjectType == 0)
        {
            if (obstacle.ColorId == currentType)
            {
                SetupColor(obstacle.ColorId);
                Destroy(obstacle.gameObject);
                counter++;
                m_ScoreText.text = $"{counter}";
            }

            if (obstacle.ColorId != currentType)
            {
                m_ScoreText.text = null;
                
                m_FinishText1.text = $"Твой счёт {counter}";
                m_FinishText2.text = $"для продолжения жми ПРОБЕЛ";
                Time.timeScale = 0;

                if(Input.GetMouseButton(0))
                {
                    Time.timeScale = 1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }

        if (obstacle.ObjectType == 1)
        {
            SetupColor(obstacle.ColorId);
            Destroy(obstacle.gameObject);
        }
        
        if (obstacle.ObjectType == 2)
        {
            Destroy(obstacle.gameObject);

           SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 

        }
    }
}
