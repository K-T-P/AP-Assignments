using System;

namespace tamrin_seri_7_soal_1
{
    class Program
    {
        static void Main()
        {
            try
            {
                Friend<Bear> bear = new Friend<Bear>(new Bear("Pooh", 5));
                Friend<Tiger> tiger = new Friend<Tiger>(new Tiger("Tiger", 3));
                Friend<Pig> pig = new Friend<Pig>(new Pig("Piglet", 4));
                Friend<Kangaroo> kangaroo = new Friend<Kangaroo>(new Kangaroo("Roo", 2));
                Friend<Donkey> donkey = new Friend<Donkey>(new Donkey("Iver", 1));

                bear.friend.Personality();
                tiger.friend.Personality();
                pig.friend.Personality();
                kangaroo.friend.Personality();
                donkey.friend.Personality();

                bear.Personality();
                tiger.Personality();
                pig.Personality();
                kangaroo.Personality();
                donkey.Personality();
            }
            catch
            {
                Console.WriteLine("An Error occured!");
            }
        }
    }
    interface IPersonality
    {
        string Name
        {
            get;
        }
        int Point
        {
            get;
        }
        string Personality();
    }
    class Bear : IPersonality
    {
        private string _name;
        public string Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        private int _point;
        public int Point
        {
            get { return this._point; }
            private set { this._point = value; }
        }

        public Bear(string bearName, int bearPoint)
        {
            this.Name = bearName;
            this.Point = bearPoint;
        }

        public string Personality()
        {
            return Name + " is yellow bear.\nHe loves honey.\n" + Name + "'s rate is " + Point;
        }
    }
    class Tiger : IPersonality
    {
        private string _name;
        public string Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        private int _point;
        public int Point
        {
            get { return this._point; }
            private set { this._point = value; }
        }

        public Tiger(string tigerName, int tigerPoint)
        {
            this.Name = tigerName;
            this.Point = tigerPoint;
        }

        public string Personality()
        {
            return Name + " is orange tiger.\nHe loves flowers.\n" + Name + "'s rate is " + Point;
        }
    }
    class Kangaroo : IPersonality
    {
        private string _name;
        public string Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        private int _point;
        public int Point
        {
            get { return this._point; }
            private set { this._point = value; }
        }

        public Kangaroo(string kangarooName, int kangarooPoint)
        {
            this.Name = kangarooName;
            this.Point = kangarooPoint;
        }

        public string Personality()
        {
            return Name + " is a brown kangaroo.\nHe loves jumping.\n" + Name + "'s rate is " + Point;
        }
    }
    class Pig : IPersonality
    {
        private string _name;
        public string Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        private int _point;
        public int Point
        {
            get { return this._point; }
            private set { this._point = value; }
        }

        public Pig(string pigName, int pigPoint)
        {
            this.Name = pigName;
            this.Point = pigPoint;
        }

        public string Personality()
        {
            return Name + " is a pink pig.\nHe loves playing.\n" + Name + "'s rate is " + Point;
        }
    }
    class Donkey : IPersonality
    {
        private string _name;
        public string Name
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        private int _point;
        public int Point
        {
            get { return this._point; }
            private set { this._point = value; }
        }

        public Donkey(string donkeyName, int donkeyPoint)
        {
            this.Name = donkeyName;
            this.Point = donkeyPoint;
        }

        public string Personality()
        {
            return Name + " is a blue donkey.\nHe loves sleeping.\n" + Name + "'s rate is " + Point;
        }
    }
    class Friend<T> where T : IPersonality
    {
        public T friend;
        public Friend(T friend)
        {
            this.friend = friend;
        }
        public string Personality()
        {
            return this.friend.Personality();
        }
    }
}
