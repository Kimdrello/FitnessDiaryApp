using System;
using Newtonsoft.Json;
using FitnessDiary.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace FitnessDiary.Uwp.App.DataSource.Exercises
{
    /// <summary>
    /// 
    /// </summary>
    public class Exercises
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Exercises Instance { get; } = new Exercises();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string baseUri = "https://wger.de/api/v2/";

        /// <summary>
        /// The client
        /// </summary>
        HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="Exercises"/> class from being created.
        /// </summary>
        private Exercises()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        /// <summary>
        /// Gets the exercises count.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public async Task<int> GetExercisesCount(string parameter)
        {
            var json = await _client.GetStringAsync("exercise/?category=" + parameter + "&language=2").ConfigureAwait(false);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
            return rootObject.Count;
        }

        /// <summary>
        /// Gets the exercises.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public async Task<Exercise[]> GetExercises(string parameter)
        {
            var json = await _client.GetStringAsync("exercise/?category=" + parameter + "&language=2").ConfigureAwait(false);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
            List<Exercise> exerciseList = new List<Exercise>();

            foreach (var res in rootObject.Results)
            {
                string titlename = res.Name;
                if(res.Name == "") titlename = "No title.";

                Exercise exercise = new Exercise
                {
                    Title = titlename,
                    Directions = res.Description,
                    Source = res.LicenseAuthor,
                    BodytypeID = res.Category,
                    ExerciseID = res.Uuid,
                    //TODO: Add proper parcing to the returned string from the API instead of Datetime.Today
                    CreationDate = DateTime.Today
                };
                exerciseList.Add(exercise);           
            }
            if(rootObject.Next != null)
            {
                int counter = 2;
                Boolean rootObjectHasNext = true;
                while(rootObjectHasNext)
                {
                    var jsonNext = await _client.GetStringAsync("exercise/?category=" + parameter + "&page=" + counter + "&language=2").ConfigureAwait(false);
                    RootObject rootObjectNext = JsonConvert.DeserializeObject<RootObject>(jsonNext);

                    foreach (var res in rootObjectNext.Results)
                    {
                        string titlename = res.Name;
                        if (res.Name == "") titlename = "No title.";

                        Exercise exercise = new Exercise
                        {
                            Title = titlename,
                            Directions = res.Description,
                            Source = res.LicenseAuthor,
                            BodytypeID = res.Category,
                            ExerciseID = res.Uuid,
                            //TODO: Add proper parcing to the returned string from the API instead of Datetime.Today
                            CreationDate = DateTime.Today
                        };
                        exerciseList.Add(exercise);
                    }
                    if(rootObjectNext.Next != null) counter++;
                    else rootObjectHasNext = false;
                }              
            }
            return exerciseList.ToArray();
        }
    }
}
