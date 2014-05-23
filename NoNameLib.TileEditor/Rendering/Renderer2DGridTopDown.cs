using NoNameLib.TileEditor.Collections;
using NoNameLib.TileEditor.Graphics;
using OpenTK.Graphics.OpenGL;


namespace NoNameLib.TileEditor.Rendering
{
    internal class Renderer2DGridTopDown : IRenderer
    {
        #region Fields

        private readonly TileEngine tileEngine;

        #endregion

        internal Renderer2DGridTopDown(TileEngine parentTileEngine)
        {
            this.tileEngine = parentTileEngine;
        }

        public void Initialize()
        {
            //InitializeShaders();

            TexUtil2D.InitTexturing();
        }

        public void SetViewportSize(int width, int height)
        {
            // Set viewport to window dimensions.
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();

            GL.Viewport(0, 0, width, height);
            GL.Ortho(0, width, height, 0, 0, 1);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
        }

        public void RenderScreen(ref TilePointTable tilePointTable)
        {
            TexUtil2D.InitTexturing();

            // Clear Screen And Depth Buffer
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 0.0f);

            GL.PushMatrix(); // It is important to push the Matrix before calling glRotatef and glTranslatef
            GL.Translate(0, 0, 0); // Translates the screen

            RenderBackground();
            if (tilePointTable.Count > 0)
            {
                RenderAll(ref tilePointTable);
            }

            GL.PopMatrix(); // Don't forget to pop the Matrix
            GL.Finish();
        }

        private void RenderBackground()
        {
            int mapTileSize = tileEngine.TileSize;

            //TilesV and H +1 for black border when show_grid = false
            int tilesH = tileEngine.MapWidth / mapTileSize + 1;
            int tilesV = tileEngine.MapHeight / mapTileSize + 1;

            TextureManager.Instance.BindTexture("sys_base");

            for (int x = 0; x < tilesH; x++)
            {
                for (int y = 0; y < tilesV; y++)
                {
                    // Calculate rendering top and left coordiates for this tile
                    float left;
                    float top;
                    if (tileEngine.ShowGrid)
                    {
                        left = (x * mapTileSize) + ((x > 0) ? (x * 1) : 0);
                        top = (y * mapTileSize) + ((y > 0) ? (y * 1) : 0);
                    }
                    else
                    {
                        left = (x * mapTileSize);
                        top = (y * mapTileSize);
                    }

                    RenderTile(left, top);
                }
            }
        }

        private void RenderAll(ref TilePointTable mapData)
        {
            TextureManager.Instance.BindTexture("Pokemon Universe Tileset 2");

            int mapTileSize = tileEngine.TileSize;

            //TilesV and H +1 for black border when show_grid = false
            int tilesH = tileEngine.MapWidth / mapTileSize + 1;
            int tilesV = tileEngine.MapHeight / mapTileSize + 1;

            for (int x = 0; x < tilesH; x++)
            {
                int tileX = tileEngine.MapPosition.X + x;
                for (int y = 0; y < tilesV; y++)
                {
                    int tileY = tileEngine.MapPosition.Y + y;

                    // Calculate rendering top and left coordiates for this tile
                    float left;
                    float top;
                    if (tileEngine.ShowGrid)
                    {
                        left = (x * mapTileSize) + ((x > 0) ? (x * 1) : 0);
                        top = (y * mapTileSize) + ((y > 0) ? (y * 1) : 0);
                    }
                    else
                    {
                        left = (x * mapTileSize);
                        top = (y * mapTileSize);
                    }

                    bool hasRenderedAnything = false;

                    TilePoint tp = mapData.GetTilePoint(tileX, tileY, false);
                    if (tp != null && tp.Layers.Count > 0)
                    {
                        // Render layers
                        foreach (var layer in tp.Layers.Values)
                        {
                            if (!tileEngine.VisibleTileLayers[layer.Z])
                                continue;

                            // Render tile layers
                            if (layer.TileLayers.Count > 0)
                            {
                                foreach (var tileLayer in layer.TileLayers.Values)
                                {
                                    if (!tileEngine.VisibleTilePointLayers[tileLayer.Layer])
                                        continue;
                                    
                                    if (tileLayer.TileId.Equals("") || tileLayer.TileId.Equals("0"))
                                        continue;

                                    RenderSprite(tileLayer.Image, (int)left, (int)top);

                                    //TextureManager.Instance.BindTexture(tileLayer.TileId);
                                    //RenderTile(left, top);

                                    hasRenderedAnything = true;
                                }

                                /*if (tileEngine.ShowBlocking)
                                {
                                    TextureManager.Instance.BindTexture("sys_m_" + layer.Movement);
                                    RenderTile(left, top);
                                }
                                else if (tileEngine.ShowEvents && layer.Event != null)
                                {
                                    TextureManager.Instance.BindTexture("sys_event");
                                    RenderTile(left, top);
                                }*/
                            }

                            // Something has been rendered, break the TilePointLayer loop
                            if (hasRenderedAnything)
                                break;
                        }
                    }

                    // Render selection
                    /*if (tileEngine.SelectedTiles.ContainsKey(TilePointTable.GenerateKey(x, y)))
                    {
                        TextureManager.Instance.BindTexture("sys_select");
                        RenderTile(left, top);
                    }*/
                }
            }
        }

        private void RenderTile(float left, float top)
        {
            float width = left + tileEngine.TileSize;
            float height = top + tileEngine.TileSize;

            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(0, 0); GL.Vertex2(left, top);
            GL.TexCoord2(1, 0); GL.Vertex2(width, top);
            GL.TexCoord2(1, 1); GL.Vertex2(width, height);
            GL.TexCoord2(0, 1); GL.Vertex2(left, height);
            GL.End();
        }

        private void RenderSprite(SpriteImage image, int x, int y)
        {
            float width = x + tileEngine.TileSize;
            float height = y + tileEngine.TileSize;

            GL.Begin(BeginMode.Quads);
            GL.TexCoord2(image.TextureCoords.X / (double)image.TextureWidth, image.TextureCoords.Y / (double)image.TextureHeight); 
            GL.Vertex2(x, y);
            GL.TexCoord2((image.TextureCoords.X + tileEngine.TileSize) / (double)image.TextureWidth, image.TextureCoords.Y / (double)image.TextureHeight); 
            GL.Vertex2(width, y);
            GL.TexCoord2((image.TextureCoords.X + tileEngine.TileSize) / (double)image.TextureWidth, (image.TextureCoords.Y + tileEngine.TileSize) / (double)image.TextureHeight);
            GL.Vertex2(width, height);
            GL.TexCoord2(image.TextureCoords.X / (double)image.TextureWidth, (image.TextureCoords.Y + tileEngine.TileSize) / (double)image.TextureHeight); 
            GL.Vertex2(x, height);
            GL.End();
        }

        /*void RenderSprite(GLuint spritesheet, unsigned spritex, unsigned spritey, unsigned texturew, unsigned textureh, int x, int y, int w, int h)
        {
            glColor4ub(255, 255, 255, 255);
            glBindTexture(GL_TEXTURE_2D, spritesheet);
            glBegin(GL_QUADS);
            glTexCoord2d(spritex / (double)texturew, spritey / (double)textureh);
            glVertex2f(x, y);
            glTexCoord2d((spritex + w) / (double)texturew, spritey / (double)textureh);
            glVertex2f(x + w, y);
            glTexCoord2d((spritex + w) / (double)texturew, (spritey + h) / (double)textureh);
            glVertex2f(x + w, y + h);
            glTexCoord2d(spritex / (double)texturew, (spritey + h) / (double)textureh);
            glVertex2f(x, y + h);
            glEnd();
        }*/
    }
}
