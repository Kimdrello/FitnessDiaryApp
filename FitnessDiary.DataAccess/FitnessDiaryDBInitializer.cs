using System;
using System.Data.Entity;
using FitnessDiary.Model;

namespace FitnessDiary.DataAccess
{
    /// <summary>
    /// 
    /// </summary>
    public class FitnessDiaryDBInitializer : DropCreateDatabaseIfModelChanges<FitnessDiaryContext>
    {
        protected override void Seed(FitnessDiaryContext context)
        {
            var exercise1 = new Exercise() { ExerciseID = Guid.NewGuid(), Title = "Benchpress", BodytypeID = 11, CreationDate = DateTime.Today, Directions = "Set your feet, Position yourself under the bar, Arch your back, Set your grip, Brace and unrack, Breathe in and lower the bar, Touch your chest, Push with leg drive.", Source = "Bodybuilding.com" };
            var exercise2 = new Exercise() { ExerciseID = Guid.NewGuid(), Title = "Biceps curls", BodytypeID = 8, CreationDate = DateTime.Today, Directions = "Stand up with your torso upright while holding a barbell at a shoulder-width grip. While holding the upper arms stationary, curl the weights forward while contracting the biceps as you breathe out.", Source = "Bodybuilding.com" };
            var session1 = new Session() { SessionID = Guid.NewGuid(), Date = DateTime.Today, ExerciseID = exercise1.ExerciseID, Sets = 3, Repetitions = 10, Notes = "Start with 10kg." };
            var session2 = new Session() { SessionID = Guid.NewGuid(), Date = DateTime.Today, ExerciseID = exercise2.ExerciseID, Sets = 5, Repetitions = 5, Notes = "Completed 4 out of 5 sets last week." };
            var exception = new ExceptionLog() { ExceptionID = Guid.NewGuid(), ExceptionMessage="Not really an Exception", ExceptionSource= "Not really an Exception", ExceptionType="Not really an Exception", DateOfException = DateTime.Today };

            context.Exercises.Add(exercise1);
            context.Exercises.Add(exercise2);
            context.Sessions.Add(session1);
            context.Sessions.Add(session2);
            context.ExceptionLogs.Add(exception);

            base.Seed(context);
        }
    }
}
