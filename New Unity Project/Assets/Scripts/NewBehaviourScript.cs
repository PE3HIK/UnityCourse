using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject m_BaseCube;
    [SerializeField] private GameObject m_BaseCube2;
    [SerializeField] private GameObject m_BaseCube3;

    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform spawnPoint2 = null;
    [SerializeField] private Transform spawnPoint3 = null;


    [SerializeField] private Transform endPoint = null;

    private GameObject actionCube = null; 

    // Start is called before the first frame update
    void Start()
    {
        actionCube = Instantiate(m_BaseCube, spawnPoint.position, Quaternion.identity);
        Instantiate(m_BaseCube2, spawnPoint2.position, Quaternion.identity);
        Instantiate(m_BaseCube3, spawnPoint3.position, Quaternion.Euler (0,0,180));
    }

    // Update is called once per frame
    void Update()
    {
        if (actionCube.transform.position.y >= endPoint.position.y)
        {
            actionCube.transform.Translate(-Vector3.up *2* Time.deltaTime);
        } 

    }
}
