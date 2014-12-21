using System;
using System.Collections.Generic;

using OpenTK;
using OpenTK.Audio;
using OpenTK.Audio.OpenAL;

using STBEngine.Core.Event;
using STBEngine.Rendering;
using STBEngine.Physics;

namespace STBEngine.Core
{

	public class CoreEngine
	{

		private static readonly CoreEngine instance = new CoreEngine();

		private IGame game;

		private List<Entity> entities;

		private STBEventManager eventHandler;

		private RenderingEngine renderingEngine;
		private PhysicsEngine physicsEngine;

		private CoreEngine()
		{

			entities = new List<Entity>();

			eventHandler = new STBEventManager();

			renderingEngine = new RenderingEngine();
			physicsEngine = new PhysicsEngine();

		}

		public void Initialize()
		{

			renderingEngine.Initialize();
			physicsEngine.Initialize();

			game.Initialize();

		}

		public void Update()
		{

			Vector3 position = renderingEngine.Camera.Parent.Transformation.Position;
			Vector3 orientation = renderingEngine.Camera.Forward;
			Vector3 up = new Vector3(0f, 1f, 0f);

			AL.Listener(ALListener3f.Position, ref position);
			AL.Listener(ALListenerfv.Orientation, ref orientation, ref up);

			renderingEngine.Update();
			physicsEngine.Update();

			game.Update();

			foreach(Entity entity in entities)
			{

				renderingEngine.UpdateUniforms(entity);

				entity.Update();

			}

		}

		public void Render()
		{

			foreach(Entity entity in entities)
			{

				renderingEngine.Render(entity);

			}

		}

		public void Terminate()
		{

			for(uint i = 0; i < entities.Count; i++)
			{

				RemoveEntity(entities[0]);

			}

			game.Terminate();

			physicsEngine.Terminate();
			renderingEngine.Terminate();

		}

		public void Run(IGame game, uint ups, uint fps)
		{

			this.game = game;

			using(GameWindow window = new Window(game.Title))
			{

				window.Run(ups, fps);

			}

		}

		public void AddEntity(Entity entity)
		{

			entity.Initialize();

			entities.Add(entity);

		}

		public void RemoveEntity(Entity entity)
		{

			entities.Remove(entity);

			entity.Terminate();

		}

		public STBEventManager EventHandler
		{

			get
			{

				return eventHandler;

			}

		}

		public RenderingEngine RenderingEngine
		{

			get
			{

				return renderingEngine;

			}

		}

		public PhysicsEngine PhysicsEngine
		{

			get
			{

				return physicsEngine;

			}

		}

		public static CoreEngine Instance
		{

			get
			{

				return instance;

			}

		}

	}

}