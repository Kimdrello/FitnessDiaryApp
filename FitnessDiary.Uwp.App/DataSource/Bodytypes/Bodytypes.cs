using System;
using Newtonsoft.Json;
using FitnessDiary.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Net.Http;

namespace FitnessDiary.Uwp.App.DataSource.Bodytypes
{
    /// <summary>
    /// 
    /// </summary>
    public class Bodytypes
    {

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Bodytypes Instance { get; } = new Bodytypes();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string baseUri = "https://wger.de/api/v2/";

        /// <summary>
        /// The client
        /// </summary>
        HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="Bodytypes"/> class from being created.
        /// </summary>
        private Bodytypes()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        /// <summary>
        /// Gets the bodytypes.
        /// </summary>
        /// <returns></returns>
        /// CA1024, I think it should stay as it is
        public async Task<Bodytype[]> GetBodytypes()
        {
            var json = await _client.GetStringAsync("exercisecategory").ConfigureAwait(false);
            RootObject rootObject = JsonConvert.DeserializeObject<RootObject>(json);
            List<Bodytype> bodytypeList = new List<Bodytype>();
            
            foreach(var res in rootObject.Results)
            {
                int exercisesCount = await Exercises.Exercises.Instance.GetExercisesCount(res.Id.ToString());
                Bodytype bodytype = new Bodytype
                {
                    Id = res.Id,
                    Name = res.Name,
                    ExerciseCount = exercisesCount
                };
                bodytypeList.Add(bodytype);
            }
            return bodytypeList.ToArray();
        }
    }
}
