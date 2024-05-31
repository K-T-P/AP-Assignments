using System;
using System.Collections;

namespace tamrin_seri_7_soal_3
{
    class Program
    {
        static void Main()
        {
            MyStack<int> myStack = null;
            while (true)
            {
                try
                {
                    Console.WriteLine("Please enter your initializing numbers [5]: ");
                    int a1 = int.Parse(Console.ReadLine());
                    int a2 = int.Parse(Console.ReadLine());
                    int a3 = int.Parse(Console.ReadLine());
                    int a4 = int.Parse(Console.ReadLine());
                    int a5 = int.Parse(Console.ReadLine());
                    Console.WriteLine("Please enter the size of the stack : ");
                    
                    int size = int.Parse(Console.ReadLine());
                    if (size < 5)
                    {
                        continue;
                    }
                    myStack = new MyStack<int>(size) { a1, a2, a3, a4, a5 };
                    break;
                }
                catch
                {
                    Console.WriteLine("An Error occured");
                }
            }
            while (true)
            {
                try
                {
                    Console.WriteLine("1- Push\n2- Pop\n3- Top\n4- Print\n5- Exit");
                    string order = Console.ReadLine();
                    if (order == "Push") { }
                    else if (order == "Pop") { }
                    else if (order == "Top") {  }
                    else if (order == "Print") { }
                    else if (order == "Exit") { break; }
                }
                catch
                {
                    Console.WriteLine("An Error occured!");
                }
            }
        }
    }
    interface IData<T>
    {
        delegate bool InterfaceDelegate(int a,int b);

        int Count
        {
            get;
        }

        int MaxSize
        {
            get;
        }

        T[] Elements
        {
            get;
        }

        string Print(InterfaceDelegate objDelegate,int S);
    }
    interface IEnumerable<T>
    {
        IEnumerator GetEnumerator();
        T Current
        {
            get;
        }
        bool MoveNext();
        void Reset();
    } 
    class MyStack<T> : IData<T>,IEnumerable
    {

        //     public delegate bool InterfaceDelegate(int a, int b);
        public IEnumerator enumerator ;
        private int _maxSize;
        public int MaxSize
        {
            get { return this._maxSize; }
            private set { this._maxSize = value; }
        }

        private T[] _elements;
        public T[] Elements
        {
            get { return this._elements; }
            private set { }
        }

        private int _count=0;
        public int Count
        {
            get { return this._count; }
            private set { this._count = value; }
        }

        public T Top
        {
            get
            {
                if (Elements.Length == 0)
                    throw new Exception("ElementsIsEmpty");
                else
                    return Elements[Count - 1];
            }
        }

        public T Pop()
        {
            if (Count == 0)
                throw new Exception("EmptyArray");
            else
            {
                T lastElement = Elements[Count - 1];
                T[] newElements = new T[Count];
                Array.Copy(Elements, 0, newElements, 0, Count - 1);
                Elements = newElements;
                Count--;
                return lastElement;
            }
        }

        public void Push(T newElement)
        {
            if (Count == MaxSize)
                throw new Exception("ElementsIsFull");
            else
            {
                Count++;
                Elements[Count] = newElement;
            }
        }

        
        static public bool CheckEquality(int a, int b)
        {
            if (a == b)
                return true;
            else
                return false;
        }

        public string Print(IData<T>.InterfaceDelegate interfaceDelegate, int S)
        {
            interfaceDelegate = new IData<T>.InterfaceDelegate(CheckEquality);
            if (Count != 0)
            {
                if (interfaceDelegate(1, S))
                {
                    string numbers = "";
                    T[] cheatSheetElements = new T[Count];
                    int totalIndex = Count - 1;
                    for (int index = 0; totalIndex >= index; index++)
                    {
                        cheatSheetElements[index] = Pop();
                        if ((totalIndex - index).IsOdd())
                        {
                            numbers += cheatSheetElements[index].ToString();
                        }
                    }
                    Elements = new T[totalIndex];
                    for (int i = totalIndex; i >= 0; i--)
                    {
                        Push(cheatSheetElements[i]);
                    }
                    return numbers;
                }
                else if (interfaceDelegate(2, S))
                {
                    string numbers = "";
                    T[] cheatSheetElements = new T[Count];
                    int totalIndex = Count - 1;
                    for (int index = 0; totalIndex >= index; index++)
                    {
                        cheatSheetElements[index] = Pop();
                        if ((totalIndex - index).IsEven())
                        {
                            numbers += cheatSheetElements[index].ToString();
                        }
                    }
                    Elements = new T[totalIndex];
                    for (int i = totalIndex; i >= 0; i--)
                    {
                        Push(cheatSheetElements[i]);
                    }
                    return numbers;
                }
                else if (interfaceDelegate(3, S))
                {
                    string numbers = "";
                    T[] cheatSheetElements = new T[Count];
                    int totalIndex = Count - 1;
                    for (int index = 0; totalIndex >= index; index++)
                    {
                        cheatSheetElements[index] = Pop();
                        numbers += cheatSheetElements[index].ToString();
                    }
                    Elements = new T[totalIndex];
                    for (int i = totalIndex; i >= 0; i--)
                    {
                        Push(cheatSheetElements[i]);
                    }
                    return numbers;
                }
                else
                {
                    return "Invalid input!";
                }
            }
            else
            {
                return "Elements is empty!";
            }
        }

        public IEnumerator GetEnumerator()
        {
            foreach(T data in Elements)
            {
                yield return data;
            }
        }

        public void Add(T initializingElement)
        {
            this.Push(initializingElement);
        }

        public MyStack(int MaxSize)
        {
            if (MaxSize <= 0)
                throw new Exception("InvalidMaxSize");
            this.MaxSize = MaxSize;
            this._elements = new T[MaxSize];
        }

        //public T Current
        //{
        //    get { return ; }
        //}
    }
    static class Util
    {
        static public bool IsOdd(this int a)
        {
            if (a % 2 == 1)
                return true;
            else
                return false;
        }
        static public bool IsEven(this int a)
        {
            if (a % 2 == 0)
                return true;
            else
                return false;
        }
    }
}
