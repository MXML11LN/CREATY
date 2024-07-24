using Es.InkPainter;
using UnityEngine;
using UnityEngine.UI;

namespace ColorDrawer
{
    public class UIPainter : MonoBehaviour
    {
        public InkCanvas canvas;
        public Vector3 cubePosition;
        public InkCanvas prefab;
        public SamplePainter samplePainter;
        public Slider brushSizeSlider;
        public Button saveButton;
        public Button loadButton;
        public Button clearButton;
        
        public Graphic previewGraphic;

        public ColorPicker colorPicker;
        private const string SavedTexturePath = "Save/SaveTexture";
        private const string DefaultTexturePath = "Texture/DefaultHeightMapMax";
        
       
        void Start()
        {
            previewGraphic.color = colorPicker.color;
            colorPicker.OnColorChanged += OnColorChanged;
            brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
            saveButton.onClick.AddListener(SaveDrawing);
            loadButton.onClick.AddListener(LoadDrawing);
            clearButton.onClick.AddListener(ClearDrawing);
            SetBrushSize(brushSizeSlider.value);
            
            LoadDrawTexture(SavedTexturePath);
        }

        private void LoadDrawTexture(string path)
        {
            Texture2D texture = Resources.Load<Texture2D>(path);
            if (texture == null)
            {
                Debug.Log("Failed to load texture from Resources folder: " + SavedTexturePath);
                texture = Resources.Load<Texture2D>(DefaultTexturePath);
            }
            Renderer rend =  prefab.GetComponent<Renderer>();
            if (rend != null)
            {
                Material material = rend.sharedMaterial;
                if (material != null)
                {
                    material.SetTexture("_MainTex", texture);
                }
            }
            if (canvas!= null)
            {
                
                var newCanvas= Instantiate(prefab,cubePosition,canvas.transform.rotation);
                Destroy(canvas.gameObject);
                canvas = newCanvas;
                return;
            }
            
            canvas = Instantiate(prefab,cubePosition,Quaternion.identity);
        }

        private void OnColorChanged(Color c)
        {
            previewGraphic.color = c;
            samplePainter.SetBrushColor(c);
        }

        private void SetBrushSize(float size)
        {
           var newSize = size / 10;
            samplePainter.SetBrushSize(newSize);
        }


        private void SaveDrawing()
        {
            RenderTexture renderTexture = canvas.GetMainTexture();
            if (renderTexture!=null)
            {
                 // SaveRenderTexture.SaveRenderTextureToPNG(renderTexture);
                 SaveRenderTexture.SaveRenderTextureToJPG(renderTexture, 100);
            }
        }

        private void LoadDrawing() => LoadDrawTexture(SavedTexturePath);

        private void ClearDrawing() => LoadDrawTexture(DefaultTexturePath);


        private void OnDestroy()
        {
            if (colorPicker != null)
                colorPicker.OnColorChanged -= OnColorChanged;
        }
    }
}