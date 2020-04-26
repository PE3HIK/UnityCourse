using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Hop;
using UnityEngine;
using UnityEngine.Serialization;

public class HopTrack : MonoBehaviour
{

    public List<HopeTrackFade> PlatformsPrefabs; 


    public List<HopeTrackFade> platforms = new List<HopeTrackFade>();

    
    // Start is called before the first frame update
    void Start()
    {
        // генерация платформ
        platforms.Add(PlatformsPrefabs[0]);

        for (int i = 0; i < 25; i++)
        {
            var randomNumber = Random.Range(0, PlatformsPrefabs.Count); 
            var obj = Instantiate(PlatformsPrefabs[randomNumber], transform);

            var pos = Vector3.zero;
            pos.z = 2 * (i + 1);
            pos.x = Random.Range(-1, 2);

            obj.transform.position = pos;

            obj.name = $"Platform {i}";
            platforms.Add(obj);
        }
    }

    public bool ISBallOnPlatform(HopPlayer player)
    {
        var position = player.transform.position;
        position.y = 0f;

        var nearestPlatform = platforms [0];

        var onPlatform = false;

        for (int i = 0; i < platforms.Count; i++)
        {
            if (platforms[i].transform.position.z + 0.5f < position.z )
            {
                continue;
            }
            if (platforms[i].transform.position.z -  position.z > 1f )
            {
                continue;
            }
            
            nearestPlatform = platforms[i];
            
            float minX = nearestPlatform.transform.position.x - 0.5f;
            float maxX = nearestPlatform.transform.position.x + 0.5f;
        
            onPlatform = position.x > minX && position.x < maxX;

            if (onPlatform)
            {
                platforms[i].m_Fade();
                platforms[i].EffectPlayer(player);
            }

            break;
        }
        return onPlatform; 
    }
   
}
