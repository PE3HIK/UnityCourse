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
    
    private int currentType;
    private Vector3 position; 
    
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
        
        if (obstacle.ColorId == currentType)
        {
            SetupColor(obstacle.ColorId);
            Destroy(obstacle.gameObject);        
        }
        
        
        if (obstacle.ColorId != currentType)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
