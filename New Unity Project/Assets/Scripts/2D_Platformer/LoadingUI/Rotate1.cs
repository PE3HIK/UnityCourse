using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rotate1 : MonoBehaviour
{
    [SerializeField] private GameObject fastArrow;
    [SerializeField] private GameObject slowArrow;

    private float z = -2f;
    void Update()
    {
        fastArrow.transform.Rotate(new Vector3(0, 0, z));
        slowArrow.transform.Rotate(new Vector3(0, 0, z/9));
    }
}