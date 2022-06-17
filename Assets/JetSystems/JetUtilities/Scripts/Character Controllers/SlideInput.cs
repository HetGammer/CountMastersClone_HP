using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace JetSystems
{
    public class SlideInput : MonoBehaviour
    {
        [Header(" Settings ")]
        [SerializeField]
        private Vector2 slideCoefficient;

        private Vector3 slidePressedPos;
        private Vector3 slideReleasedPos;
        private float moveMagnitudeX;
        private float moveMagnitudeY;
        private bool pressed = false;

        [Header(" Events ")]
        public UnityEvent onMouseDown;
        public OnMouseDrag onMouseDrag;
        public UnityEvent onMouseUp;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            ManageSlideControl();
        }

        private void ManageSlideControl()
        {
            // On click down, save the 2 positions
            if (Input.GetMouseButtonDown(0) && !pressed)
            {
                pressed = true;

                slidePressedPos = GetCorrectedMousePosition();
                slideReleasedPos = GetCorrectedMousePosition();
                            
                onMouseDown?.Invoke();
            }
            else if (Input.GetMouseButton(0) && pressed)
            {
                slideReleasedPos = GetCorrectedMousePosition();
                
                moveMagnitudeX = slideReleasedPos.x - slidePressedPos.x;
                moveMagnitudeX *= slideCoefficient.x / (float)Screen.width;

                moveMagnitudeY = slideReleasedPos.y - slidePressedPos.y;
                moveMagnitudeY *= slideCoefficient.y / (float)Screen.height;

                onMouseDrag?.Invoke(new Vector2(moveMagnitudeX, moveMagnitudeY));
  
            }
            else if (Input.GetMouseButtonUp(0) && pressed)
            {
                moveMagnitudeX = 0;
                moveMagnitudeY = 0;
                pressed = false;

                onMouseUp?.Invoke();
            }

        }

        Vector3 GetCorrectedMousePosition()
        {
            Vector3 actualMousePos = new Vector3(Input.mousePosition.x - Screen.width / 2,
                Input.mousePosition.y - Screen.height / 2,
                Input.mousePosition.z);
            return actualMousePos;
        }
    }

    [System.Serializable]
    public class OnMouseDrag : UnityEvent<Vector2>
    {
        // DO NOT DELETE
    }
}