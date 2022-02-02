using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Launch;
public class Line_Controller : MonoBehaviour
{
   // Ilauncher launch;
    Vector2[] points;
    LineRenderer trajectory;
    [SerializeField] Transform start;

    [SerializeField] LayerMask obstacle;
    Mouse_Controller mouse;
    Nut_Controller nut;

    //
    Transform where;
    Transform to;
    float height;
    [SerializeField]
    public float speedOfHeightChanging;

    void Start()
    {
        trajectory = GetComponent<LineRenderer>();
        mouse = FindObjectOfType<Mouse_Controller>();
        nut = FindObjectOfType<Nut_Controller>();
      
    }

   
    private void Update()
    {
        to = nut.mouse.transform;
        where = nut.spawn;
        height = nut.height;

        if (nut.canAim)
        {
            DrawPath();
        }
        else trajectory.positionCount = 0;
        ChangingHeight();
        
        
    }
    
    void ChangingHeight()
    {
        if (mouse.mouseScroll.y != 0)
           nut.height += speedOfHeightChanging * mouse.mouseScroll.y;
        if (nut.height > nut.maxHeight)
        {
             nut.height= nut.maxHeight;
        }
        if(nut.height < nut.minHeight)
        {
            nut.height = nut.minHeight;
        }
       
    }
    private void Set(List<Vector2> points)
    {

        
        trajectory.positionCount = points.Count;
        this.points = points.ToArray();
        for(int i = 0; i < this.points.Length; i++)
        {
            trajectory.SetPosition(i, points[i]);
            
        }
    }
    public void DrawPath()
    {
        int resolution = 30;
        //Vector2[] points = new Vector2[resolution + 1];
        List<Vector2> points = new List<Vector2>();
        LaunchData launchData = Projectile_Proccesor.CalculateLaunchData(to, where, height);
        Vector3 previousDrawPoint = where.position;
        points.Add ( where.position);
        for (int i = 1; i <= resolution; i++)
        {
            float simulationTime = i / (float)resolution * launchData.timeToTarget; // время в промежутке от нуля до конечного времени
            Vector3 displacement = launchData.initialVelocity * simulationTime + Vector3.up * Projectile_Proccesor.gravity * simulationTime * simulationTime / 2f; // уравнение 3, чтобы расчитать расстояние между точками
            Vector3 drawPoint = new Vector3 (where.position.x + displacement.x, where.position.y + displacement.y, where.position.z + displacement.z);
            
            if (Physics2D.Linecast(previousDrawPoint, drawPoint, obstacle))
            {
                
                //points.Length = i;
                break;
            }
            points.Add(drawPoint);
            previousDrawPoint = drawPoint;
        }

        this.Set(points); 
    }


   
}
