using System.Collections;
using System.Collections.Generic;
using ColorSnake;
using UnityEngine;

public class ColorSnake_LvLGenerotor : MonoBehaviour
{
    [SerializeField] private ColorSnake_Types m_Types;
    [SerializeField] private ColorSnake_GameController m_Controller;

    private int Line = 1;
    private List<GameObject> Obstacles = new List<GameObject>();
    
    // Start is called before the first frame update
    void Start()
    {
        var upBorder = m_Controller.Bounds.Up;
        while (Line *2f < upBorder + 2f)
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
        
        // ToDo уничтожение нижних объектов написать самостоятельно 
    }

    private void GenerateObstacle()
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

        }

        Vector3 pos = obstacle.transform.position;
        pos.y = Line * 2;

        obstacle.transform.position = pos;

        Line++;
        Obstacles.Add(obstacle); 
    }
}
