using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LRU
{
    public class Storage<T>
    {
        private LinkedList<Node<T>> list = new LinkedList<Node<T>>();

        private Dictionary<string, LinkedListNode<Node<T>>> table =
                    new Dictionary<string, LinkedListNode<Node<T>>>();

        public Storage(int capacity)
        {
            Capacity = capacity;
        }

        public int Capacity { get; private set; }

        public void Put(string key, T value)
        {
            if (table.ContainsKey(key))
            {
                UpdateExistingNode(key, value);
            }
            else
            {
                if (table.Count >= Capacity)
                {
                    table.Remove(list.Last.Value.Key);
                    list.RemoveLast();
                }

                AddNodeToFront(key, value);
            }
        }

        // Get the node and move it to front
        // return value associate with node
        public T Get(string key)
        {
            if (!table.ContainsKey(key))
            {
                return default(T);
            }

            LinkedListNode<Node<T>> linkedListNode = table[key];

            T result = linkedListNode.Value.Value;

            list.Remove(linkedListNode);
            list.AddFirst(linkedListNode);

            return result;
        }

        // Creates a new node in the linked list
        // Adds reference of this node the dictionary
        private void AddNodeToFront(string key, T value)
        {
            Node<T> newNode = new Node<T>(key, value);
            LinkedListNode<Node<T>> newLinkedListNode = list.AddFirst(newNode);
            table.Add(key, newLinkedListNode);
        }

        private void UpdateExistingNode(string key, T value)
        {
            LinkedListNode<Node<T>> existingLinkedListNode = table[key];
            existingLinkedListNode.Value.Value = value;

            // Move node to front of list if it is 
            // not at the start already
            if (list.First != existingLinkedListNode)
            {
                list.Remove(existingLinkedListNode);
                list.AddFirst(existingLinkedListNode);
            }
        }
    }


    public class Node<T>
    {
        public Node(string key, T value)
        {
            Key = key;
            Value = value;
        }

        public string Key { get; set; }

        public T Value { get; set; }
    }
}
