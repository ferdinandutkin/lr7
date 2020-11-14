using System.Collections;
using System.Linq;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using System.IO;

namespace lr4
{
    public interface ICollection<T> : IEnumerable<T> //where T:  
    {
        public void Add(T el);

        public void Insert(int index, T el);
        public void Remove(T el);
        public void RemoveAt(int index);

        T this[Index index]
        {
            get;
            set;
        }
    }


    
    public  class SLL<T> : ICollection<T>  
    {

        public SLL()
        {
            Head = null;
            Tail = null;
            Length = 0;
        }

        public SLL(IEnumerable<T> values) : this()
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }

        public SLL(params T[] values) : this() => Add(values);

        public SLL(T value)
        {
            var node = new Node(value);
            Head = node;
            Tail = node;
            Length = 1;
        }


        public T this[Index index]
        {
            get
            { 
                if (index.Value < Length)
                {
                    return this.ElementAt(index.IsFromEnd ? (Length - index.Value) : (index.Value));         
                }
                else throw new IndexOutOfRangeException();
                

            }


            set
            {
                if (index.Value < Length)
                {
                   
                    int destindex = index.IsFromEnd ? (Length - index.Value) : (index.Value);

                    var current = Head;

                    for (int i = 0; i < destindex; i++)
                    {
                        current = current.Next;
                    }

                    current.Value = value;
                }
                else throw new IndexOutOfRangeException();
            }

        }

    
      
        public override bool Equals(object obj) => (obj is SLL<T> list) ? list.SequenceEqual(this) : false;

        public void Add(params T[] values)
        {
            foreach (T val in values)
            {
                Add(val);
            }
        }

        public void Add(T value)
        {
            var node = new Node(value);

            if (Head == null)
            {
                Head = node;
            }
            else
            {
                Tail.Next = node;
            }

            Tail = node;
            Length++;
        }

        public void RemoveAt(int index)
        {
            int count = 0;
            var curr = Head;
            var prev = curr;

            while (count <= index)
            {
                if (count == index)
                {
                    prev.Next = curr.Next;
                    curr = null;
                    Length--;
                }
                else
                {
                    prev = curr;
                    curr = curr.Next;
                }
                count++;
            }

        }

        public void Insert(int index, T el)
        {
            int count = 0;
            var curr = Head;

            while (count <= index)
            {
                if (count == index)
                {                 
                    var node = new Node(el);

                    node.Next = curr.Next;
                   
                    curr.Next = node;
                }
                else
                {
                    curr = curr.Next;
                }
                count++;
            }

        }

        public void Remove(T value)
        {
            var curr = Head;
            var prev = curr;

            while (curr != null)
            {
                if (curr.Value.Equals(value))
                {
                    if (curr == Head)
                    {
                        Head = curr.Next;
                    }
                    else if (curr == Tail)
                    {
                        Tail = prev;
                    }
                    else
                    {
                        prev.Next = curr.Next;
                    }

                    Length--;
                    return;
                }
                prev = curr;
                curr = curr.Next;
            }
        }


        public SLL<T> Slice(int start, int length)
        {
            var arr = this.ToArray();
            var slice = new T[length];
            Array.Copy(arr, start, slice, 0, length);
            return new SLL<T>(slice);
        }

        static public bool operator ==(SLL<T> l, SLL<T> r) => l.Equals(r);
        static public bool operator !=(SLL<T> l, SLL<T> r) => !(l == r);

        static public SLL<T> operator +(SLL<T> l, SLL<T> r) => new SLL<T>(l.Concat(r));
        
        public override string ToString() => '{' + string.Join(", ", this) + '}';  


        public IEnumerator<T> GetEnumerator()
        {
            var curr = Head;
            while (curr != null)
            {
                yield return curr.Value;
                curr = curr.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class Node
        {
            public T Value { get; set; }
            public Node Next { get; set; }
            public Node(T value) => Value = value;
            public Node() => Value = default;
        }

        private Node Head { get; set; }
        private Node Tail { get; set; }
        public int Length { get; private set; }



        static public SLL<T> FromJson(string json) =>
            new SLL<T>(JsonConvert.DeserializeObject<IEnumerable<T>>(json));


        static public SLL<T> FromJsonFile(string filename) =>
            FromJson(File.ReadAllText(filename));
   

    }


}


