using System;
using System.Collections.Generic;
using System.Text;

namespace learn_lld.LLD.LRU
{
    public class LRUCache
    {
        private class Node
        {
            public int Key;
            public int Value;
            public Node Prev;
            public Node Next;

            public Node(int key, int value)
            {
                Key = key;
                Value = value;
            }
        }

        private readonly int _capacity;
        private readonly Dictionary<int, Node> _cache;

        private readonly Node _head; // Dummy Head
        private readonly Node _tail; // Dummy Tail

        public LRUCache(int capacity)
        {
            _capacity = capacity;
            _cache = new Dictionary<int, Node>();

            _head = new Node(0, 0);
            _tail = new Node(0, 0);

            _head.Next = _tail;
            _tail.Prev = _head;
        }

        public int Get(int key)
        {
            if (!_cache.ContainsKey(key))
                return -1;

            Node node = _cache[key];

            RemoveNode(node);
            AddToFront(node);

            return node.Value;
        }

        public void Put(int key, int value)
        {
            if (_cache.ContainsKey(key))
            {
                Node existingNode = _cache[key];
                existingNode.Value = value;

                RemoveNode(existingNode);
                AddToFront(existingNode);
                return;
            }

            if (_cache.Count == _capacity)
            {
                Node lruNode = _tail.Prev;

                RemoveNode(lruNode);
                _cache.Remove(lruNode.Key);
            }

            Node newNode = new Node(key, value);

            AddToFront(newNode);
            _cache[key] = newNode;
        }

        private void AddToFront(Node node)
        {
            node.Next = _head.Next;
            node.Prev = _head;

            _head.Next.Prev = node;
            _head.Next = node;
        }

        private void RemoveNode(Node node)
        {
            node.Prev.Next = node.Next;
            node.Next.Prev = node.Prev;
        }
    }
}
