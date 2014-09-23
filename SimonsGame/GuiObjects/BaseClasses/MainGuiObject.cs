using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimonsGame.Modifiers;
using Microsoft.Xna.Framework.Content;
using SimonsGame.GuiObjects.Utility;

namespace SimonsGame.GuiObjects
{
	public enum Group
	{
		Ignore,
		HoldUp,
		PushDown
	}

	public abstract class MainGuiObject : GuiVariables
	{
		protected Guid _guid;
		public Guid Id { get { return _guid; } }


		protected Animator _animator;

		#region Graphics
		public Vector2 Position { get; set; }
		public Vector2 Size { get; set; }
		public Texture2D HitboxImage { get; set; }
		private Color _hitBoxColor = new Color(1f, 1f, 1f, .8f);
		public Color HitBoxColor { get { return _hitBoxColor; } set { _hitBoxColor = value; } }
		public Vector4 Bounds { get { return new Vector4(Position.X, Position.Y, Size.Y, Size.X); } }
		#endregion

		/////////////////////
		// Need a base for //
		// all  modifiable //
		//    variables    //
		/////////////////////
		#region Base Variables
		public float ScaleBase { get; set; }

		#region MovementBase
		// Percentage of MaxSpeeds an object will move in one tick.
		public Vector2 MovementBase { get; set; }

		// Percentage of movement an object can gain in one tick.  Base is 1
		public Vector2 AccelerationBase { get; set; }

		// Max speed one can achieve (right now, only utilizing X direction
		public Vector2 MaxSpeedBase { get; set; }

		// Speed at which the object is currently moving.
		protected Vector2 CurrentMovementBase { get; set; }
		#endregion
		#endregion

		#region Modifiers
		protected List<ModifierBase> Modifiers;
		#endregion

		public Group Group { get; set; }
		public Level Level { get; set; }



		#region Abstract Functions
		public abstract void PreUpdate(GameTime gameTime);
		public abstract void PostUpdate(GameTime gameTime);
		public abstract void PreDraw(GameTime gameTime, SpriteBatch spriteBatch);
		public abstract void PostDraw(GameTime gameTime, SpriteBatch spriteBatch);
		public abstract void SetMovement(GameTime gameTime);
		public abstract float GetXMovement();
		public abstract float GetYMovement();
		public abstract void AddCustomModifiers(GameTime gameTime, ModifierBase modifyAdd);
		public abstract void MultiplyCustomModifiers(GameTime gameTime, ModifierBase modifyMult);
		#endregion

		public MainGuiObject(Vector2 position, Vector2 hitbox, Group group, Level level)
		{
			_guid = Guid.NewGuid();
			Position = position;
			Size = hitbox;
			IGraphicsDeviceService graphicsService = (IGraphicsDeviceService)Program.Game.Services.GetService(typeof(IGraphicsDeviceService));
			HitboxImage = new Texture2D(graphicsService.GraphicsDevice, 1, 1);
			HitboxImage.SetData(new[] { _hitBoxColor });
			Group = group;
			Level = level;

			Modifiers = new List<ModifierBase>();


			// Init to 0 (non-movable objects)
			MovementBase = new Vector2(0f, 0f);
			AccelerationBase = new Vector2(0f, 0f);
			CurrentMovementBase = new Vector2(0f, 0f);
			MaxSpeedBase = new Vector2(0);
			AccelerationBase = new Vector2(1f);
		}

		public void Update(GameTime gameTime)
		{

			PreUpdate(gameTime);


			PostUpdate(gameTime);
		}
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			PreDraw(gameTime, spriteBatch);

			//Rectangle destinationRect = new Rectangle((int)Position.X, (int)Position.Y, (int)Size.X, (int)Size.Y);
			//spriteBatch.Draw(HitboxImage, destinationRect, _hitBoxColor);
			
			PostDraw(gameTime, spriteBatch);
		}
		public Vector2 GetIntersectionDepth(MainGuiObject obj)
		{
			Vector4 thisBounds = this.Bounds;
			Vector4 thatBounds = obj.Bounds;
			return GetIntersectionDepth(thisBounds, thatBounds);
		}
		public static Vector2 GetIntersectionDepth(Vector4 rectA, Vector4 rectB)
		{
			// Calculate half sizes.
			float halfWidthA = rectA.W / 2.0f;
			float halfHeightA = rectA.Z / 2.0f;
			float halfWidthB = rectB.W / 2.0f;
			float halfHeightB = rectB.Z / 2.0f;

			// Calculate centers.
			Vector2 centerA = new Vector2(rectA.X + halfWidthA, rectA.Y + halfHeightA);
			Vector2 centerB = new Vector2(rectB.X + halfWidthB, rectB.Y + halfHeightB);

			// Calculate current and minimum-non-intersecting distances between centers.
			float distanceX = centerA.X - centerB.X;
			float distanceY = centerA.Y - centerB.Y;
			float minDistanceX = halfWidthA + halfWidthB;
			float minDistanceY = halfHeightA + halfHeightB;

			// If we are not intersecting at all, return (0, 0).
			if (Math.Abs(distanceX) >= minDistanceX || Math.Abs(distanceY) >= minDistanceY)
				return Vector2.Zero;

			// Calculate and return intersection depths.
			float depthX = distanceX > 0 ? minDistanceX - distanceX : -minDistanceX - distanceX;
			float depthY = distanceY > 0 ? minDistanceY - distanceY : -minDistanceY - distanceY;
			return new Vector2(depthX, depthY);
		}
	}
}
