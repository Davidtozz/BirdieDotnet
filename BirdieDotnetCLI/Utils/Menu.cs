using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Utils.Menu
{
    public sealed class Menu
    {
        //TODO Make options a <string,bool> data type
        private string[] MenuOptions;
        private string MenuTitle;
        private bool[] SelectedMenuOptions;


        //TODO Refactor
        public void ShowMenuOptions()
        {
            int CursorIndex = 0;
            while (true)
            {
                Console.Clear(); // Render cursor movement
                Console.WriteLine(MenuTitle);

                for (int i = 0; i < MenuOptions.Length; i++)
                {
                    char selectionIndicator = SelectedMenuOptions[i] ? '*' : ' ';
                    string cursorIndicator = i == CursorIndex ? "\u001b[1;36m" : ""; // sets line color to ANSI cyan
                    Console.WriteLine($"{cursorIndicator}[{selectionIndicator}] {MenuOptions[i]}\u001b[0m");
                }

                ConsoleKeyInfo input = Console.ReadKey(true);

                switch (input.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (!(CursorIndex == 0)) { CursorIndex--; }
                        break;

                    case ConsoleKey.DownArrow:
                        if (CursorIndex < MenuOptions.Length - 1) CursorIndex++;
                        break;

                    case ConsoleKey.Spacebar:
                        SelectedMenuOptions[CursorIndex] = !SelectedMenuOptions[CursorIndex];
                        break;
                }
            }
        }

        public Menu withOptions(params string[] options)
        {
            MenuOptions = options;

            //TODO Make options a <string,bool> data type
            SelectedMenuOptions = new bool[MenuOptions.Length];
            return this;
        }

        public Menu withTitle(string title)
        {
            MenuTitle = title;
            return this;
        }
    
        public Menu OnOptionSelected()
        {
            throw new NotImplementedException();
            
        }
        
    }

}