using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_Stick; // 
    [SerializeField] private StickHeroPlayer m_Player;
    [SerializeField] private StickHeroPlatform [] m_Platforms;

    private int counter; 
    public enum EGameState
    {
        Wait,
        Scaling,
        Rotate,
        Movement,
        Defeate,
    }

    private EGameState currentGameState; 

    // Start is called before the first frame update
    void Start()
    {
        currentGameState = EGameState.Wait;
        counter = 0;

        m_Stick.ResetStick(m_Platforms[0].GetStickPosition());  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) == false)
        {
            return; 
        }
        switch (currentGameState)
        {
            // если не осуществлён страт игры
            case EGameState.Wait:
                currentGameState = EGameState.Scaling;
                m_Stick.StartScaling();
                break;
            
            // стик увеличивается - прерываем увеличение и запускаем поворот
            case EGameState.Scaling:
                currentGameState = EGameState.Rotate;
                m_Stick.StopScaling();
                break;

            // ничего не делаем 
            case EGameState.Rotate:
                break;

            // ничего не делаем 
            case EGameState.Movement:
                break;

            //
            case EGameState.Defeate:
                print("Defeat");
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                break;
            
            default:
                break;
        }

    }
    public void StopStickScale()
    {
        currentGameState = EGameState.Rotate;
        m_Stick.StartRotate();
    }
    public void StopStickRotate()
    {
        currentGameState = EGameState.Movement;
    }

    public void StartPlayerMovement(float length)
    {
        currentGameState = EGameState.Movement;

        StickHeroPlatform nextPlatform = m_Platforms[counter + 1];

        // находим min длину стика
        float targetLength = nextPlatform.transform.position.x - m_Stick.transform.position.x;

        float platformSize = nextPlatform.GetPlatformSize();

        float min = targetLength - platformSize * 0.5f;
        min -= m_Player.transform.localScale.x * 0.9f;

        // находим мах длину стика
        float max = targetLength + platformSize * 0.5f; 

        //при успехе переходим в центр следующей платформы
        if (length < min || length > max)
        {
            // будем падать
            float targetPosition = m_Stick.transform.position.x + length + m_Player.transform.localScale.x;

            m_Player.StartMovement(targetPosition, true);
        }
        else
        {
            float targetPosition = nextPlatform.transform.position.x;
            m_Player.StartMovement(targetPosition, false);
        }
    }
    public void StopPlayerMovement()
    {
        currentGameState = EGameState.Wait;
        counter++;
        m_Stick.ResetStick(m_Platforms[counter].GetStickPosition());
    }

    public void ShowScores()
    {
        currentGameState = EGameState.Defeate;

        print($"Game Over at { counter}");
    }
}
