using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ColorDrawer
{
    public static class SaveRenderTexture
    {
        
        public static void SaveRenderTextureToJPG(RenderTexture renderTexture, int quality, Action<TextureImporter> importAction = null)
        {
            string directoryPath = "Assets/Resources/Save/";
            string filePath = Path.Combine(directoryPath, "SaveTexture.jpg");
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
            
            var newTex = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            newTex.Apply();

            byte[] jpgData = newTex.EncodeToJPG(quality);
            if (jpgData != null)
            {
                File.WriteAllBytes(filePath, jpgData);
                AssetDatabase.Refresh();

                var importer = AssetImporter.GetAtPath(filePath) as TextureImporter;
                if (importAction != null)
                {
                    importAction(importer);
                }

                Debug.Log("Saved texture to: " + filePath);
            }
        }
        
        public static void SaveRenderTextureToPNG(RenderTexture renderTexture, Action<TextureImporter> importAction = null)
        {
            string directoryPath = "Assets/Resources/Save/";
            string filePath = Path.Combine(directoryPath, "SaveTexture.png");
            
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            var newTex = new Texture2D(renderTexture.width, renderTexture.height);
            RenderTexture.active = renderTexture;
            newTex.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            newTex.Apply();

            byte[] pngData = newTex.EncodeToPNG();
            if (pngData != null)
            {
                File.WriteAllBytes(filePath, pngData);
                AssetDatabase.Refresh();
                var importer = AssetImporter.GetAtPath(filePath) as TextureImporter;
                if (importAction != null)
                {
                    importAction(importer);
                }
                Debug.Log("Saved texture to: " + filePath);
            }
        }
    }
}