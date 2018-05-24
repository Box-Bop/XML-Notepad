using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.IO;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("What would you like to do?:\n" +
                    "\n(1) Write a note." +
                    "\n(2) Read from a note." +
                    "\n(3) Delete a note.");
                var answer = Console.ReadLine();
                var note = new List<Note>();
                string path = Directory.GetCurrentDirectory();
                string xmlPath = path + @"\..\..\Notepad.xml";
                if (answer == "1")
                {
                    var write = new Note();
                    write.NoteWrite();
                }
                if (answer == "2")
                {
                    var read = new Note();
                    read.NoteRead();
                }

                if (answer == "3")
                {
                    var delete = new Note();
                    delete.NoteDelete();
                }
            }
        }
    }
}
