using System.Collections.Generic;
using System.Drawing;
using System.IO;
using NoNameLib.TileEditor.Properties;
using OpenTK.Graphics.OpenGL;

namespace NoNameLib.TileEditor.Graphics
{
    public class TextureManager
    {
        public enum TextureErrors
        {
            TexturePathDoesNotExists,
            UnableToBindTexture,
            SystemTextureDoesNotExists,
            TextureAlreadyExists,
        }

        private static TextureManager instance;

        private readonly Dictionary<string, Texture> textureMap = new Dictionary<string, Texture>();

        private TextureManager()
        {
            LoadResourceTextures();
        }

        public static TextureManager Instance
        {
            get { return instance ?? (instance = new TextureManager()); }
        }

        /// <summary>
        /// Get <see cref="Texture"/> object.
        /// </summary>
        /// <param name="textureFile">Texture to retreive</param>
        /// <param name="texture">Texture out object</param>
        /// <returns>True if texture exists, otherwise false.</returns>
        public bool TryGetTexture(string textureFile, out Texture texture)
        {
            return textureMap.TryGetValue(textureFile, out texture);
        }

        /// <summary>
        /// Add new texture to TextureManager. 
        /// </summary>
        /// <param name="textureFilePath"></param>
        /// <exception cref="NoNameLibException">A NoNameLibException will be thrown when the same texture has already been added or when the texture can not be found.</exception>
        public string AddTexture(string textureFilePath)
        {
            var isSystemTexture = textureFilePath.StartsWith("sys_");

            Texture texture;
            if (isSystemTexture)
            {
                if (textureMap.ContainsKey(textureFilePath))
                {
                    throw new NoNameLibException(TextureErrors.TextureAlreadyExists, "A texture with name '{0}' has already been loaded.", textureFilePath);
                }

                texture = new Texture(textureFilePath);
            }
            else
            {
                if (!File.Exists(textureFilePath))
                {
                    throw new NoNameLibException(TextureErrors.TexturePathDoesNotExists, "Unable to find file: {0}", textureFilePath);
                }

                var filename = Path.GetFileName(textureFilePath);
                var path = Path.GetDirectoryName(textureFilePath);
                var key = Path.GetFileNameWithoutExtension(textureFilePath);
                
                if (filename == null)
                {
                    throw new NoNameLibException(TextureErrors.TexturePathDoesNotExists, "Unable to extract filename from texture path: {0}", textureFilePath);
                }

                if (textureMap.ContainsKey(textureFilePath))
                {
                    throw new NoNameLibException(TextureErrors.TextureAlreadyExists, "A texture with name '{0}' has already been loaded.", filename);
                }

                texture = new Texture(key, filename, path);
            }

            textureMap.Add(texture.Key, texture);

            return texture.Key;
        }

        /// <summary>
        /// Bind texture to OpenGL
        /// </summary>
        /// <param name="textureFile">Texture file to bind</param>
        internal void BindTexture(string textureFile)
        {
            Texture texture;

            if (textureMap.TryGetValue(textureFile, out texture))
            {
                GL.BindTexture(TextureTarget.Texture2D, texture.TextureId);
            }
            else
            {
                throw new NoNameLibException(TextureErrors.UnableToBindTexture, "Unable to bind texture with name: {0}. Does not exists in texture map, check if texture has been added.", textureFile);
            }
        }

        private void LoadResourceTextures()
        {
            AddTexture("sys_base");
            AddTexture("sys_m_1");
            AddTexture("sys_m_2");
            AddTexture("sys_m_3");
            AddTexture("sys_m_4");
            AddTexture("sys_m_5");
            AddTexture("sys_m_6");
            AddTexture("sys_m_7");
            AddTexture("sys_m_8");
            AddTexture("sys_m_9");
            AddTexture("sys_m_10");
            AddTexture("sys_m_11");
        }
    }

    public class Texture
    {
        public int TextureId { get; private set; }
        public string Directory { get; private set; }
        public string Filename { get; private set; }
        public string Key { get; private set; }

        /// <summary>
        /// Constructor for system textures
        /// </summary>
        /// <param name="textureKey"></param>
        internal Texture(string textureKey) : this(textureKey, "", "", true)
        {
        }

        /// <summary>
        /// Default Texture constructor
        /// </summary>
        /// <param name="textureKey"></param>
        /// <param name="textureFilename"></param>
        /// <param name="texturePath"></param>
        /// <param name="systemTexture"></param>
        internal Texture(string textureKey, string textureFilename, string texturePath, bool systemTexture = false)
        {
            Directory = texturePath;
            Filename = textureFilename;
            Key = textureKey;

            if (systemTexture)
            {
                var newTexture = LoadSystemTexture(textureKey);
                TextureId = TexUtil2D.CreateTextureFromBitmap((Bitmap)newTexture);
            }
            else
            {
                TextureId = TexUtil2D.CreateTextureFromFile(Path.Combine(Directory, Filename));
            }
        }

        private Image LoadSystemTexture(string textureName)
        {
            Image textureImage;

            if (textureName.Equals("sys_base"))
            {
                textureImage = Resources.sys_base;
            }
            else if (textureName.Equals("sys_m_1"))
            {
                textureImage = Resources.sys_m_1;
            }
            else if (textureName.Equals("sys_m_2"))
            {
                textureImage = Resources.sys_m_2;
            }
            else if (textureName.Equals("sys_m_3"))
            {
                textureImage = Resources.sys_m_3;
            }
            else if (textureName.Equals("sys_m_4"))
            {
                textureImage = Resources.sys_m_4;
            }
            else if (textureName.Equals("sys_m_5"))
            {
                textureImage = Resources.sys_m_5;
            }
            else if (textureName.Equals("sys_m_6"))
            {
                textureImage = Resources.sys_m_6;
            }
            else if (textureName.Equals("sys_m_7"))
            {
                textureImage = Resources.sys_m_7;
            }
            else if (textureName.Equals("sys_m_8"))
            {
                textureImage = Resources.sys_m_8;
            }
            else if (textureName.Equals("sys_m_9"))
            {
                textureImage = Resources.sys_m_9;
            }
            else if (textureName.Equals("sys_m_10"))
            {
                textureImage = Resources.sys_m_10;
            }
            else if (textureName.Equals("sys_m_11"))
            {
                textureImage = Resources.sys_m_11;
            }
            else
            {
                // TODO: Add system textures
                // sys_event
                // sys_select
    
                throw new NoNameLibException(TextureManager.TextureErrors.SystemTextureDoesNotExists, "System texture with name '{0}' has not been implemented.", textureName);
            }
            
            
            return textureImage;
        }
    }
}
