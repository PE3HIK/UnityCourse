using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FingerDriverTrack : MonoBehaviour
{
    public class TrackSegment
    {
        public Vector3[] Points;

        public bool IsPointSegment(Vector3 point)
        {
            return MathfTriangles.IsPointInTriangleXY(point, Points[0], Points[1], Points[2]);
        }
    }

    [SerializeField] private LineRenderer m_LineRenderer;
    [SerializeField] private bool m_ViewDebug; 

    private Vector3[] corners;
    private TrackSegment[] segments; 
     
    // Start is called before the first frame update
    void Start()
    {
        // запоминаем массив опорных точек
        corners = new Vector3[transform.childCount];
        for (int i = 0; i < corners.Length; i++)
        {
            GameObject obj = transform.GetChild(i).gameObject;
            corners[i] = obj.transform.position;
            obj.GetComponent<MeshRenderer>().enabled = false;
        }

        // настраиваем LineRenderer
        m_LineRenderer.positionCount = corners.Length;
        m_LineRenderer.SetPositions(corners);

        //Из полученного m_LineRenderer запекаем mesh
        Mesh mesh = new Mesh();
        m_LineRenderer.BakeMesh(mesh, true);

        // создаём массив сегментов трассы
        // каждый тругольник описан 3мя вершинами из массива вершин 

        segments = new TrackSegment[mesh.triangles.Length / 3];
        int segmentsCounter = 0;
        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            segments[segmentsCounter] = new TrackSegment();
            segments[segmentsCounter].Points = new Vector3[3];
            int nm = mesh.triangles[i];
            segments[segmentsCounter].Points[0] = mesh.vertices[nm];
            nm = mesh.triangles[i + 1];
            segments[segmentsCounter].Points[1] = mesh.vertices[nm];
            nm = mesh.triangles[i + 2];
            segments[segmentsCounter].Points[2] = mesh.vertices[nm];
            segmentsCounter++;
        }

        if (!m_ViewDebug)
        {
            return;
        }

        foreach (var segment in segments)
        {
            foreach (var point in segment.Points)
            {
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);

                sphere.transform.position = point;
                sphere.transform.localScale = Vector3.one * 0.1f;
            }
        }
    }
    public bool IsPointInTrack(Vector3 point)
    {
        foreach (var segment in segments)
        {
            if (segment.IsPointSegment(point))
            {
                return true;
            }
        }

        return false;
    }
}
