using System;

using OpenTK.Graphics.OpenGL;

using STBEngine.Utilities;

namespace STBEngine.Rendering
{

	public class Shader
	{

		private int program;

		public Shader()
		{

			program = 0;

		}

		public void AddVertexShader(string path)
		{

			if(program != 1)
				program = GL.CreateProgram();

			int shader = GL.CreateShader(ShaderType.VertexShader);

			GL.ShaderSource(shader, IOUtils.ReadFile(path));
			GL.CompileShader(shader);

			Console.WriteLine(GL.GetShaderInfoLog(shader));

			GL.AttachShader(program, shader);

			GL.DeleteShader(shader);

		}

		public void AddFragmentShader(string path)
		{

			if(program != 1)
				program = GL.CreateProgram();

			int shader = GL.CreateShader(ShaderType.FragmentShader);

			GL.ShaderSource(shader, IOUtils.ReadFile(path));
			GL.CompileShader(shader);

			Console.WriteLine(GL.GetShaderInfoLog(shader));

			GL.AttachShader(program, shader);

			GL.DeleteShader(shader);

		}

		public void AddGeometryShader(string path)
		{

			if(program != 1)
				program = GL.CreateProgram();

			int shader = GL.CreateShader(ShaderType.GeometryShader);

			GL.ShaderSource(shader, IOUtils.ReadFile(path));
			GL.CompileShader(shader);

			Console.WriteLine(GL.GetShaderInfoLog(shader));

			GL.AttachShader(program, shader);

			GL.DeleteShader(shader);

		}

		public void Compile()
		{

			GL.LinkProgram(program);
			GL.ValidateProgram(program);

			Console.WriteLine(GL.GetProgramInfoLog(program));

		}

		public void Bind()
		{

			GL.UseProgram(program);

		}

		public void UnBind()
		{

			GL.UseProgram(0);

		}

		public void Delete()
		{

			GL.DeleteProgram(program);

		}

	}

}