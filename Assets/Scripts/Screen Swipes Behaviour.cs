using UnityEngine;

public class ScreenSwipesBehaviour : MonoBehaviour
{
    private Vector2 fingerDownPosition;
    private Vector2 fingerUpPosition;

    [SerializeField]
    private bool detectSwipeOnlyAfterRelease = false;

    [SerializeField]
    private float minDistanceForSwipe = 20f;

    int _status = -1;

    void Update()
    {
        _status = -1;

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                fingerDownPosition = touch.position;
                fingerUpPosition = touch.position;
            }

            if (!detectSwipeOnlyAfterRelease && touch.phase == TouchPhase.Moved)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();
            }

            if (touch.phase == TouchPhase.Ended)
            {
                fingerUpPosition = touch.position;
                DetectSwipe();

                // Обработка тапа (касания без свайпа)
                if (Vector2.Distance(fingerDownPosition, fingerUpPosition) < minDistanceForSwipe)
                {
                    Debug.Log("Tap");
                    _status = 4;
                }
            }
        }
    }

    void DetectSwipe()
    {
        float deltaX = fingerUpPosition.x - fingerDownPosition.x;
        float deltaY = fingerUpPosition.y - fingerDownPosition.y;

        if (Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
        {
            // Горизонтальный свайп
            if (Mathf.Abs(deltaX) > minDistanceForSwipe)
            {
                if (deltaX > 0)
                {
                    // Свайп вправо
                    Debug.Log("Swipe Right");
                    _status = 0;
                }
                else
                {
                    // Свайп влево
                    Debug.Log("Swipe Left");
                    _status = 1;
                }
            }
        }
        else
        {
            // Вертикальный свайп
            if (Mathf.Abs(deltaY) > minDistanceForSwipe)
            {
                if (deltaY > 0)
                {
                    // Свайп вверх
                    Debug.Log("Swipe Up");
                    _status = 2;
                }
                else
                {
                    // Свайп вниз
                    Debug.Log("Swipe Down");
                    _status = 3;
                }
            }
        }
    }

    public int GetGlobalSwipesStatus()
    {
        return _status;
    }
}
