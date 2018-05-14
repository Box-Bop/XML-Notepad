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
                    var serializer = new XmlSerializer(note.GetType());
                    if (!File.Exists(xmlPath))
                    {
                        using (var writer = XmlWriter.Create(xmlPath))
                        {
                            serializer.Serialize(writer, note);
                        }
                    }
                    else if (File.Exists(xmlPath))
                    {
                        using (var reader = XmlReader.Create(xmlPath))
                        {
                            note = (List<Note>)serializer.Deserialize(reader);
                        }
                    }
                    string title = "";
                    while (true)
                    {
                        Console.Clear();
                        Console.WriteLine("Great, what should the title of the note be?:\n");
                        title = Console.ReadLine();
                        if (title == "")
                        {
                            Console.WriteLine("\nTitle shouldn't be blank");
                            Console.ReadLine();
                        }
                        else
                        {
                            break;
                        }
                    }
                    Console.Clear();
                    Console.WriteLine("What should the note \"" + title + "\" contain?:\n");
                    var noteContent = Console.ReadLine();
                    var insertNote = new Note() { NoteTitle = title, NoteContent = noteContent };
                    note.Add(insertNote);
                    using (var writer = XmlWriter.Create(xmlPath))
                    {
                        serializer.Serialize(writer, note);
                    }
                    Console.WriteLine("\nYour note has been saved!");
                    Console.ReadLine();
                }
                if (answer == "2")
                {
                    int selection = 0;
                    Console.Clear();
                    Console.WriteLine("These are your notes, which one would you like to read?: (input title of note)\n");
                    if (!File.Exists(xmlPath))
                    {
                        Console.WriteLine("Notepad.xml file doesn't exist.");
                    }
                    else
                    {
                        var notes = new List<Note>();
                        var serializer = new XmlSerializer(typeof(List<Note>));
                        using (var reader = XmlReader.Create(xmlPath))
                        {
                            notes = (List<Note>)serializer.Deserialize(reader);
                        }
                        foreach (var n0te in notes)
                        {
                            Console.WriteLine("(" + selection + ") " + n0te.NoteTitle);
                            selection++;
                        }
                        selection--;
                        Console.WriteLine();
                        if (selection >= 0)
                        {
                            int dec = Convert.ToInt16(Console.ReadLine());
                            if (dec <= selection)
                            {
                                Console.WriteLine();
                                Console.WriteLine(notes[dec].NoteContent);
                                Console.ReadLine();
                            }
                            else
                            {
                                Console.WriteLine("Invalid selection, please choose a note by inputting one of its corresponding numbers");
                                Console.ReadLine();
                            }
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("No available notes.");
                            Console.ReadLine();
                        }
                    }
                }

                if (answer == "3")
                {
                    Console.Clear();
                    int selection = 0;
                    Console.WriteLine("Which note would you like to delete?:\n");
                    var remNotes = new List<Note>();
                    var serializer = new XmlSerializer(typeof(List<Note>));
                    using (var reader = XmlReader.Create(xmlPath))
                    {
                        remNotes = (List<Note>)serializer.Deserialize(reader);
                    }
                    foreach (var n0te in remNotes)
                    {
                        Console.WriteLine("(" + selection + ") " + n0te.NoteTitle);
                        selection++;
                    }
                    selection--;
                    Console.WriteLine();
                    if (selection >= 0)
                    {
                        int dec = Convert.ToInt16(Console.ReadLine());
                        if (dec <= selection)
                        {
                            remNotes.RemoveAt(dec);
                            using (var writer = XmlWriter.Create(xmlPath))
                            {
                                serializer.Serialize(writer, remNotes);
                            }
                            Console.WriteLine("\nYour note has been deleted!");
                            Console.ReadLine();
                        }
                        else
                        {
                            Console.WriteLine("Invalid selection, please choose a note by inputting one of its corresponding numbers");
                            Console.ReadLine();
                        }
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("No available notes.");
                        Console.ReadLine();
                    }
                }
            }
        }
    }
}
