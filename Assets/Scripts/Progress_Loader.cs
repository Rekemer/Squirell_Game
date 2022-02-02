using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Progress_Loader : MonoBehaviour
{
    Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Update()
    {
        image.fillAmount = Loader.GetLoadingProgress();
    }
}
