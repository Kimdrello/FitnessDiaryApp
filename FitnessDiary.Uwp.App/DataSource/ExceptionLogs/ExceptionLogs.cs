using System;
using Newtonsoft.Json;
using FitnessDiary.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace FitnessDiary.Uwp.App.DataSource.ExceptionLogs
{
    /// <summary>
    /// 
    /// </summary>
    public class ExceptionLogs
    {
        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static ExceptionLogs Instance { get; } = new ExceptionLogs();

        /// <summary>
        /// The base URI
        /// </summary>
        private const string baseUri = "http://localhost:51640/api/";

        /// <summary>
        /// The client
        /// </summary>
        HttpClient _client;

        /// <summary>
        /// Prevents a default instance of the <see cref="ExceptionLogs"/> class from being created.
        /// </summary>
        private ExceptionLogs()
        {
            _client = new HttpClient
            {
                BaseAddress = new Uri(baseUri)
            };
        }

        /// <summary>
        /// Adds the exception log.
        /// </summary>
        /// <param name="exceptionLogs">The exception logs.</param>
        /// <returns></returns>
        public async Task<bool> AddExceptionLog(ExceptionLog exceptionLogs)
        {
            string postBody = JsonConvert.SerializeObject(exceptionLogs);
            var response = await _client.PostAsync("exceptionLogs", new StringContent(postBody, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            return response.IsSuccessStatusCode;
        }
    }
}
