// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System.Collections;

namespace GameLibrary
{
    public class List<T> where T : class
    {
        internal class Node
        {
            internal T data;
            internal Node next;
            internal Node prev;
            public Node(T d)
            {
                data = d;
                next = null;
                prev = null;
            }

        }

        private Node Head, Tail;
        public uint Count { get; internal set; }

        public List()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        public void Append(T item)
        {
            if (Head == null)
            {
                Head = new Node(item);
                Tail = Head;
            }
            else
            {
                Head.next = new Node(item)
                {
                    prev = Head
                };
                Head = Head.next;
            }
            Count += 1;
        }

        public bool Remove(T item)
        {
            var pointer = Tail;
            bool result = false;
            while (pointer != null && !result)
            {
                if (item == pointer.data)
                {
                    if (Count == 1)
                    {
                        Head = Tail = null;
                    }
                    else if (pointer.prev == null)
                    {
                        Tail = pointer.next;
                        pointer.next.prev = null; //-V3095
                    }
                    else if (pointer.next == null)
                    {
                        Head = pointer.prev;
                        pointer.prev.next = null;
                    }
                    else
                    {
                        pointer.next.prev = pointer.prev;
                        pointer.prev.next = pointer.next;
                    }
                    pointer.next = pointer.prev = null;
                    Count--;
                    result = true;
                }
                pointer = pointer.next;
            }
            return result;
        }
        public IEnumerator GetEnumerator()
        {
            var pointer = Tail;
            while (pointer != null)
            {
                yield return pointer.data;
                pointer = pointer.next;
            }
        }

    }
}
