using UnityEngine;
using UnityEngine.UI;

namespace ColorDrawer
{
    public class ColorPreview : MonoBehaviour
    {
        public Graphic previewGraphic;

        public ColorPicker colorPicker;

        private void Start()
        {
            previewGraphic.color = colorPicker.color;
            colorPicker.OnColorChanged += OnColorChanged;
        }

        public void OnColorChanged(Color c)
        {
            previewGraphic.color = c;
        }

        private void OnDestroy()
        {
            if (colorPicker != null)
                colorPicker.OnColorChanged -= OnColorChanged;
        }
    }
}