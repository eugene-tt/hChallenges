using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NConsoleMenu.DefaultItems
{
	public class MI_Quit : CMenuItem
	{
		private readonly CMenu _Menu;

		public MI_Quit (CMenu menu, string title = "quit")
			: base (title)
		{
			_Menu = menu;
			HelpText = ""
				+ "quit\n"
				+ "Quits menu processing.";
		}

		public override void Execute (string arg)
		{
			_Menu.Quit ();
		}
	}
}
