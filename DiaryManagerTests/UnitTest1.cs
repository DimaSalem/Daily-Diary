using DiaryManager;

namespace DiaryManagerTests
{
    public class UnitTest1
    {
         private string filePath = Path.Combine(Environment.CurrentDirectory, "mydiary.txt");

        [Fact]
        public void ReadDiaryFileTest()
        {
            string expectedValue= File.ReadAllText(filePath);
            string actualValue = DailyDiary.ReadAllDiary();

            Assert.Equal(expectedValue, actualValue);
        }

        [Fact]
        public void WriteDiaryFileTest()
        {
            int numberOfEntriesBefore = DailyDiary.NumberOfEntries();
            DailyDiary.WriteDiary("2024-6-28", "today I solve lab5");

            int expectedNumberOfEntries = ++numberOfEntriesBefore;
            int actualNumberOfEntries= DailyDiary.NumberOfEntries();

            Assert.Equal(expectedNumberOfEntries, actualNumberOfEntries);
        }
    }
}