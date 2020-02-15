using Newtonsoft.Json;
using RecyclingAssistant.Earth911Servies.models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace RecyclingAssistant.Earth911Servies
{
    public class E911Service
    {
        private readonly string _APIKey = "c34c5ea725af8463";
        private readonly string _APIBase = "https://api.earth911.com/earth911.";
        private readonly HttpClient _client;

        public E911Service(HttpClient client)
        {
            this._client = client;
        }


        public async Task<List<MatDetails>> GetZipMaterialsAndLocations(string zipCode)
        {
            var programDetail = await this.GetZipLocationDetails(zipCode);

            var recycObjects = programDetail.result.Select(r => r.Value).SelectMany(v => v.materials).ToList();
            var uniqueObjects = recycObjects.GroupBy(m => m.material_id).ToList();

            var matDetailsOut = new List<MatDetails>();

            foreach (var materialGroup in uniqueObjects) {

                var detail = new MatDetails();
                detail.Material = materialGroup.First();
                detail.Programs = programDetail.result.Select(r => r.Value).Where(r => r.materials.Select(m => m.material_id).Contains(materialGroup.Key)).ToList();
                
                matDetailsOut.Add(detail);
            }

            return matDetailsOut;
        }

        public async Task<RequestResponceDictionary<RecycProgramDetails>> GetZipLocationDetails(string zipCode)
        {
            var programs = await this.GetProgramsForZip(zipCode);
            var programDetail = await this.GetProgramDetails(programs.result);

            return programDetail;
        }

        private async Task<RequestResponceDictionary<RecycProgramDetails>> GetProgramDetails(List<RecycProgram> Programs)
        {

            var ids = "";

            var index = -1;
            foreach (var lodId in Programs.Select(p => p.program_id))
            {
                index++;
                ids = ids + $"&program_id[{index}]={lodId}";
            }


            var url = @$"{_APIBase}getProgramDetails?api_key={_APIKey}{ids}";

            var httpResponse = await _client.GetAsync(url);

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve project zip code");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<RequestResponceDictionary<RecycProgramDetails>>(content);

            return location;

        }

        private async Task<RequestResponse<List<RecycProgram>>> GetProgramsForZip(string zip)
        {

            var locations = await this.GetZipLocation(zip);

            var httpResponse = await _client.GetAsync($"{_APIBase}searchPrograms?api_key={_APIKey}&latitude={locations.result.latitude}&longitude={locations.result.longitude}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve Programs");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var providers = JsonConvert.DeserializeObject<RequestResponse<List<RecycProgram>>>(content);

            return providers;
        }

        private async Task<RequestResponse<Location>> GetZipLocation(string zip)
        {

            var httpResponse = await _client.GetAsync($"{_APIBase}getPostalData?api_key={_APIKey}&country=us&postal_code={zip}");

            if (!httpResponse.IsSuccessStatusCode)
            {
                throw new Exception("Cannot retrieve project zip code");
            }

            var content = await httpResponse.Content.ReadAsStringAsync();
            var location = JsonConvert.DeserializeObject<RequestResponse<Location>>(content);

            return location;

        }
    }
}
