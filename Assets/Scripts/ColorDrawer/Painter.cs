using Es.InkPainter;
using UnityEngine;

namespace ColorDrawer
{
    public class SamplePainter : MonoBehaviour
    {
        [SerializeField] private Brush brush;

        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                HandlePaint(Input.mousePosition);
            }

            if (Input.touchCount > 0)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase is TouchPhase.Began or TouchPhase.Moved or TouchPhase.Stationary)
                    {
                        HandlePaint(touch.position);
                    }
                }
            }
        }

        void HandlePaint(Vector2 inputPosition)
        {
            var ray = Camera.main.ScreenPointToRay(inputPosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                if (paintObject != null)
                {
                    paintObject.Paint(brush, hitInfo);
                }
            }
        }

        public void SetBrushColor(Color color) => brush.Color = color;

        public void SetBrushSize(float size) => brush.Scale = size;
    }
}