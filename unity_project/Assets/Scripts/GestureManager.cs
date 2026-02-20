using UnityEngine;
using System;
using System.Collections.Generic;

public class GestureManager : MonoBehaviour
{
    public static GestureManager Instance;

    private Vector2 startPos;
    private float swipeThreshold = 50f;

    public event Action OnTap;
    public event Action OnSwipeLeft;
    public event Action OnSwipeRight;
    public event Action OnSwipeUp;
    public event Action OnSwipeDown;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    void Update()
    {
        HandleTouchInput();
        HandleMouseInput();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    startPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    Vector2 endPos = touch.position;
                    Vector2 direction = endPos - startPos;

                    if (direction.magnitude < swipeThreshold)
                    {
                        OnTap?.Invoke();
                        Debug.Log("Tap Detected");
                    }
                    else
                    {
                        HandleSwipe(direction);
                    }
                    break;
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Vector2 endPos = Input.mousePosition;
            Vector2 direction = endPos - startPos;

            if (direction.magnitude < swipeThreshold)
            {
                OnTap?.Invoke();
                Debug.Log("Tap Detected");
            }
            else
            {
                HandleSwipe(direction);
            }
        }
    }

    void HandleSwipe(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if (direction.x > 0)
            {
                OnSwipeRight?.Invoke();
                Debug.Log("Swipe Right");
            }
            else
            {
                OnSwipeLeft?.Invoke();
                Debug.Log("Swipe Left");
            }
        }
        else
        {
            if (direction.y > 0)
            {
                OnSwipeUp?.Invoke();
                Debug.Log("Swipe Up");
            }
            else
            {
                OnSwipeDown?.Invoke();
                Debug.Log("Swipe Down");
            }
        }
    }
}
