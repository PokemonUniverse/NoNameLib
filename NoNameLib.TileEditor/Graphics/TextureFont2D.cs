﻿using Img = System.Drawing.Imaging;

using OpenTK.Graphics.OpenGL;

namespace NoNameLib.TileEditor.Graphics
{
    public class TextureFont2D
    {
        /// <summary>
        /// Create a TextureFont object. The sent-in textureId should refer to a
        /// texture bitmap containing a 16x16 grid of fixed-width characters,
        /// representing the ASCII table. A 32 bit texture is assumed, aswell as
        /// all GL state necessary to turn on texturing. The dimension of the
        /// texture bitmap may be anything from 128x128 to 512x256 or any other
        /// order-by-two-squared-dimensions.
        /// </summary>
        public TextureFont2D(int textureId)
        {
            this.textureId = textureId;
        }

        /// <summary>
        /// Draw an ASCII string around coordinate (0,0,0) in the XY-plane of the
        /// model space coordinate system. The height of the text is 1.0.
        /// The width may be computed by calling ComputeWidth(string).
        /// This call modifies the currently bound
        /// 2D-texture, but no other GL state.
        /// </summary>
        public void WriteString(string text)
        {
            GL.BindTexture(TextureTarget.Texture2D, textureId);
            GL.PushMatrix();
            double width = ComputeWidth(text);
            GL.Translate(-width / 2.0, -0.5, 0);
            GL.Begin(BeginMode.Quads);
            double xpos = 0;
            foreach (var ch in text)
            {
                WriteCharacter(ch, xpos);
                xpos += AdvanceWidth;
            }
            GL.End();
            GL.PopMatrix();
        }

        /// <summary>
        /// Determines the distance from character center to adjacent character center, horizontally, in
        /// one written text string. Model space coordinates.
        /// </summary>
        public double AdvanceWidth = 0.75;

        /// <summary>
        /// Determines the width of the cut-out to do for each character when rendering. This is necessary
        /// to avoid artefacts stemming from filtering (zooming/rotating). Make sure your font contains some
        /// "white space" around each character so they won't be clipped due to this!
        /// </summary>
        public double CharacterBoundingBoxWidth = 0.8;

        /// <summary>
        /// Determines the height of the cut-out to do for each character when rendering. This is necessary
        /// to avoid artefacts stemming from filtering (zooming/rotating). Make sure your font contains some
        /// "white space" around each character so they won't be clipped due to this!
        /// </summary>
        public double CharacterBoundingBoxHeight = 0.8;//{ get { return 1.0 - borderY * 2; } set { borderY = (1.0 - value) / 2.0; } }

        /// <summary>
        /// Computes the expected width of text string given. The height is always 1.0.
        /// Model space coordinates.
        /// </summary>
        public double ComputeWidth(string text)
        {
            return text.Length * AdvanceWidth;
        }

        /// <summary>
        /// This is a convenience function to write a text string using a simple coordinate system defined to be 0..100 in x and 0..100 in y.
        /// For example, writing the text at 50,50 means it will be centered onscreen. The height is given in percent of the height of the viewport.
        /// No GL state except the currently bound texture is modified. This method is not as flexible nor as fast
        /// as the WriteString() method, but it is easier to use.
        /// </summary>
        public void WriteStringAt(string text, double heightPercent, double xPercent, double yPercent, double degreesCounterClockwise)
        {
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(0, 100, 0, 100, -1, 1);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Translate(xPercent, yPercent, 0);
            double aspectRatio = ComputeAspectRatio();
            GL.Scale(aspectRatio * heightPercent, heightPercent, heightPercent);
            GL.Rotate(degreesCounterClockwise, 0, 0, 1);
            WriteString(text);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Modelview);
        }

        private static double ComputeAspectRatio()
        {
            var viewport = new int[4];
            GL.GetInteger(GetPName.Viewport, viewport);
            int w = viewport[2];
            int h = viewport[3];
            double aspectRatio = (float)h / w;
            return aspectRatio;
        }

        private void WriteCharacter(char ch, double xpos)
        {
            byte ascii;
            unchecked { ascii = (byte)ch; }

            int row = ascii >> 4;
            int col = ascii & 0x0F;

            double centerx = (col + 0.5) * SIXTEENTH;
            double centery = (row + 0.5) * SIXTEENTH;
            double halfHeight = CharacterBoundingBoxHeight * SIXTEENTH / 2.0;
            double halfWidth = CharacterBoundingBoxWidth * SIXTEENTH / 2.0;
            double left = centerx - halfWidth;
            double right = centerx + halfWidth;
            double top = centery - halfHeight;
            double bottom = centery + halfHeight;

            GL.TexCoord2(left, top); GL.Vertex2(xpos, 1);
            GL.TexCoord2(right, top); GL.Vertex2(xpos + 1, 1);
            GL.TexCoord2(right, bottom); GL.Vertex2(xpos + 1, 0);
            GL.TexCoord2(left, bottom); GL.Vertex2(xpos, 0);
        }

        private readonly int textureId;
        private const double SIXTEENTH = 1.0 / 16.0;
    }
}
