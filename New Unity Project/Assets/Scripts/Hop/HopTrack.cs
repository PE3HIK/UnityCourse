using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class HopTrack : MonoBehaviour
{
    [SerializeField] private GameObject m_Platform;
    [SerializeField] private GameObject m_Platform1;

    private List<GameObject> platforms = new List<GameObject>();
    private List<GameObject> platforms1 = new List<GameObject>();

    
    // Start is called before the first frame update
    void Start()
    {
        // генерация платформ
        platforms.Add(m_Platform);
        platforms1.Add(m_Platform1);

        for (int i = 0; i < 25; i++)
        {
            GameObject obj = Instantiate(m_Platform, transform);
            GameObject obj1 = Instantiate(m_Platform1);

            Vector3 pos = Vector3.zero;
            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);

            obj.transform.position = pos;
            obj1.transform.position = pos;

            obj.name = $"Platform {i}";
            obj1.name = $"Platform {i}";
            platforms.Add(obj);
            platforms1.Add(obj1);
        }
    }

    public bool ISBallOnPlatform(Vector3 position)
    {
        position.y = 0f;

        GameObject nearestPlatform = platforms [0];

        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].transform.position.z + 0.5f < position.z)
            {
                continue;
            }
            if (platforms[i].transform.position.z -  position.z > 1f )
            {
                continue;
            }
            
            nearestPlatform = platforms[i];
            platforms[i].SetActive(false);
            platforms1[i].SetActive(true);
            break;
        }
        
        float minX = nearestPlatform.transform.position.x - 0.5f;
        float maxX = nearestPlatform.transform.position.x + 0.5f;

        return position.x > minX && position.x < maxX;
    }
   
}
