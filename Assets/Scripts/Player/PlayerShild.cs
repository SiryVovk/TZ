using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class PlayerShild : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool pointerDown;
    private float pointerTimePresed;
    private float pointerMaxTime = 2f;

    public UnityEvent<bool> onButtonDown;
    public UnityEvent<bool> onButtonUp;

    public void OnPointerDown(PointerEventData eventData)
    {
        pointerDown = true;
        onButtonDown?.Invoke(true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Reset();
        onButtonUp?.Invoke(false);
    }

    private void Update()
    {
        if(pointerDown)
        {
            pointerTimePresed += Time.deltaTime;
            if(pointerTimePresed >= pointerMaxTime)
            {
                onButtonUp?.Invoke(false);
            }
        }
    }

    private void Reset()
    {
        pointerDown = false;
        pointerTimePresed = 0;
    }
}
