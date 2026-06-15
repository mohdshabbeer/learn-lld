using learn_lld.LLD;
using learn_lld.LLD.LRU;
using Microsoft.VisualBasic.Logging;
using System.Numerics;

namespace learn_lld
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnDemoLRU_Click(object sender, EventArgs e)
        {
            var cache = new LRUCache(2);
            cache.Put(1, 10);
            cache.Put(2, 20);

            //Console.WriteLine();
            Log(cache.Get(1).ToString()); // 10
            cache.Put(3, 30); // Evicts key 2

            Log(cache.Get(2).ToString()); // -1
            Log(cache.Get(3).ToString()); // 30
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lboxOutput.Items.Clear();
        }

        public void Log(string message)
        {
            lboxOutput.Items.Add(message + Environment.NewLine);
        }

        private void btnLaderBoard_Click(object sender, EventArgs e)
        {
            //var board = new Leaderboard();

            //board.UpdateScore(1, 100);
            //board.UpdateScore(2, 200);
            //board.UpdateScore(3, 150);
            //board.UpdateScore(4, 350);

            //var top = board.GetTopN(2);

            //foreach (var p in top)
            //{
            //    //Console.WriteLine($"{p.Id} - {p.Score}");
            //    Log($"{p.Id} - {p.Score}"); // 30
            //}


            var leaderboard = new Leaderboard();

            leaderboard.UpdateScore(1, 100);
            leaderboard.UpdateScore(2, 100);
            leaderboard.UpdateScore(3, 150);

            leaderboard.PrintLeaderboard();

            Log("Top 2 Players");

            foreach (var player in leaderboard.GetTopN(2))
            {
                Log(player.ToString());
            }
            Log("--");

            Log("Player 2 gains 100 points");

            leaderboard.UpdateScore(2, 100);

            leaderboard.PrintLeaderboard();

            Log("Player 3 loses 50 points");

            leaderboard.UpdateScore(3, -50);

            leaderboard.PrintLeaderboard();
        }
    }
}
