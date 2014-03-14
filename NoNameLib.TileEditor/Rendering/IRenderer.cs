using NoNameLib.TileEditor.Collections;

namespace NoNameLib.TileEditor.Rendering
{
    internal interface IRenderer
    {
        void Initialize();

        void SetViewportSize(int width, int height);

        void RenderScreen(ref TilePointTable tilePointTable);
    }
}
