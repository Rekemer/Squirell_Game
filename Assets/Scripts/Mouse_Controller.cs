using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse_Controller : MonoBehaviour
{
    [HideInInspector]
    public Camera mainCamera;
    private Nut_Controller nut_ctr;
    [SerializeField] float howFarCanAim;
    public Vector2 mouseScroll;
    void Start(){
        nut_ctr = FindObjectOfType<Nut_Controller>();
        mainCamera = FindObjectOfType<Camera>();
    }

    void Update()
    {
        
        Vector3 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = mousePos.z - mainCamera.transform.position.z;
        gameObject.transform.position = mousePos;
        if (Mathf.Abs(gameObject.transform.localPosition.x) > howFarCanAim)
        {
            gameObject.transform.localPosition = new Vector3(howFarCanAim * Mathf.Sign(gameObject.transform.localPosition.x), gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        }
        mouseScroll = Input.mouseScrollDelta;
        

        //Debug.Log("Позиция мыши на экране " + gameObject.transform.position);
    }
}
