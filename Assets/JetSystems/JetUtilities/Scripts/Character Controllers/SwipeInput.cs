using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


namespace JetSystems
{
    public class SwipeInput : MonoBehaviour
    {
        enum SwipeDirection { Right, Left, Up, Down }

        [Header(" Logic ")]
        private bool pressed;

        [Header(" Control ")]
        [SerializeField]
        private float moveThreshold;
        private Vector2 pressedMousePosition, currentMousePosition;

        [Header(" Callbacks ")]
        [SerializeField]
        private UnityEvent onSwipeRight;

        [SerializeField]
        private UnityEvent onSwipeDown;

        [SerializeField]
        private UnityEvent onSwipeLeft;

        [SerializeField]
        private UnityEvent onSwipeUp;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ManageInput();
        }

        #region Movement

        private void ManageInput()
        {
            if (Input.GetMouseButtonDown(0))
            {
                pressedMousePosition = Input.mousePosition;
                pressed = true;
            }
            else if (Input.GetMouseButtonUp(0) && pressed)
            {
                currentMousePosition = Input.mousePosition;

                Vector2 difference = currentMousePosition - pressedMousePosition;


                difference.x /= Screen.width;
                difference.y /= Screen.height;

                if (Mathf.Abs(difference.x) > Mathf.Abs(difference.y))
                {
                    // Moving Right left
                    if (Mathf.Abs(difference.x) < moveThreshold / Screen.width) return;

                    if (difference.x < 0)
                        TriggerSwipe(SwipeDirection.Left);
                    else
                        TriggerSwipe(SwipeDirection.Right);
                }
                else
                {
                    // Moving front back
                    if (Mathf.Abs(difference.y) < moveThreshold / Screen.height) return;

                    if (difference.y < 0)
                        TriggerSwipe(SwipeDirection.Down);
                    else
                        TriggerSwipe(SwipeDirection.Up);
                }



                pressed = false;
            }
        }

        private void TriggerSwipe(SwipeDirection swipeDirection)
        {
            switch (swipeDirection)
            {
                case SwipeDirection.Right:
                    onSwipeRight?.Invoke();
                    break;

                case SwipeDirection.Left:
                    onSwipeLeft?.Invoke();
                    break;

                case SwipeDirection.Up:
                    onSwipeUp?.Invoke();
                    break;

                case SwipeDirection.Down:
                    onSwipeDown?.Invoke();
                    break;

                default:
                    break;
            }
        }

        #endregion
    }
}