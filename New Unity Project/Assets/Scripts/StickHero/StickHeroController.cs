using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StickHeroController : MonoBehaviour
{
    [SerializeField] private StickHeroStick m_Stick; // 
    [SerializeField] private StickHeroPlayer m_Player;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private float[] spawnSteps;
    [SerializeField] private StickHeroPlatform[] m_PlatformPrefabs;
    [SerializeField] private Text Text;


    private List<StickHeroPlatform> m_Platforms = new List<StickHeroPlatform>();

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

        SpawnNext(0);
        SpawnNext();
        SpawnNext();

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
            // если не осуществлён старт игры
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


    public void SpawnNext()
    {
        var id = Random.Range(0, m_PlatformPrefabs.Length);
        SpawnNext(id);
    }


    public void SpawnNext(int id)
    {
        var platform = Instantiate(m_PlatformPrefabs[id]);
        if (m_Platforms.Count == 0)
        {
            platform.transform.position = spawnPoint;
        }
        else
        {
            var lastPlatformPosition = m_Platforms[m_Platforms.Count - 1].transform.position;
            var offsetLeft = m_Platforms[m_Platforms.Count - 1].transform.localScale.x * 0.5f;
            var offsetRight = platform.transform.localScale.x * 0.5f;
            var offset = spawnSteps[Random.Range(0, spawnSteps.Length)];
            platform.transform.position = new Vector3(offset + offsetLeft + offsetRight, 0f, 0f) + lastPlatformPosition;
            //Debug.Log($"{offset}   {offsetLeft}   {offsetRight}");
        }


        m_Platforms.Add(platform);
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
            SpawnNext();
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
        Text.text = $"Game Over at {counter}";
        print($"Game Over at {counter}");
    }
}