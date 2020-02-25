using System;

namespace Ray2Mod.Components.UI.Menu
{
    public struct MenuItem
    {
        public MenuItem(string name, Action action)
        {
            Name = name;
            Action = action;
            Submenu = null;
        }

        public MenuItem(string name, Menu submenu)
        {
            Name = name;
            Action = null;
            Submenu = submenu;
        }

        public string Name;
        public Action Action;
        public Menu Submenu;
    }
}