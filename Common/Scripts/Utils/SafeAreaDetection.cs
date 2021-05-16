using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAreaDetection : MonoBehaviour
{
    public delegate void SafeAreachanged(Rect safeArea);
    public static SafeAreachanged OnSafeAreaChanged;

    private Rect safeArea;

    private void Awake()
    {
        safeArea = Screen.safeArea;
    }


    private void Update()
    {
        if (safeArea != Screen.safeArea)
        {
            safeArea = Screen.safeArea;
            OnSafeAreaChanged?.Invoke(safeArea);
        }
    }

}
