using System;
using System.Collections.Generic;

using OpenTK;

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

			game.Update();

			foreach(Entity entity in entities)
			{

				entity.Update();

			}

			renderingEngine.Update();
			physicsEngine.Update();

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

			entities.Add(entity);

		}

		public void RemoveEntity(Entity entity)
		{

			entities.Remove(entity);

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