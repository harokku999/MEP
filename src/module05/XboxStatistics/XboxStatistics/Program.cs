using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace XboxStatistics
{
    class Program
    {
        private static readonly MyXboxOneGames Xbox = new MyXboxOneGames();

        static void Main(string[] args)
        {
            Question("How many games do I have?", HowManyGamesDoIHave);
            Question("How many games have I completed?", HowManyGamesHaveICompleted);
            Question("How much Gamerscore do I have?", HowMuchGamescoreDoIHave);
            Question("How many days did I play?", HowManyDaysDidIPlay);
            Question("Which game have I spent the most hours playing?", WhichGameHaveISpentTheMostHoursPlaying);
            Question("In which game did I unlock my latest achievement?", InWhichGameDidIUnlockMyLatestAchievement);
            Question("List all of my statistics in Binding of Isaac:", ListAllOfMyStatisticsInBindingOfIsaac);
            Question("How many achievements did I earn per year?", HowManyAchievementsDidIEarnPerYear);
            Question("List all of my games where I have earned a rare achievement", ListAllOfMyGamesWhereIHaveEarnedARareAchievement);
            Question("List the top 3 games where I have earned the most rare achievements", ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements);
            Question("Which is my rarest achievement?", WhichIsMyRarestAchievement);

            Console.ReadLine();
        }

        static void Question(string question, Func<string> answer)
        {
            Console.WriteLine($"Q: {question}");
            Console.WriteLine($"A: {answer()}");
            Console.WriteLine();
        }

        static string HowManyGamesDoIHave()
        {
            return Xbox.MyGames.Count().ToString();
        }

        static string HowManyGamesHaveICompleted()
        {
            //HINT: you need to count the games where I reached the maximum Gamerscore
            return Xbox.MyGames.Count(g => g.CurrentGamerscore == g.MaxGamerscore).ToString();
        }

        public static bool Asdasdasd(int i)
        {
            return true;
        }

        public static IEnumerable<int> qwerqwe(int n)
        {
            for (var i = 0; i < n; i++)
            {
                yield return i;
            }
        }

        static string HowManyDaysDidIPlay()
        {
            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes

            Func<int, DateTime, int> asdasdasdasd = (kl, ret) =>
            {
                return 42;
            };


            

            Xbox.GameStats.Values.Sum(s =>
            {
                var minutesPlayedStat = s.SingleOrDefault(k => k.Name == "MinutesPlayed" && string.IsNullOrEmpty(k.Value));

                return minutesPlayedStat == null || !float.TryParse(minutesPlayedStat.Value, out var result)
                ? 0
                : result;
            });

            var q = new int[] { 1, 2, 3 };
            var asd = q.SelectMany(s => qwerqwe(s)).ToList();
            // Console.WriteLine(string.Join(", ", asd));

            

            // return Xbox.GameStats.Values.Sum(s => s.Single(ss => ss.Name == "MinutesPlayed").Select(ss => long.TryParse(ss.Value))).ToString();

            var e = new[] { "niki", "dani", "tody" };




            var totalMinutesPlayed = Xbox.GameStats.SelectMany(s => s.Value) // get all the stat items within the values
                                                   .Where(s => s.Name == "MinutesPlayed" && string.IsNullOrEmpty(s.Value) && float.TryParse(s.Value, out var _)) // filter out onlz the minutes played ones
                                                   .Select(s => float.Parse(s.Value)) // convert (for fun we could use cast as well...)
                                                   .Sum(); // aggregate the result


            var minsPlayed = Xbox.GameStats.SelectMany(s => s.Value).Where(s => s.Name == "MinutesPlayed").Sum(s => long.Parse(s.Value ?? "0"));

            var daysPlayed = minsPlayed / 1440;

            return $"{daysPlayed:F2}";
        }   

        static string WhichGameHaveISpentTheMostHoursPlaying()
        {

            //HINT: there's a game stat property called MinutesPlayed, and as the name suggests it stored total minutes
            var facGame = Xbox.GameStats.Join(Xbox.MyGames, kvp => kvp.Key, game => game.TitleId, (kvp, game) => new
            {
                GameId = kvp.Key,
                Title = game.Name,
                MinutesPlayed = int.Parse(kvp.Value.SingleOrDefault(s => s.Name == "MinutesPlayed" && s.Titleid != null)?.Value ?? "0")
            }).OrderByDescending(a => a.MinutesPlayed)
              .Take(1)
              .Single();

            return $"{facGame.Title} -> {facGame.MinutesPlayed / 60:F0} hours";
        }

        static string HowMuchGamescoreDoIHave()
        {
            return $"{Xbox.MyGames.Sum(g => g.CurrentGamerscore)}G";
        }

        static string InWhichGameDidIUnlockMyLatestAchievement()
        {
            var result = Xbox.MyGames.Join(Xbox.Achievements, g => g.TitleId, a => a.Key, (g, a) => new
            {
                Title = g.Name,
                LastAchievementEarned = a.Value.Where(k => k.ProgressState == "Achieved" && k.Progression != null)
                                               .Max(s => s.Progression.TimeUnlocked)
            }).OrderByDescending(l => l.LastAchievementEarned)
              .First();

            return $"{result.Title} on {result.LastAchievementEarned.ToString(CultureInfo.InvariantCulture)}";
        }

        static string ListAllOfMyStatisticsInBindingOfIsaac()
        {
            var isaacId = Xbox.MyGames.Single(gameTitle => gameTitle.Name.Contains("Binding of Isaac")).TitleId;
            return string.Join(Environment.NewLine, Xbox.GameStats.Single(stat => stat.Key == isaacId).Value
                                                                  .Select(s=> $"{s.Name} = {s.Value}"));
        }

        static string HowManyAchievementsDidIEarnPerYear()
        {
            //HINT: unlocked achievements have an "Achieved" progress state
            return string.Join(Environment.NewLine, Xbox.Achievements.SelectMany(a => a.Value)
                                                                     .Where(a => a.ProgressState == "Achieved")
                                                                     .GroupBy(a => a.Progression.TimeUnlocked.Year)
                                                                     .OrderBy(g => g.Key).Select(g => $"{g.Key}: {g.Count()}"));
        }

        static string ListAllOfMyGamesWhereIHaveEarnedARareAchievement()
        {
            //HINT: rare achievements have a rarity category called "Rare"
            var rareAchievementGameIds = Xbox.Achievements.Where(kvp => kvp.Value.Any(a => a.ProgressState == "Achieved" 
                                                                                        && a.Rarity.CurrentCategory == "Rare"))
                                                          .Select(kvp => kvp.Key);

            return string.Join(Environment.NewLine, Xbox.MyGames.Where(game => rareAchievementGameIds
                                                                .Any(id => id == game.TitleId)).Select(game => game.Name));
        }

        static string ListTheTop3GamesWhereIHaveEarnedTheMostRareAchievements()
        {
            return string.Join(Environment.NewLine, Xbox.Achievements.Join(Xbox.MyGames, achieKvp => achieKvp.Key, game => game.TitleId, (kvp, game) => new
                                                                      {
                                                                          Title = game.Name,
                                                                          RareAchievementCount = kvp.Value.Count(achie => achie.ProgressState == "Achieved" && achie.Rarity.CurrentCategory == "Rare")
                                                                      }).OrderByDescending(a => a.RareAchievementCount)
                                                                        .Take(3).Select(a => $"{a.Title} ({a.RareAchievementCount})"));
        }

        static string WhichIsMyRarestAchievement()
        {
            var rarestAchievement = Xbox.Achievements.SelectMany(kvp => kvp.Value)
                                                     .Where(achie => achie.ProgressState == "Achieved")
                                                     .OrderBy(achie => achie.Rarity.CurrentPercentage)
                                                     .Select(achie => new {
                                                         achie.Name,
                                                         Percentage = achie.Rarity.CurrentPercentage,
                                                         GameTitle = achie.TitleAssociations[0].Name })
                                                     .First();
            return $"You are among the {rarestAchievement.Percentage}% of gamers who earned the \"{rarestAchievement.Name}\" achievement in {rarestAchievement.GameTitle}";
        }
    }
}