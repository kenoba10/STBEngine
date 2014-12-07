using System;

using OpenTK;
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

		public void SetUniform(string uniform, float value)
		{

			GL.Uniform1(GL.GetUniformLocation(program, uniform), value);

		}

		public void SetUniform(string uniform, Vector2 value)
		{

			GL.Uniform2(GL.GetUniformLocation(program, uniform), value);

		}

		public void SetUniform(string uniform, Vector3 value)
		{

			GL.Uniform3(GL.GetUniformLocation(program, uniform), value);

		}

		public void SetUniform(string uniform, Vector4 value)
		{

			GL.Uniform4(GL.GetUniformLocation(program, uniform), value);

		}

		public void SetUniform(string uniform, Matrix2 value)
		{

			GL.UniformMatrix2(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix2x3 value)
		{

			GL.UniformMatrix2x3(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix2x4 value)
		{

			GL.UniformMatrix2x4(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix3 value)
		{

			GL.UniformMatrix3(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix3x2 value)
		{

			GL.UniformMatrix3x2(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix3x4 value)
		{

			GL.UniformMatrix3x4(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix4 value)
		{

			GL.UniformMatrix4(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix4x2 value)
		{

			GL.UniformMatrix4x2(GL.GetUniformLocation(program, uniform), false, ref value);

		}

		public void SetUniform(string uniform, Matrix4x3 value)
		{

			GL.UniformMatrix4x3(GL.GetUniformLocation(program, uniform), false, ref value);

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