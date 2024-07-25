using Es.InkPainter;
using UnityEngine;
using UnityEngine.UI;

namespace ColorDrawer
{
  public class UIPainter : MonoBehaviour
    {
        private const string SavedTexturePath = "Save/SaveTexture";
        private const string DefaultTexturePath = "Texture/DefaultHeightMapMax";
        private const string MainTexture = "_MainTex";
        private static readonly int MainTex = Shader.PropertyToID(MainTexture);

        private InkCanvas _currentDrawingCanvas;
        [SerializeField] private Painter painter;
        [SerializeField] private Vector3 cubePosition;
        [SerializeField] private InkCanvas prefab;

        [Header("UI links")]
        [SerializeField] private ColorPicker colorPicker;
        [SerializeField] private Slider brushSizeSlider;
        [SerializeField] private Graphic previewGraphic;

        [Header("Buttons")]
        [SerializeField] private Button saveButton;
        [SerializeField] private Button loadButton;
        [SerializeField] private Button clearButton;

        public int saveJpegQuality = 100;
        public TextureFormat textureFormat = TextureFormat.ARGB32;

        private void Start()
        {
            colorPicker.OnColorChanged += ChangePaintColor;
            AddListeners();
            InitializeBrush();
            LoadDrawTexture(SavedTexturePath);
        }

        private void LoadDrawTexture(string path)
        {
            Texture2D texture = Resources.Load<Texture2D>(path);

            if (texture == null)
            {
                Debug.Log("Failed to load texture from Resources folder: " + path);
                texture = Resources.Load<Texture2D>(DefaultTexturePath);
            }

            SetRenderTexture(texture);

            if (_currentDrawingCanvas != null)
            {
                CreateNewCube();
                return;
            }

            CreateCube();
        }

        private void CreateCube()
        {
            _currentDrawingCanvas = Instantiate(prefab, cubePosition, Quaternion.identity);
        }

        private void SetRenderTexture(Texture2D texture)
        {
            Renderer rend = prefab.GetComponent<Renderer>();
            if (rend != null)
            {
                Material material = rend.sharedMaterial;
                if (material != null)
                {
                    material.SetTexture(MainTex, texture);
                }
            }
        }

        private void CreateNewCube()
        {
            var newCube = Instantiate(prefab, cubePosition, _currentDrawingCanvas.transform.rotation);
            Destroy(_currentDrawingCanvas.gameObject);
            _currentDrawingCanvas = newCube;
        }

        private void ChangePaintColor(Color c)
        {
            previewGraphic.color = c;
            painter.SetBrushColor(c);
        }

        private void SetBrushSize(float size)
        {
            var newSize = size / 10;
            painter.SetBrushSize(newSize);
        }

        private void AddListeners()
        {
            brushSizeSlider.onValueChanged.AddListener(SetBrushSize);
            saveButton.onClick.AddListener(SaveDrawing);
            loadButton.onClick.AddListener(LoadDrawing);
            clearButton.onClick.AddListener(ClearDrawing);
        }

        private void SaveDrawing()
        {
            RenderTexture renderTexture = _currentDrawingCanvas.GetMainTexture();
            if (renderTexture != null)
            {
                Texture2D texture = TextureUtils.ConvertRenderTextureToTexture2D(renderTexture,textureFormat);
                TextureUtils.SaveTextureToJPG(texture, saveJpegQuality);
            }
        }

        private void LoadDrawing() => LoadDrawTexture(SavedTexturePath);

        private void ClearDrawing() => LoadDrawTexture(DefaultTexturePath);

        private void InitializeBrush()
        {
            previewGraphic.color = colorPicker.color;
            SetBrushSize(brushSizeSlider.value);
        }

        private void OnDestroy()
        {
            if (colorPicker != null)
                colorPicker.OnColorChanged -= ChangePaintColor;
        }

        
    }
}