using System;
using Newtonsoft.Json;
using FitnessDiary.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace FitnessDiary.Uwp.App.DataSource.Exercises
{
    /// <summary>
    /// 
    /// </summary>
    public class AddedExercises
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static AddedExercises Instance { get; } = new AddedExercises();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string baseUri = "http://localhost:51640/api/";

        /// <summary>
        /// The client
        /// </summary>
        HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="AddedExercises"/> class from being created.
        /// </summary>
        private AddedExercises()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        /// <summary>
        /// Gets the added exercises.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        public async Task<Exercise[]> GetAddedExercises(string parameter)
        {
            var json = await _client.GetStringAsync("exercises/?BodytypeID=" + parameter).ConfigureAwait(false);
            Exercise[] exercises = JsonConvert.DeserializeObject<Exercise[]>(json);
            return exercises;
        }

        /// <summary>
        /// Adds the exercise.
        /// </summary>
        /// <param name="exercise">The exercise.</param>
        /// <returns></returns>
        public async Task<bool> AddExercise(Exercise exercise)
        {
            string postBody = JsonConvert.SerializeObject(exercise);
            var response = await _client.PostAsync("exercises", new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes the exercise.
        /// </summary>
        /// <param name="exercise">The exercise.</param>
        /// <returns></returns>
        public async Task<bool> DeleteExercise(Exercise exercise)
        {
            var response = await _client.DeleteAsync($"exercises\\{exercise.ExerciseID}").ConfigureAwait(false);
            return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Updates the exercise.
        /// </summary>
        /// <param name="exercise">The exercise.</param>
        /// <returns></returns>
        public async Task<bool> UpdateExercise(Exercise exercise)
        {
            string postBody = JsonConvert.SerializeObject(exercise);
            var response = await _client.PutAsync(($"exercises\\{exercise.ExerciseID}"), new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
