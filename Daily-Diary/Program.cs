namespace DiaryManager
{
    internal class Program
    {
        enum enUserOptions { ReadDiaries= 1, WriteDiary=2 , DeleteDiary= 3, DiaryNumberOfLines= 4 }
        static private enUserOptions readOption()
        {
            string input = Console.ReadLine();
            while (string.IsNullOrEmpty(input) || !input.All(char.IsDigit) || (input != "1" && input != "2" && input != "3" && input != "4"))
            {
                Console.WriteLine("Invalid input!, try again");
                input = Console.ReadLine();
            }
            bool success = Int16.TryParse(input, out Int16 option);
            return (enUserOptions)option;
        }
        static private void showMainScreen()
        {
            Console.WriteLine("--------------- Daily Diary Manager ---------------");
            Console.WriteLine("[1]Read All Diaries");
            Console.WriteLine("[2]Write an Entry");
            Console.WriteLine("[3]Delete an Entry");
            Console.WriteLine("[4]Number of Lines in the Diary");
            Console.Write("Please choose an option: ");
        }
        static Entry ReadEntryInfo() 
        {
            Entry entry = new Entry();
            Console.WriteLine("Enter entry date in 'yyyy-MM-dd' format");
            entry.date = Console.ReadLine();
            Console.WriteLine("Enter entry content");
            entry.content = Console.ReadLine();
            return entry;
        }
      
        static private void performOption(enUserOptions option)
        {
            Console.Clear();
            switch (option)
            {
                case enUserOptions.ReadDiaries:
                    Console.WriteLine(DailyDiary.ReadAllDiary()); 
                    break;
                case enUserOptions.WriteDiary:
                    Entry entry= ReadEntryInfo();
                    if(DailyDiary.WriteDiary(entry.date, entry.content))
                        Console.WriteLine("Entry added successfully");
                    else
                        Console.WriteLine("Added failed");
                    break;
                case enUserOptions.DeleteDiary:
                    Console.WriteLine("Enter the date of the entry to delete it");
                    string input= Console.ReadLine();
                    if (DailyDiary.DeleteDiaryEntry(input))
                        Console.WriteLine("Entry deleted successfully");
                    else
                        Console.WriteLine("Deleted failed");
                    break;
                case enUserOptions.DiaryNumberOfLines:
                    Console.WriteLine(DailyDiary.DiaryNumberOfLines());
                    break;
            }
            Console.WriteLine("Press any key to go back to Main Menue...");
            Console.ReadKey();
            DiaryManager();
        }

        static public void DiaryManager()
        {
            Console.Clear();
            showMainScreen();
            try
            {
                performOption(readOption());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
        static void Main(string[] args)
        {           
            DiaryManager();

        }
    }
}
