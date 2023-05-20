namespace ConsoleRPG {
    class Base {
        static public void Main(string[] args) {
            Console.Clear();
            Enemy.Create();
            GameState.Gamestates.Add("Main Menu");

            Player player = new Player("playername");

            GameState.Update_Gamestate(GameState.Gamestates[0]);
        }
    }
}