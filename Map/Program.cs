using System;
using System.Collections;
using System.Collections.Generic;

namespace Map
{
    class Program
    {
        static void Main(string[] args)
        {
            Map<int, char> map = new Map<int, char>();
            
            map.Insert(2,'a');
            map.Insert(3,'b');
            map.Insert(6,'c');
            map.Insert(5,'d');
            map.Insert(0,'e');
            map.Insert(1,'f');
            map.Insert(8,'g');
            map.Insert(9,'h');
            map.Insert(7,'i');
            map.Insert(10,'k');
            map.Insert(11,'l');
            map.Insert(12,'m');
            map.Insert(13,'n');
            map.Insert(14,'o');

            map.Clear();

            map.Print();
           
        }
    }

}