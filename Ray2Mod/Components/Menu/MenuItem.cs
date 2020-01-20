using System;

namespace Ray2Mod.Components.Menu
{
    public struct MenuItem
    {
        public MenuItem(string name, Action action)
        {
            Name = name;
            Action = action;
            IsSubmenu = false;
            Submenu = null;
        }

        public MenuItem(string name, Menu submenu)
        {
            Name = name;
            Action = null;
            IsSubmenu = true;
            Submenu = submenu;
        }

        public string Name;
        public Action Action;
        public bool IsSubmenu;
        public Menu Submenu;
    }
}