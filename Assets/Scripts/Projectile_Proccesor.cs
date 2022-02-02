using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Launch;
public static class Projectile_Proccesor 
{
    //public Projectile_Proccesor(float height) => h = height;
    
	//static float h = 10;
	public static float gravity = -10;
    
    public static void Launch(IThrowable obj, Transform where, Transform from, float height) 
    {
        Physics.gravity = Vector3.up * gravity;
        //obj.rd.isKinematic = false;
       // obj.rd.velocity = new Vector2(0, 0);
        obj.rd.velocity = CalculateLaunchData(where, from, height).initialVelocity;    
    }
   public static LaunchData CalculateLaunchData(Transform where, Transform from, float h) // ���������� ������ �������� ������������ ������ ��� 
    {
        float displacementY = where.position.y - from.position.y; // ���������� ����� ��������� �� ��� Y
        displacementY = Mathf.Clamp(displacementY, 0, h);
        Vector3 displacementXZ = new Vector3(where.position.x - from.position.x, 0, 0);// ���������� ����� ��������� �� ��� x � z
       //displacementXZ = Clamp(ref displacementXZ);
        float time = Mathf.Sqrt(-2 * h / gravity) + Mathf.Sqrt(2 * (displacementY - h) / gravity); // ����� ������   t_up + t_down 
        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * h); // ��������� ��������, ���������� �� ��������� 5 
        Vector3 velocityXZ = displacementXZ / time; // �������� ������������ ��� x � z
        //Debug.Log(velocityXZ + velocityY * -Mathf.Sign(gravity));
       
        return new LaunchData(velocityXZ + velocityY * -Mathf.Sign(gravity), time);
    }

   


};
