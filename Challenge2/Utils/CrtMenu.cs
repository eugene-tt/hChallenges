using Challenge2.ViewControllers;
using NConsoleMenu;
using NConsoleMenu.DefaultItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge2.Utils
{
    public class CrtMenu : CMenu
    {
        public CrtMenu(string selector = null, bool addDefaultItems = false, bool immediateMode = true, string quitMenu = "..")
            : base(selector, addDefaultItems, immediateMode)
        {
            Add(new MI_Quit(this, quitMenu));

            OnRun += (m) =>
            {
                BaseViewController.BeginOutput();
                BaseViewController.PrintHeader($"{this.Selector} menu");
            };
        }
    }
}
