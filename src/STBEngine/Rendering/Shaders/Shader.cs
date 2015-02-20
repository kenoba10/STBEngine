using System;

using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace STBEngine.Rendering.Shaders
{

	public abstract class Shader
	{

		private int program;

		public Shader()
		{

			program = GL.CreateProgram();

		}

		public void AddVertexShader(string source)
		{

			int shader = GL.CreateShader(ShaderType.VertexShader);

			GL.ShaderSource(shader, source);
			GL.CompileShader(shader);

			int result;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out result);

			if(result == 0)
			{

				Console.WriteLine(GL.GetShaderInfoLog(program));

			}

			GL.AttachShader(program, shader);

			GL.DeleteShader(shader);

		}

		public void AddGeometryShader(string source)
		{

			int shader = GL.CreateShader(ShaderType.GeometryShader);

			GL.ShaderSource(shader, source);
			GL.CompileShader(shader);

			int result;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out result);

			if(result == 0)
			{

				Console.WriteLine(GL.GetShaderInfoLog(program));

			}

			GL.AttachShader(program, shader);

			GL.DeleteShader(shader);

		}

		public void AddFragmentShader(string source)
		{

			int shader = GL.CreateShader(ShaderType.FragmentShader);

			GL.ShaderSource(shader, source);
			GL.CompileShader(shader);

			int result;
			GL.GetShader(shader, ShaderParameter.CompileStatus, out result);

			if(result == 0)
			{

				Console.WriteLine(GL.GetShaderInfoLog(program));

			}

			GL.AttachShader(program, shader);

			GL.DeleteShader(shader);

		}

		public void Compile()
		{

			GL.LinkProgram(program);
			GL.ValidateProgram(program);

			int result;

			GL.GetProgram(program, GetProgramParameterName.LinkStatus, out result);

			if(result == 0)
			{

				Console.WriteLine(GL.GetProgramInfoLog(program));

			}

			GL.GetProgram(program, GetProgramParameterName.ValidateStatus, out result);

			if(result == 0)
			{

				Console.WriteLine(GL.GetProgramInfoLog(program));

			}

		}

		public void SetUniform(string uniform, int value)
		{

			GL.Uniform1(GL.GetUniformLocation(program, uniform), value);

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