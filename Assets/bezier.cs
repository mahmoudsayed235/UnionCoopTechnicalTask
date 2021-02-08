using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bezier : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform point1, point2,point3,point4;
    int numberOfPoints = 100;
    private Vector3[] points = new Vector3[100];
    public void Draw()
    {
        lineRenderer.positionCount = numberOfPoints;
        DrawLine();
    }
    public void Draw4()
    {
        lineRenderer.positionCount = numberOfPoints;
        DrawCubicLine();
    }

    // Update is called once per frame
    void DrawLine()
    {
        for(int i = 1; i < numberOfPoints+1; i++)
        {
            float point = i / (float)numberOfPoints;
            points[i - 1] = CalculatePoint2PointCurve(point,point1.position,point2.position);
        }
        lineRenderer.SetPositions(points);
    }
    Vector3 CalculatePoint2PointCurve(float point, Vector3 point1, Vector3 point2)
    {
        return point1 + point * (point2 - point1);
    }
    void DrawCubicLine()
    {
        for (int i = 1; i < numberOfPoints + 1; i++)
        {
            float point = i / (float)numberOfPoints;
            points[i - 1] = CalculatePoint4PointCurve(point, point1.position, point2.position, point3.position, point4.position);
        }
        lineRenderer.SetPositions(points);
    }
    Vector3 CalculatePoint4PointCurve(float point, Vector3 point1, Vector3 point2,Vector3 point3,Vector3 point4)
    {
        float u = 1 - point;
        float pointSquare = point * point;
        float uSquare = u * u;
        float uCube = uSquare * u;
        float pointCube = pointSquare * point;
        Vector3 p = uCube * point1;
        p += 3 * uSquare * point * point2;
        p += 3 * u * pointSquare * point3;
        p += pointCube * point4;
        return p;


        return point1 + point * (point2 - point1);
    }
}
