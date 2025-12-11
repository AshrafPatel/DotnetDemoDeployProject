using Contacts.Shared.DTOs;

namespace Contacts.UI.Services
{
    public class ContactsApiClient
    {
        private readonly HttpClient _http;

        public ContactsApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ContactDto>?> GetAllAsync()
        {
            return await _http.GetFromJsonAsync<List<ContactDto>>("https://localhost:5000/api/contacts");
        }

        public async Task<ContactDto?> GetByIdAsync(Guid id)
        {
            return await _http.GetFromJsonAsync<ContactDto>($"https://localhost:5000/api/contacts/{id}");
        }   

        public async Task<bool> CreateAsync(ContactDto contact)
        {
            var response = await _http.PostAsJsonAsync("https://localhost:5000/api/contacts", contact);
            return response.IsSuccessStatusCode;
        }

        public async Task DeleteAsync(Guid id) {
            var response = await _http.DeleteAsync($"https://localhost:5000/api/contacts/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> UpdateAsync(ContactDto contact) {
            var response = await _http.PutAsJsonAsync($"https://localhost:5000/api/contacts/{contact.Id}", contact);
            return response.IsSuccessStatusCode;
        }

        public async Task<List<ContactDto>?> SearchAsync(string name)
        {
            return await _http.GetFromJsonAsync<List<ContactDto>>($"https://localhost:5000/api/contacts/{Uri.EscapeDataString(name)}");
        }
    }
}
