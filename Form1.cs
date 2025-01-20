using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace DoceboClient
{
    public partial class Form1 : Form
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private const string API_SCOPE = "api";
        private const string GRANT_TYPE = "password";

        public Form1()
        {
            InitializeComponent();
            _httpClient = new HttpClient();

            // Load configuration from appsettings.json
            string sCurrentDirectory = Directory.GetCurrentDirectory();

            _configuration = new ConfigurationBuilder()
                .SetBasePath(sCurrentDirectory)
                .AddJsonFile("appsettings.json", optional: false)
                .Build();
        }

        private async Task<string> HandleApiResponseAsync(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"API request failed: {response.StatusCode} - {response.ReasonPhrase}");
            }

            var content = await response.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(content))
            {
                throw new InvalidOperationException("API returned empty response");
            }

            txtResponse.Text = content;
            return content;
        }

        private async void cmdGo_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtURL.Text))
                {
                    MessageBox.Show("Please enter URL", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Configure HTTP client
                _httpClient.BaseAddress = new Uri(txtURL.Text);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var postData = new Dictionary<string, string>
                {
                    ["client_id"] = _configuration["Docebo:ClientID"],
                    ["client_secret"] = _configuration["Docebo:ClientSecret"],
                    ["grant_type"] = GRANT_TYPE,
                    ["scope"] = API_SCOPE,
                    ["username"] = _configuration["Docebo:Username"],
                    ["password"] = _configuration["Docebo:Password"]
                };

                using var content = new FormUrlEncodedContent(postData);
                using var response = await _httpClient.PostAsync(txtURL.Text, content);

                var responseContent = await HandleApiResponseAsync(response);

                // Parse token from response
                var jsonDoc = JsonDocument.Parse(responseContent);
                if (jsonDoc.RootElement.TryGetProperty("access_token", out JsonElement accessTokenElement))
                {
                    txtToken.Text = accessTokenElement.GetString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Add proper logging here
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtToken.Text))
                {
                    MessageBox.Show("Please enter a valid Token", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var catalogUrl = _configuration["Docebo:CatalogUrl"];
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", txtToken.Text);

                using var response = await _httpClient.GetAsync(catalogUrl);
                await HandleApiResponseAsync(response);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Add proper logging here
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient?.Dispose();
            }
            base.Dispose(disposing);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
