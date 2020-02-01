using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insideTest : MonoBehaviour
{
    void Update()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        Rect finalRect = new Rect(rectTransform.position, rectTransform.sizeDelta);

        if (finalRect.Contains(Input.mousePosition, true))
        {
            Debug.Log("Inside");
        }
        else
        {            
           // Debug.Log(Input.mousePosition);
        }
        
    }
    private void onCollisionEnter()
    {
        Debug.Log("collsion");

    }
}
