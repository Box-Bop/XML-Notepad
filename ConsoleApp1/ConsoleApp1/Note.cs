using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace ConsoleApp1
{
    /// <summary>
    /// This is the note class.
    /// It contains the title and then content of the note
    /// </summary>
    /// <remarks>
    /// All functions within the Note class do not need anything passed in it, due to all of them being void functions.
    /// </remarks>
    public class Note
    {
        public string NoteTitle { get; set; }
        public string NoteContent { get; set; }

        /// <summary>
        /// Used to append a note the the Notpad.xml file.
        /// </summary>
        public void NoteWrite()
        {
            var note = new List<Note>();
            string path = Directory.GetCurrentDirectory();
            string xmlPath = path + @"\..\..\Notepad.xml";
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
        /// <summary>
        /// Used to display a list of the current notes in Notepad.xml.
        /// The selection of a note must be made after it's called.
        /// </summary>
        public void NoteRead()
        {
            string path = Directory.GetCurrentDirectory();
            string xmlPath = path + @"\..\..\Notepad.xml";
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
        /// <summary>
        /// Gives the user a selection of notes from the Notepad.xml file, and asks which one to delete.
        /// </summary>
        public void NoteDelete()
        {
            string path = Directory.GetCurrentDirectory();
            string xmlPath = path + @"\..\..\Notepad.xml";
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
