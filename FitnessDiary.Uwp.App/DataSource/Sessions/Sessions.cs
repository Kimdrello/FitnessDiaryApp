using System;
using Newtonsoft.Json;
using FitnessDiary.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace FitnessDiary.Uwp.App.DataSource.Sessions
{
    /// <summary>
    /// 
    /// </summary>
    public class Sessions
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static Sessions Instance { get; } = new Sessions();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string baseUri = "http://localhost:51640/api/";

        /// <summary>
        /// The client
        /// </summary>
        HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="Sessions"/> class from being created.
        /// </summary>
        private Sessions()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        /// <summary>
        /// Gets the sessions.
        /// </summary>
        /// <returns></returns>
        /// CA1024, I think this should stay as it is
        public async Task<Session[]> GetSessions()
        {
            var json = await _client.GetStringAsync("sessions").ConfigureAwait(false);
            Session[] sessions = JsonConvert.DeserializeObject<Session[]>(json);
            return sessions;
        }

        /// <summary>
        /// Adds the session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public async Task<bool> AddSession(Session session)
        {
            string postBody = JsonConvert.SerializeObject(session);
            var response = await _client.PostAsync("sessions", new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }

        /// <summary>
        /// Deletes the session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public async Task<bool> DeleteSession(Session session)
        {
            var response = await _client.DeleteAsync($"sessions\\{session.SessionID}").ConfigureAwait(false);
            return response.IsSuccessStatusCode || response.StatusCode == System.Net.HttpStatusCode.NotFound;
        }

        /// <summary>
        /// Updates the session.
        /// </summary>
        /// <param name="session">The session.</param>
        /// <returns></returns>
        public async Task<bool> UpdateSession(Session session)
        {
            string postBody = JsonConvert.SerializeObject(session);
            var response = await _client.PutAsync(($"sessions\\{session.SessionID}"), new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
