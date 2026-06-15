using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace learn_lld.LLD
{
    public class Player
    {
        public int Id { get; set; }

        public long Score { get; set; }

        // Sequence when current score was achieved
        public long ScoreAchievedAt { get; set; }

        public override string ToString()
        {
            return $"PlayerId={Id}, Score={Score}, AchievedAt={ScoreAchievedAt}";
        }
    }

    public class LeaderboardComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (ReferenceEquals(x, y))
                return 0;

            // Higher score first
            int scoreCompare = y.Score.CompareTo(x.Score);

            if (scoreCompare != 0)
                return scoreCompare;

            // Earlier achievement wins
            int achievementCompare =
                x.ScoreAchievedAt.CompareTo(y.ScoreAchievedAt);

            if (achievementCompare != 0)
                return achievementCompare;

            // Ensure uniqueness
            return x.Id.CompareTo(y.Id);
        }
    }

    public class Leaderboard
    {
        private readonly Dictionary<int, Player> _players;

        private readonly SortedSet<Player> _ranking;

        private long _sequence;

        public Leaderboard()
        {
            _players = new Dictionary<int, Player>();

            _ranking = new SortedSet<Player>(
                new LeaderboardComparer());

            _sequence = 0;
        }

        public void UpdateScore(int playerId, long delta)
        {
            if (!_players.TryGetValue(playerId, out Player player))
            {
                player = new Player
                {
                    Id = playerId,
                    Score = delta,
                    ScoreAchievedAt = ++_sequence
                };

                _players[playerId] = player;
                _ranking.Add(player);

                return;
            }

            // Remove old ranking position
            _ranking.Remove(player);

            // Update score
            player.Score += delta;

            // Record when this score was achieved
            player.ScoreAchievedAt = ++_sequence;

            // Reinsert
            _ranking.Add(player);
        }

        public List<Player> GetTopN(int n)
        {
            return _ranking.Take(n).ToList();
        }

        public Player GetPlayer(int playerId)
        {
            return _players.TryGetValue(playerId, out Player? player) ? player : null;
        }

        public void PrintLeaderboard()
        {
            Console.WriteLine("Leaderboard:");

            int rank = 1;

            foreach (var player in _ranking)
            {
                Console.WriteLine(
                    $"Rank {rank++}: Player {player.Id}, Score={player.Score}");
            }

            Console.WriteLine();
        }
    }
}
