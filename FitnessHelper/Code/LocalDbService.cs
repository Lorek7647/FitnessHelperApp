using SQLite;

namespace FitnessHelper.Code
{
    public class LocalDbService
    {
        private const string DB_NAME = "fitness_local_db.db3";
        private static string DB_PATH => Path.Combine(FileSystem.AppDataDirectory, DB_NAME);
        private SQLiteAsyncConnection _connection;
        private SQLiteAsyncConnection Database => (_connection ??= new SQLiteAsyncConnection(DB_PATH, SQLiteOpenFlags.Create |
            SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache));
        public LocalDbService()
        {
            
        }
        /////////////////////////////////////
        async Task CreateTableGender()
        {
            var result = await Database.CreateTableAsync<Gender>();
        }
        public async Task<List<Gender>> GetGenderList()
        {
            await CreateTableGender();
            return await Database.Table<Gender>().ToListAsync();
        }
        public async Task<int> SaveGender(Gender item)
        {
            await CreateTableGender();
            await Database.ExecuteAsync("DELETE FROM Gender");
            return await Database.InsertAsync(item);
        }
        /////////////////////////////////////
        async Task CreateTableAge()
        {
            var result = await Database.CreateTableAsync<Age>();
        }
        public async Task<List<Age>> GetAgeList()
        {
            await CreateTableAge();
            return await Database.Table<Age>().ToListAsync();
        }
        public async Task<int> SaveAge(Age item)
        {
            await CreateTableAge();
            await Database.ExecuteAsync("DELETE FROM Age");
            return await Database.InsertAsync(item);
        }
        /////////////////////////////////////
        async Task CreateTableHeight()
        {
            var result = await Database.CreateTableAsync<Height>();
        }
        public async Task<List<Height>> GetHeightList()
        {
            await CreateTableHeight();
            return await Database.Table<Height>().ToListAsync();
        }
        public async Task<int> SaveHeight(Height item)
        {
            await CreateTableHeight();
            await Database.ExecuteAsync("DELETE FROM Height");
            return await Database.InsertAsync(item);
        }
        /////////////////////////////////////
        async Task CreateTableWeight()
        {
            var result = await Database.CreateTableAsync<Code.Weight>();
        }
        public async Task<List<Code.Weight>> GetWeightList()
        {
            await CreateTableWeight();
            return await Database.Table<Code.Weight>().ToListAsync();
        }
        public async Task<int> SaveWeight(Code.Weight item)
        {
            await CreateTableWeight();
            await Database.ExecuteAsync("DELETE FROM Weight");
            return await Database.InsertAsync(item);
        }
        /////////////////////////////////////
        async Task CreateTableTimerBreak()
        {
            var result = await Database.CreateTableAsync<TimerBreak>();
        }
        public async Task<List<TimerBreak>> GetTimerBreakList()
        {
            await CreateTableTimerBreak();
            return await Database.Table<TimerBreak>().ToListAsync();
        }
        public async Task<int> SaveTimerBreak(TimerBreak item)
        {
            await CreateTableTimerBreak();

            await Database.ExecuteAsync("DELETE FROM TimerBreak");

            return await Database.InsertAsync(item);

        }
        /////////////////////////////////////
        async Task CreateTableTimerExercise()
        {
            var result = await Database.CreateTableAsync<TimerExercise>();
        }
        public async Task<List<TimerExercise>> GetTimerExerciseList()
        {
            await CreateTableTimerExercise();
            return await Database.Table<TimerExercise>().ToListAsync();
        }
        public async Task<int> SaveTimerExercise(TimerExercise item)
        {
            await CreateTableTimerExercise();

            await Database.ExecuteAsync("DELETE FROM TimerExercise");

            return await Database.InsertAsync(item);

        }
        /////////////////////////////////////
        async Task CreateTableTimerSets()
        {
            var result = await Database.CreateTableAsync<TimerSets>();
        }
        public async Task<List<TimerSets>> GetTimerSetsList()
        {
            await CreateTableTimerSets();
            return await Database.Table<TimerSets>().ToListAsync();
        }
        public async Task<int> SaveTimerSets(TimerSets item)
        {
            await CreateTableTimerSets();
            await Database.ExecuteAsync("DELETE FROM TimerSets");
            return await Database.InsertAsync(item);
        }
        /////////////////////////////////////
        async Task CreateTableDailyQuoteControl()
        {
            var result = await Database.CreateTableAsync<DailyQuoteControl>();
        }
        public async Task<List<DailyQuoteControl>> GetDailyQuoteControlList()
        {
            await CreateTableDailyQuoteControl();
            return await Database.Table<DailyQuoteControl>().ToListAsync();
        }
        public async Task<int> SaveDailyQuoteControl(DailyQuoteControl item)
        {
            await CreateTableDailyQuoteControl();
            return await Database.InsertAsync(item);
        }
        public async Task<int> DeleteDailyQuoteControl(string item)
        {
            await CreateTableDailyQuoteControl();
            string sql = "DELETE FROM DailyQuoteControl WHERE DailyQuote = ?"; 
            return await Database.ExecuteAsync(sql, item); 
        }
        /////////////////////////////////////
        async Task CreateTableDailyMealControl()
        {
            var result = await Database.CreateTableAsync<DailyMealControl>();
        }
        public async Task<List<DailyMealControl>> GetDailyMealControlList()
        {
            await CreateTableDailyMealControl();
            return await Database.Table<DailyMealControl>().ToListAsync();
        }
        public async Task<int> SaveDailyMealControl(DailyMealControl item)
        {
            await CreateTableDailyMealControl();
            return await Database.InsertAsync(item);
        }
        public async Task<int> DeleteDailyMealControl(DailyMealControl item)
        {
            await CreateTableDailyMealControl();
            return await Database.DeleteAsync(item);
        }
    }
}
