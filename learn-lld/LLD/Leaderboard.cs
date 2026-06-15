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

        // Used for tie-break
        public long AchievementTime { get; set; }
    }
    public class LeaderboardComparer : IComparer<Player>
    {
        public int Compare(Player x, Player y)
        {
            if (x.Score != y.Score)
                return y.Score.CompareTo(x.Score);

            if (x.AchievementTime != y.AchievementTime)
                return x.AchievementTime.CompareTo(y.AchievementTime);

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
            if (!_players.ContainsKey(playerId))
            {
                var player = new Player
                {
                    Id = playerId,
                    Score = delta,
                    AchievementTime = ++_sequence
                };

                _players[playerId] = player;
                _ranking.Add(player);

                return;
            }

            var existing = _players[playerId];

            _ranking.Remove(existing);

            existing.Score += delta;

            existing.AchievementTime = ++_sequence;

            _ranking.Add(existing);
        }

        public List<Player> GetTopN(int n)
        {
            return _ranking.Take(n).ToList();
        }
    }
}
