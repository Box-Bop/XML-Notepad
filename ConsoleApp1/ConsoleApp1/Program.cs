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
                Console.WriteLine("What would you like to do?:\n");
                Console.WriteLine("(1) Write a note.");
                Console.WriteLine("(2) Read all the current notes.");
                var answer = Console.ReadLine();
                var note = new Note();
                if (answer == "1")
                {
                    string path = Directory.GetCurrentDirectory();
                    string xmlPath = path + @"\..\..\Notepad.xml";
                    //var theNote = new List<Note>();
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Great, what should the title of the note be?:\n");
                        note.NoteTitle = Console.ReadLine();
                        if (note.NoteTitle == "")
                        {
                            Console.WriteLine("\nTitle can't be blank");
                            Console.ReadLine();
                        }
                        else
                        {
                            //if (note.NoteTitle.Contains(" "))
                            //{
                            //    Console.WriteLine("\nPlease make a title that doesn't have any spaces in it.");
                            //    Console.ReadLine();
                            //}
                            //else
                            //{
                            //    break;
                            //}
                            break;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("What should the note \"" + note.NoteTitle + "\" contain?:\n");
                    var noteContent = Console.ReadLine();
                    var noteList = new List<Note>();

                    //var note = new Note() { NoteContent = noteContent, NoteTitle = noteTitle };
                    //theNote.Add(note);
                    //more notes

                    //XmlDocument doc = new XmlDocument();
                    //doc.Load("Notepad.xml");
                    //XmlNode newNote = doc.CreateNode(XmlNodeType.Element, "Note", note.NoteTitle);
                    //XmlAttribute newAttribute = doc.CreateAttribute(note.NoteTitle);
                    //newNote.InnerText = noteContent;
                    //doc.DocumentElement.AppendChild(newNote);
                    //doc.Save("Notepad.xml");
                    var note0 = new Note() { NoteTitle = note.NoteTitle, NoteContent = noteContent };
                    noteList.Add(note0);
                    var serializer = new XmlSerializer(noteList.GetType());
                    if (!File.Exists("Notepad.xml"))
                    {
                        using (var writer = XmlWriter.Create("Notepad.xml"))
                        {
                            serializer.Serialize(writer, noteList);
                        }
                    }
                    else
                    {
                        using (var reader = XmlReader.Create("Notepad.xml"))
                        {
                            var notes = (List<Note>)serializer.Deserialize(reader);
                        }
                        var note1 = new Note() { NoteTitle = note.NoteTitle, NoteContent = noteContent };
                        noteList.Add(note1);

                        using (var writer = XmlWriter.Create("Notepad.xml"))
                        {
                            serializer.Serialize(writer, noteList);
                        }
                    }
                    Console.WriteLine("\nYour note has been saved!");
                    Console.ReadLine();
                }
                if (answer == "2")
                {
                    int selection = 0;
                    Console.Clear();
                    Console.WriteLine("These are your notes, which one would you like to read?: (input title of note)\n");
                    List<string> titles = new List<string>();
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load("Notepad.xml");
                    XmlNodeList noteNodes = xmlDoc.SelectNodes("//ArrayOfNotes/Note");
                    foreach (XmlNode xmlnode in xmlDoc.DocumentElement)
                    {
                        Console.WriteLine("(" + selection + ") " + noteNodes);
                        selection++;
                    }
                    selection--;
                    Console.WriteLine();
                    int dec = Convert.ToInt16(Console.ReadLine());
                    if (dec <= selection)
                    {
                        Console.WriteLine();
                        Console.WriteLine(titles[dec]);
                        Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Invalid selection, please choose a note by inputting one of its corresponding numbers");
                        Console.ReadLine();
                    }

                }
            }
        }
    }
}
