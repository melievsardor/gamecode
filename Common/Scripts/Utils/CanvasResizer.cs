using UnityEngine;
using System.Collections;

public class CanvasResizer : MonoBehaviour
{

    const int HEIGHT = 1080;
    // Use this for initialization

    void Start()
    {
        RectTransform t = gameObject.GetComponent<RectTransform>();
        t.SetWidth(HEIGHT * Screen.width / Screen.height);
    }


}