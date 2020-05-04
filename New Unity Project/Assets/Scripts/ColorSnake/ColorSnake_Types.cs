using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = System.Object;
using Random = UnityEngine.Random;

public class ColorSnake_Types : MonoBehaviour
{
// цвета объектов

    [Serializable] public class ColorType
    {
        public string Name;
        public int Id;
        public Color color;
    }
    
    [Serializable] public class  ObjectType
    {
        public string Name;
        public int Id;
        public GameObject Object;
    }
    
    [Serializable] public class  TemplateType
    {
        public string Name;
        public int Id;
        public Transform[] points;
    }

    [SerializeField] private ColorType[] m_Colors; 
    [SerializeField] private ObjectType[] m_Objects; 
    [SerializeField] private TemplateType[] m_Template;
    

    public ColorType GetRandomColorType()
    {
        int rand = Random.Range(0, m_Colors.Length);
        return m_Colors[rand]; 
    }

    public ObjectType GetRandomObjectType()
    {
        int rand = Random.Range(0, m_Objects.Length);
        return m_Objects[rand]; 
    }
    
    public TemplateType GetRandomOTemplateType()
    {
        int rand = Random.Range(0, m_Template.Length);
        return m_Template[rand]; 
    }

    public ColorType GetColrType(int id)
    {
        return m_Colors.FirstOrDefault(c => c.Id == id);
    }

    public ObjectType GetObjectType(int id)
    {
        return m_Objects.FirstOrDefault(c => c.Id == id); 
    }
}
