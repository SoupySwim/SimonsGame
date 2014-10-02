using Microsoft.Xna.Framework;
using SimonsGame.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SimonsGame.GuiObjects
{
	public class Player : PhysicsObject
	{

		// Temp jump code.
		private int _testJumpTime = 20;
		public Player(Vector2 position, Vector2 hitbox, Group group, Level level)
			: base(position, hitbox, group, level)
		{

		}
		public override float GetXMovement()
		{
			KeyboardState keyboardState = Keyboard.GetState();
			int direction = 0;
			if (keyboardState.IsKeyDown(Keys.D))
				direction = 4;
			else if (keyboardState.IsKeyDown(Keys.A))
				direction = -4;
			return direction;
		}
		public override float GetYMovement()
		{
			// Temp "bad" jump logic.
			KeyboardState keyboardState = Keyboard.GetState();
			int direction = keyboardState.IsKeyDown(Keys.W) ? -1 : 0;

			if (_testJumpTime != 20)
				_testJumpTime--;
			if (_testJumpTime == -15)
			{
				_testJumpTime = 20;
			}
			if (direction == 0)
			{
				return CurrentMovement.Y;
			}
			else if (_testJumpTime > 0)
			{
				if (_testJumpTime == 20)
				{
					_testJumpTime--;
				}
				return direction * _testJumpTime;
			}
			return CurrentMovement.Y;
		}
		public override void PostPhysicsPreUpdate(GameTime gameTime)
		{
			KeyboardState keyboardState = Keyboard.GetState();
			VerticalPass = keyboardState.IsKeyDown(Keys.S);
		}
		public override void AddCustomModifiers(GameTime gameTime, Modifiers.ModifierBase modifyAdd) { }
		public override void MultiplyCustomModifiers(GameTime gameTime, Modifiers.ModifierBase modifyMult) { }
		public override void PostUpdate(GameTime gameTime) { }
		public override void PreDraw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }
		public override void PostDraw(GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }
		public override void SetMovement(GameTime gameTime) { }
	}
}
