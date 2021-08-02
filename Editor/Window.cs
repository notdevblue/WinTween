using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using WinTween.Position;
using WinTween.Scale;

public class Window : MonoBehaviour
{
    private void Awake()
    {
        gameObject.AddComponent<Position>();
        gameObject.AddComponent<WindowPosition>();


        gameObject.AddComponent<WindowScale>();
    }
}
