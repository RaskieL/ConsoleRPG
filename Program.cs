using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Linq;
namespace ConsoleRPG{
    public class Base{
        static public void Main(string[] args){
            Console.Clear();
            Enemy.Create();

            GameState.Gamestates.Add("Main Menu");
            GameState.Update_Gamestate(GameState.Gamestates[0]);
        }
    }
}