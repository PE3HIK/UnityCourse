using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HopeTrackFade : MonoBehaviour
{
    [SerializeField] private  GameObject m_PlatformFade;
    [SerializeField] private GameObject m_PlatformUnFade;
    
    
    public virtual void EffectPlayer(HopPlayer player)
         {
             player.m_BallSpeed = 1f;
             player.m_JumpDistance = 2f;
         }


    public void m_Fade()
    {
        m_PlatformFade.SetActive(false);
        m_PlatformUnFade.SetActive(true);
    }
}