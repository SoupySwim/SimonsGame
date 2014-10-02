using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SimonsGame.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// To be implemented later.

namespace SimonsGame.GuiObjects
{
	/// <summary>
	/// Level will contain all of the GUI objects in the game.
	/// It will go through every object and "Draw" and "Update" them.
	/// </summary>
	public class Level
	{

		// This will store the objects that make up the environment.
		private Dictionary<Group, List<MainGuiObject>> _guiObjects;

		// This will store what players are currently in the environment.
		private Dictionary<Guid, Player> _players;
		public Dictionary<Guid, Player> Players { get { return _players; } }


		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="Size"> Determines the viewport of the Level (What is displayed on the screen). </param>
		public Level(Vector2 Size)
		{
			_guiObjects = new Dictionary<Group, List<MainGuiObject>>();
			_players = new Dictionary<Guid, Player>();
		}
		public void AddPlayer(Player player)
		{
			_players.Add(player.Id, player);
		}
		public void AddGuiObject(MainGuiObject guiObject)
		{
			if (_guiObjects.ContainsKey(guiObject.Group))
			{
				_guiObjects[guiObject.Group].Add(guiObject);
			}
			else
			{
				_guiObjects.Add(guiObject.Group, new List<MainGuiObject>() { guiObject });
			}
		}

		// Update will call Update on all of the objects within the level.
		public void Update(GameTime gameTime)
		{
			// Update all the platforms
			foreach (var kv in _guiObjects)
			{
				kv.Value.ForEach(p => p.Update(gameTime));
			}
			// Update all the players.
			Players.Values.ToList().ForEach(p => p.Update(gameTime));
		}

		// Draw will call Draw on all of the objects within the level.
		public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
		{
			// Draw all the platforms
			foreach (var kv in _guiObjects)
			{
				kv.Value.ForEach(p => p.Draw(gameTime, spriteBatch));
			}
			// Draw all the players.
			Players.Values.ToList().ForEach(p => p.Draw(gameTime, spriteBatch));
		}

		public Dictionary<Group, List<MainGuiObject>> GetAllGuiObjects()
		{
			return _guiObjects;
		}

		public List<MainGuiObject> GetAllGuiObjects(Group group)
		{
			List<MainGuiObject> objects = new List<MainGuiObject>();
			_guiObjects.TryGetValue(group, out objects);
			return objects;
		}
	}
}
