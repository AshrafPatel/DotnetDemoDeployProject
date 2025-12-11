using Contacts.Shared.DTOs;

namespace Contacts.UI.Services
{
    public class ContactsApiClient
    {
        private readonly HttpClient _http;
        private readonly IConfiguration _config;

        public ContactsApiClient(HttpClient http, IConfiguration config)
        {
            _http = http;
            _config = config;
        }

        private string BaseUrl => _config["Api:BaseUrl"];

        public async Task<List<ContactDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ContactDto>>($"{BaseUrl}/api/contacts");
        }

        public async Task<ContactDto?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<ContactDto>($"{BaseUrl}/api/contacts/{id}");
        }   

        public async Task<bool> CreateAsync(ContactDto contact)
        {
            var response = await _http.PostAsJsonAsync($"{BaseUrl}/api/contacts", contact);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteAsync(Guid id) {
            var response = await _http.DeleteAsync($"{BaseUrl}/api/contacts/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> UpdateAsync(ContactDto contact) {
            var response = await _http.PutAsJsonAsync($"{BaseUrl}/api/contacts/{contact.Id}", contact);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ContactDto>?> SearchAsync(string name)
        {
            return await _http.GetFromJsonAsync<List<ContactDto>>($"{BaseUrl}/api/contacts/{Uri.EscapeDataString(name)}");
        }
    }
}
