using System;
using System.Collections.Concurrent;
using System.Drawing;
using NoNameLib.TileEditor.Collections;
using NoNameLib.TileEditor.Graphics;
using NoNameLib.TileEditor.Rendering;

namespace NoNameLib.TileEditor
{
    public class TileEngine
    {
        #region Public Properties 

        /// <summary>
        /// Gets or sets the property to show grid lines
        /// </summary>
        public bool ShowGrid { get; set; }

        /// <summary>
        /// Gets or sets the tile size
        /// </summary>
        public int TileSize { get; set; }

        /// <summary>
        /// Gets the map height in pixels
        /// </summary>
        public int MapHeight { get; private set; }

        /// <summary>
        /// Gets the map width in pixels
        /// </summary>
        public int MapWidth { get; private set; }

        /// <summary>
        /// Gets the current map scroll position. Point is based from top/left corner
        /// </summary>
        public Point MapPosition { get; set; }

        /// <summary>
        /// Array of visibility for each tilepoint layer (0-2)
        /// </summary>
        public bool[] VisibleTilePointLayers { get; private set; }

        /// <summary>
        /// Array of visibility for each tile layer (0-9)
        /// </summary>
        public bool[] VisibleTileLayers { get; private set; }

        /// <summary>
        /// Get or sets the property to render TilePoint blocking values
        /// </summary>
        public bool ShowBlocking { get; set; }

        /// <summary>
        /// Gets or sets the property to render events on TilePoints
        /// </summary>
        public bool ShowEvents { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public ConcurrentDictionary<Int64, bool> SelectedTiles { get; set; }

        #endregion

        private readonly IRenderer renderingEngine;

        /// <summary>
        /// TileEngine Ctor
        /// </summary>
        public TileEngine()
        {
            ShowGrid = true;
            TileSize = 32;
            MapHeight = 256;
            MapWidth = 512;
            MapPosition = new Point(0, 0);

            VisibleTilePointLayers = new [] {true, true, true};
            VisibleTileLayers = new[] {true, true, true, true, true, true, true, true, true, true};

            ShowBlocking = false;
            ShowEvents = false;

            SelectedTiles = new ConcurrentDictionary<long, bool>();
            
            // TODO: Make this generic somekind of enum?
            renderingEngine = new Renderer2DGridTopDown(this);
        }

        public void Initialize()
        {
            renderingEngine.Initialize();
        }

        /// <summary>
        /// Set the map height and width
        /// </summary>
        /// <param name="height"></param>
        /// <param name="width"></param>
        public void SetMapSize(int height, int width)
        {
            MapHeight = height;
            MapWidth = width;

            renderingEngine.SetViewportSize(MapWidth, MapHeight);
        }

        /// <summary>
        /// Sets the size of the rendered tiles
        /// </summary>
        /// <param name="tilesize"></param>
        public void SetTileSize(int tilesize)
        {
            TileSize = tilesize;
        }

        /// <summary>
        /// Move the map a number of tiles
        /// </summary>
        /// <param name="point">Point to which side to translate the map</param>
        public void TranslateMap(Point point)
        {
            MapPosition = new Point()
                {
                    X = MapPosition.X + point.X,
                    Y = MapPosition.Y + point.Y
                };
        }

        /// <summary>
        /// Add a new texture to <see cref="TextureManager"/>
        /// </summary>
        /// <param name="textureFilePath">Absolute path to file on disk</param>
        public string AddTexture(string textureFilePath)
        {
            return TextureManager.Instance.AddTexture(textureFilePath);
        }

        /// <summary>
        /// Render passed TilePointTable to screen
        /// </summary>
        /// <param name="tilePointTable">TilePointTable to render. Passed as reference</param>
        public void DoRender(ref TilePointTable tilePointTable)
        {
            renderingEngine.RenderScreen(ref tilePointTable);
        }

        /// <summary>
        /// Calculate the tilepoint X and Y coordinates from screen pixels and current map position
        /// </summary>
        /// <param name="screenX"></param>
        /// <param name="screenY"></param>
        /// <returns></returns>
        public TilePoint GetTilePointCoordinatesFromScreen(int screenX, int screenY)
        {
            int gridOffset = (ShowGrid) ? 1 : 0;

            int tileX = screenX / (TileSize + gridOffset);
            int tileY = screenY / (TileSize + gridOffset);

            var point = new TilePoint
            {
                X = MapPosition.X + tileX,
                Y = MapPosition.Y + tileY
            };
            return point;
        }
    }
}
