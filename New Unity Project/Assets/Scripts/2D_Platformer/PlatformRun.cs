using System;
using System.Collections;
using System.Collections.Generic;
using _2D_Platformer;
using UnityEngine;

public class PlatformRun : MonoBehaviour
{
    [SerializeField] private bool moved = true;
    [SerializeField] private float didtance = 6f;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float pauseTime = 1f;

    [SerializeField] private bool moveUP;

    private Vector3 startPodition;
    private Vector3 targetPosition; 
    
    // Start is called before the first frame update
    void Start()
    {
        if (moved)
        {
            startPodition = transform.position; 
            targetPosition = transform.position;

            if (moveUP)
            {
                targetPosition.y += didtance;
            }
            else
            {
                targetPosition.x += didtance;
            }
            StartCoroutine(MovementProcess());
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MovementProcess()
    {
        var k = 0f;
        var dir = 1f;
        
        while (true)
        {
            transform.position = Vector3.Lerp(startPodition, targetPosition, k);
            k += Time.deltaTime* dir * speed;

            if (k > 1f)
            {
                dir = -1;
                k = 1;
                yield return new WaitForSeconds(pauseTime);
            }
            if (k < 0)
            {
                dir = 1;
                k = 0;
                yield return new WaitForSeconds(pauseTime);
            }
            
            yield return null;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        bool isMovementObject = other.transform.GetComponent<CharacterMovement>();
        if (isMovementObject)
        {
            other.transform.parent = transform;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent == transform)
        {
            other.transform.parent = null; 
        }
    }
}
