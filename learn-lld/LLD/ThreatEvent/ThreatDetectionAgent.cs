using System;
using System.Collections.Generic;
using System.Text;

namespace learn_lld.LLD.ThreatEvent
{
    public class ThreatEvent
    {
        public string EventId { get; set; }

        public string EventType { get; set; }

        public string HostId { get; set; }

        public DateTime CreatedAt { get; set; }

        public Dictionary<string, object> Metadata { get; set; }
    }

  

    public class ThreatAlert
    {
        public string AlertId { get; set; }

        public string RuleName { get; set; }

        public string Severity { get; set; }

        public ThreatEvent Event { get; set; }
    }
    public class DetectionRule
    {
        public string Name { get; set; }

        public Func<ThreatEvent, bool> Rule { get; set; }

        public bool IsMatch(ThreatEvent threatEvent)
        {
            return Rule(threatEvent);
        }
    }

    public class ThreatDetectionAgent
    {
        private readonly List<DetectionRule> _rules =
            new();

        private readonly Queue<ThreatAlert> _queue =
            new();

        public void AddRule(DetectionRule rule)
        {
            _rules.Add(rule);
        }

        public void ProcessEvent(
            ThreatEvent threatEvent)
        {
            foreach (var rule in _rules)
            {
                if (rule.IsMatch(threatEvent))
                {
                    var alert = new ThreatAlert
                    {
                        AlertId = Guid.NewGuid().ToString(),
                        RuleName = rule.Name,
                        Severity = "High",
                        Event = threatEvent
                    };

                    _queue.Enqueue(alert);
                }
            }
        }

        public List<ThreatAlert> GetPendingAlerts()
        {
            return _queue.ToList();
        }

        public void Demo()
        {
            var agent = new ThreatDetectionAgent();

            // Rule 1
            agent.AddRule(new DetectionRule
            {
                Name = "Suspicious Process",
                Rule = e =>
                    e.EventType == "ProcessCreated" &&
                    e.Metadata.ContainsKey("ProcessName") &&
                    e.Metadata["ProcessName"].ToString() == "cmd.exe"
            });

            // Rule 2
            agent.AddRule(new DetectionRule
            {
                Name = "Failed Login",
                Rule = e =>
                    e.EventType == "LoginFailed" &&
                    e.Metadata.ContainsKey("Count") &&
                    Convert.ToInt32(e.Metadata["Count"]) > 5
            });

            // Event 1
            var event1 = new ThreatEvent
            {
                EventId = "E1",
                EventType = "ProcessCreated",
                HostId = "Server-1",
                CreatedAt = DateTime.UtcNow,
                Metadata = new Dictionary<string, object>
        {
            { "ProcessName", "cmd.exe" }
        }
            };

            // Event 2
            var event2 = new ThreatEvent
            {
                EventId = "E2",
                EventType = "LoginFailed",
                HostId = "Server-2",
                CreatedAt = DateTime.UtcNow,
                Metadata = new Dictionary<string, object>
        {
            { "Count", 10 }
        }
            };

            // Event 3
            var event3 = new ThreatEvent
            {
                EventId = "E3",
                EventType = "ProcessCreated",
                HostId = "Server-3",
                CreatedAt = DateTime.UtcNow,
                Metadata = new Dictionary<string, object>
        {
            { "ProcessName", "notepad.exe" }
        }
            };

            agent.ProcessEvent(event1);
            agent.ProcessEvent(event2);
            agent.ProcessEvent(event3);

            var alerts = agent.GetPendingAlerts();

            foreach (var alert in alerts)
            {
                Console.WriteLine(
                    $"AlertId={alert.AlertId}\n" +
                    $"Rule={alert.RuleName}\n" +
                    $"Severity={alert.Severity}\n" +
                    $"Host={alert.Event.HostId}\n" +
                    $"EventType={alert.Event.EventType}\n");
            }
        }
    }
}
