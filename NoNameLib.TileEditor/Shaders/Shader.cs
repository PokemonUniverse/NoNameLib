using OpenTK.Graphics.OpenGL;

namespace NoNameLib.TileEditor.Shaders
{
    internal class Shader
    {
        public int ProgramId { get; private set; }

        public int UTexture { get; private set; }
        public int UModulation { get; private set; }
        public int UProjection { get; private set; }

        public int APosition { get; private set; }
        public int ATexCoord { get; private set; }

        internal Shader(int programId)
        {
            ProgramId = programId;

            UTexture = GL.GetUniformLocation(programId, "u_texture");
            UModulation = GL.GetUniformLocation(programId, "u_modulation");
            UProjection = GL.GetUniformLocation(programId, "u_projection");

            APosition = GL.GetAttribLocation(programId, "a_position");
            ATexCoord = GL.GetAttribLocation(programId, "a_texCoord");
        }
    }
}
