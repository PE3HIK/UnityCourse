using System.Collections;
using System.Collections.Generic;
using ColorSnake;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class ColorSnake_LvLGenerotor : MonoBehaviour
{
    [SerializeField] private ColorSnake_Types m_Types;
    [SerializeField] private ColorSnake_GameController m_Controller;

    private int Line = 1;
    private List<GameObject> Obstacles = new List<GameObject>();
    private List<int> ObstaclesTemp = new List<int> ();

    // Start is called before the first frame update
    void Start()
    {
        var upBorder = m_Controller.Bounds.Up;
        while (Line *4f < upBorder + 2f)
        {
            GenerateObstacle(); 
        }
    }

    // Update is called once per frame
    
    void Update()
    {
        var upBorder = m_Controller.Bounds.Up + m_Controller.Camera.transform.position.y;
        if (Line * 2 > upBorder + 2)
        {
                return;
        }  
        GenerateObstacle();


        for (int i = 0; i < Obstacles.Count; i++)
        {
            if (Obstacles[i].transform.position.y <
                m_Controller.Camera.transform.position.y + m_Controller.Bounds.Down * 1.5)
            {
                Destroy(Obstacles[i]);
                ObstaclesTemp.Add(i);
            }
        }

        for (int i = 0; i < ObstaclesTemp.Count; i++)
        {
            Obstacles.RemoveAt(ObstaclesTemp[i]);
        }
        
        ObstaclesTemp.Clear();
    }

    private void GenerateObstacle()
    {
        if (Line%3 != 0)
        {
            var template = m_Types.GetRandomOTemplateType();
            var obstacle = new GameObject($"Obstacle_{Line}");

            foreach (var point in template.points)
            {
                var objType = m_Types.GetRandomObjectType();
                var colorTyepe = m_Types.GetRandomColorType();

                var obj = Instantiate(objType.Object, point.position, point.rotation);
                obj.transform.parent = obstacle.transform;

                obj.GetComponent<SpriteRenderer>().color = colorTyepe.color;

                var obstacleComponent = obj.AddComponent<ColorSnake_Obstacles>();
                obstacleComponent.ColorId = colorTyepe.Id;
                obstacleComponent.ObjectType = 0; 
            }

            Vector3 pos = obstacle.transform.position;
            pos.y = Line * 2;

            obstacle.transform.position = pos;
        
            Line ++;
            Obstacles.Add(obstacle);
        }
        else
        {
            var template = m_Types.GetColorChangerTemplateType();
            var obstacle = new GameObject($"Obstacle_{Line}");

            foreach (var point in template.points)
            {
                var objType = m_Types.GetColorChangerObjectType();
                var colorTyepe = m_Types.GetRandomColorType();

                var obj = Instantiate(objType.Object, point.position, point.rotation);
                obj.transform.parent = obstacle.transform;

                obj.GetComponent<SpriteRenderer>().color = colorTyepe.color;

                var obstacleComponent = obj.AddComponent<ColorSnake_Obstacles>();
                obstacleComponent.ColorId = colorTyepe.Id;
                obstacleComponent.ObjectType = 1; 
            }

            Vector3 pos = obstacle.transform.position;
            pos.y = Line * 2;

            obstacle.transform.position = pos;
        
            Line ++;
            Obstacles.Add(obstacle);
        }
    }
}
