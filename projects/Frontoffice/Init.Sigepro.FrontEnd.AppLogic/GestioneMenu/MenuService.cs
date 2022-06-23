using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Init.Sigepro.FrontEnd.AppLogic.GestioneMenu
{
    public class MenuService
    {
        MenuReader _menuReader;
        MenuUpgrader _menuUpgrader;
        private readonly MenuUrlBuilder _urlBuilder;

        public MenuService(MenuReader menuReader, MenuUpgrader menuUpgrader, MenuUrlBuilder urlBuilder)
        {
            this._menuReader = menuReader;
            this._menuUpgrader = menuUpgrader;
            _urlBuilder = urlBuilder;
        }

        public MainMenu LoadMenu()
        {
            var menuFile = this._menuReader.Read();
            var mainMenu = this._menuUpgrader.Upgrade(menuFile);

            return this._urlBuilder.ParseMenu(mainMenu);
        }
    }
}
