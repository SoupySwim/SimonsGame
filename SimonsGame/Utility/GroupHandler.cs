﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimonsGame.Utility
{
	public enum Group
	{
		Impassable, // Object cannot pass an object with this group.
		ImpassableIncludingMagic, // Object cannot pass an object with this group even with the aid of magic (such as teleport).
		VerticalPassable, // Can pass through object in the Y direction.  This will still hold up.
		PassableFromBottom, // Implying that it is NOT passable from top.
		PassableFromTop, // Implying that it is NOT passable from bottom.
		HorizontalPassable, // Can pass through object in the X direction.  This will still block some objects.
		PassableFromLeft,// Implying that it is NOT passable from right.
		PassableFromRight, // Implying that it is NOT passable from left.
		BothPassable // Can pass through object in the X and Y direction.
	}
}