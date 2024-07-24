using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace ColorDrawer
{
    public static class TextureStatics
    {
        private const string DirectoryPath = "Assets/Resources/Save/";
        public static Texture2D ConvertRenderTextureToTexture2D(RenderTexture renderTexture,TextureFormat textureFormat)
        {
            Texture2D texture = new Texture2D(renderTexture.width, renderTexture.height, textureFormat, false);

            RenderTexture currentRT = RenderTexture.active;
            RenderTexture.active = renderTexture;
            texture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
            texture.Apply();
            RenderTexture.active = currentRT;
            return texture;
        }
        public static void SaveTextureToJPG(Texture2D texture, int quality, Action<TextureImporter> importAction = null)
        {
            string filePath = Path.Combine(DirectoryPath, "SaveTexture.jpg");

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            byte[] jpgData = texture.EncodeToJPG(quality);
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

        public static void SaveTextureToPNG(Texture2D texture, Action<TextureImporter> importAction = null)
        {
            string filePath = Path.Combine(DirectoryPath, "SaveTexture.png");

            if (!Directory.Exists(DirectoryPath))
            {
                Directory.CreateDirectory(DirectoryPath);
            }

            byte[] pngData = texture.EncodeToPNG();
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