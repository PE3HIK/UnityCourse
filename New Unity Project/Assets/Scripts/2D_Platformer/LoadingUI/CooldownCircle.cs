using UnityEngine;
using System.Collections;
using _2D_Platformer;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CooldownCircle : MonoBehaviour
{
    public Image cooldown;
    public bool coolingDown = true;
    public SceneLoader Loader; 

    // Update is called once per frame
    void Update()
    {
        if (coolingDown == true)
        {
            //Reduce fill amount over 30 seconds
            cooldown.fillAmount += 1.0f / Loader.loadTime * 0.9f * Time.deltaTime;
            
        }
        
        
    }
}