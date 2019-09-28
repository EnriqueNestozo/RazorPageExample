using ListaCursos.Interfaces;
using ListaCursos.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ListaCursos.Providers
{
    public class WebApiCoursesProvider : ICoursesProvider
    {
        private readonly IHttpClientFactory httpClientFactory;

        public WebApiCoursesProvider(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public Task<(bool isSuccess, int? Id)> AddAsync(Course course)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Course>> GetAllAsync()
        {
            var client = httpClientFactory.CreateClient("coursesService");
            var response = await client.GetAsync("api/Courses");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ICollection<Course>>(content);
                return result;
            }
            return null;
        }

        public Task<Course> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Course>> SearchAsync(string search)
        {
            var client = httpClientFactory.CreateClient("coursesService");
            var response = await client.GetAsync($"api/Courses/Search/{search}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ICollection<Course>>(content);
                return result;
            }
            return null;
        }

        public async Task<bool> UpdateAsync(Course course)
        {
            var client = httpClientFactory.CreateClient("coursesService");
            var body = new StringContent( JsonConvert.SerializeObject(course));
            var response = await client.PutAsync($"api/Courses/{course.Id}",body);
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }
    }
}
