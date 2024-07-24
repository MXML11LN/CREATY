using Es.InkPainter;
using UnityEngine;

namespace ColorDrawer
{
    public class SamplePainter : MonoBehaviour
    {
        [SerializeField]
        private Brush brush;

        private void Update()
        {
            if(Input.GetMouseButton(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hitInfo;
                if(Physics.Raycast(ray, out hitInfo))
                {
                    var paintObject = hitInfo.transform.GetComponent<InkCanvas>();
                    if(paintObject != null)
                        paintObject.Paint(brush, hitInfo);
                }
            }
        }

        public void SetBrushColor(Color color) => brush.Color = color;

        public void SetBrushSize(float size) => brush.Scale = size;
    }

}