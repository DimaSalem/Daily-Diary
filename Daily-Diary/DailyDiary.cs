using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace DiaryManager
{
    public class DailyDiary
    {
        static private string filePath = Path.Combine(Environment.CurrentDirectory, "mydiary.txt");

        static public List<Entry> ReadDiary()
        {
            List <Entry> entries = new List <Entry>();
            if(File.Exists(filePath))
            {
                string[] lines= File.ReadAllLines(filePath);
                Entry? currentEntry = null;
               
                foreach (string line in lines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        if (currentEntry != null)
                            entries.Add(currentEntry);
                        currentEntry = null;
                    }
                    else
                    {
                        if(currentEntry == null)
                        {
                            currentEntry= new Entry();
                            currentEntry.date = line.Trim();
                        }
                        else
                        {
                            currentEntry.content = line.Trim();
                        }
                    }
                }
                return entries;
            }
            else
            {
                return null;
            }
        }
        static public string ReadAllDiary()
        {
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);
            else
                return null;
        }
        static private string TryGetFormattedDate(string date)
        {
            DateOnly dateOnly;
            bool success = DateOnly.TryParse(date, out dateOnly);

            if (success)
                return dateOnly.ToString("yyyy-MM-dd");
            else
                return null;
        }
        static public bool WriteDiary(string date, string content)
        {
            string formattedDate= TryGetFormattedDate(date);
            if (!string.IsNullOrEmpty(formattedDate) && !string.IsNullOrEmpty(content))
            {
                string[] entryArr = {"", formattedDate, content};
                File.AppendAllLines(filePath, entryArr);
                return true;
            }
            else
            {
                return false;
            }
        }
        static public void WriteAllDiary(List <Entry> entries)
        {
            File.WriteAllText(filePath, "");
            foreach (Entry entry in entries)
            {
                File.AppendAllText(filePath, entry.date);
                File.AppendAllText(filePath, "\n" + entry.content + "\n\n");
            }
        }
        static public bool DeleteDiaryEntry(string date)
        {
            string formattedDate = TryGetFormattedDate(date);
            List<Entry> entries= ReadDiary();

            if (entries == null || string.IsNullOrEmpty(formattedDate))
                return false;

            for(int i=0; i<entries.Count; i++)
            {
                if (entries[i].date == formattedDate)
                {
                    entries.RemoveAt(i);
                    break;
                }
            }         

            WriteAllDiary(entries);
            return true;
        }
        static public int DiaryNumberOfLines()
        {
            string[] numberOfLines= File.ReadAllLines(filePath);
            return numberOfLines.Length;
        }
        static public int NumberOfEntries()
        {
            List<Entry> entries = ReadDiary();
            return entries.Count;
        }



    }
}
