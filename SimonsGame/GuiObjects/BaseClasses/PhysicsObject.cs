using Microsoft.Xna.Framework;
using SimonsGame.Modifiers;
using SimonsGame.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimonsGame.GuiObjects.BaseClasses
{
	class PhysicsObject : MainGuiObject
	{

		public AbilityManager AbilityManager { get; set; }
		public bool IsLanded { get; set; }
		protected bool StopGravity { get; set; }
		public Vector2 BasePosition { get; set; }

		public PhysicsObject(Vector2 position, Vector2 hitbox, Group group, Level level)
			: base(position, hitbox, group, level)
		{
		}
		public abstract void PostPhysicsPreUpdate(GameTime gameTime) { }
		public override void PreUpdate(GameTime gameTime) { }
		public override void AddCustomModifiers(GameTime gameTime, ModifierBase modifyAdd) { }
		public override void MultiplyCustomModifiers(GameTime gameTime, ModifierBase modifyMult) { }
		public void ForceAbility(GameTime gameTime) { }
	}
}
