using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject m_BaseCube;
    [SerializeField] private GameObject m_BaseCube2;
    [SerializeField] private GameObject m_BaseCube3;
    [SerializeField] private GameObject Text;

    [SerializeField] private Transform spawnPoint = null;
    [SerializeField] private Transform spawnPoint2 = null;
    [SerializeField] private Transform spawnPoint3 = null;
    [SerializeField] private Transform spawnPoint4 = null;

    [SerializeField] private Transform endPoint1 = null;

    private GameObject actionCube = null;
    
    GameObject myInstance;


    // Start is called before the first frame update
    void Start()
    {
        GameObject[] figures = { m_BaseCube, m_BaseCube2, m_BaseCube3 }; 

        actionCube = Instantiate(figures[Random.Range(0, figures.Length)], spawnPoint.position, Quaternion.identity);
        Instantiate(m_BaseCube2, spawnPoint2.position, Quaternion.identity);
        Instantiate(m_BaseCube3, spawnPoint3.position, Quaternion.Euler (0,0,90));
        myInstance = Instantiate(figures[Random.Range(0, figures.Length)], spawnPoint4.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (endPoint1.transform.position.y <= spawnPoint.transform.position.y)
        {
            if (actionCube.transform.position.y >= endPoint1.position.y)
            {
                actionCube.transform.Translate(-Vector3.up * 10 * Time.deltaTime);
            }

            if (actionCube.transform.position.y <= endPoint1.position.y)
            {
                actionCube = Instantiate(myInstance, spawnPoint.position, Quaternion.identity);
                Destroy(myInstance);
                endPoint1.transform.Translate(Vector3.up);
                GameObject[] figures = { m_BaseCube, m_BaseCube2, m_BaseCube3 };
                myInstance = Instantiate(figures[Random.Range(0, figures.Length)], spawnPoint4.position, Quaternion.identity);
            }
        }
        /*
        if (endPoint1.transform.position.y >= spawnPoint.transform.position.y)
        {
                  Instantiate(Text, spawnPoint5.position, Quaternion.identity);
        }
        */
        
        Text.gameObject.SetActive(endPoint1.transform.position.y >= spawnPoint.transform.position.y);


    }
}
