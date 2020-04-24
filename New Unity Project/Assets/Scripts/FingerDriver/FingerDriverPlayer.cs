using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FingerDriverPlayer : MonoBehaviour
{
    [SerializeField] private FingerDriverTrack m_Track;
    [SerializeField] private FingerDriverInput m_Input;
    
    //точка по которой будет проверятся нохождение на трассе, вынесена в нос автомобился
    [SerializeField] private Transform m_trackPoint;
    [SerializeField] private float m_CarSpeed = 2f;
    [SerializeField] private float m_MaxSteer = 90f;
    
    [SerializeField] private Text Text;

    private List<Collider> checkpoints = new List<Collider>(); 
    
    private void Update()
    {
        if (m_Track.IsPointInTrack(m_trackPoint.position))
        {
            transform.Translate(transform.up * Time.deltaTime * m_CarSpeed, Space.World);
            transform.Rotate(0f, 0f, m_MaxSteer * m_Input.SteerAxis*Time.deltaTime);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("checkpoint") && !checkpoints.Contains(other))
        {
            checkpoints.Add(other);
            Text.text = $"{checkpoints.Count-1}"; 
            Debug.Log(checkpoints.Count - 1);
        }
    }
}