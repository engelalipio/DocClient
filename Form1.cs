using System.Net;
using System.Net.Http.Headers;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Text;

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

                string sClientID = string.Empty,
                       sClientSecret = string.Empty;

                switch (cboEnvironment.Text)
                {
                    case "DEV":
                        sClientID = _configuration["Docebo:DEV_ClientID"];
                        sClientSecret = _configuration["Docebo:DEV_ClientSecret"]; 
                        break;
                    case "UAT":
                        sClientID = _configuration["Docebo:UAT_ClientID"];
                        sClientSecret = _configuration["Docebo:UAT_ClientSecret"];
                        break;
                    case "PROD":
                        sClientID = _configuration["Docebo:PROD_ClientID"];
                        sClientSecret = _configuration["Docebo:PROD_ClientSecret"];
                        break;
                }

                string baseURL = txtBaseURL.Text,
                       apiURL =  txtToken.Text,
                       tokenURL = string.Format("{0}{1}", baseURL,apiURL);

                // Configure HTTP client
                _httpClient.BaseAddress = new Uri(tokenURL);
                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));

                var postData = new Dictionary<string, string>
                {
                    ["client_id"] = sClientID,
                    ["client_secret"] = sClientSecret,
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


                _httpClient.DefaultRequestHeaders.Accept.Clear();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", txtToken.Text);

                string sMethod = this.cboMethod.Text,
                       baseURL = txtBaseURL.Text,
                       apiURL = txtURL.Text,
                       fullURL = string.Format("{0}{1}", baseURL, apiURL);

                if (sMethod.Equals("GET"))
                {
                    var getURL = fullURL;
                    using var response = await _httpClient.GetAsync(getURL);

                    await HandleApiResponseAsync(response);
                }
 

                if (sMethod.Equals("POST") || sMethod.Equals("PUT")  || sMethod.Equals("DELETE")){

                    var postURL = fullURL;
                    _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var postData = new Dictionary<string, string>
                    {
                        ["tag"] = this.txtResponse.Text
                    };

                    using var jsonContent = new StringContent(
                        JsonSerializer.Serialize(postData),
                        Encoding.UTF8,"application/json");

                    if (sMethod.Equals("POST"))
                    {
                        using var response = await _httpClient.PostAsync(txtURL.Text, jsonContent);


                        if (!response.IsSuccessStatusCode)
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Error {response.StatusCode}: {errorContent}");
                            return;
                        }



                        var responseContent = await HandleApiResponseAsync(response);

                        // Parse token from response
                        var jsonDoc = JsonDocument.Parse(responseContent);

                        if (jsonDoc != null)
                        {
                            if (jsonDoc.RootElement.TryGetProperty("data", out JsonElement postElement))
                            {
                                this.txtResponse.Text = postElement.GetString();
                            }

                        }

                    }


                    if (sMethod.Equals("PUT")){
                       using var response = await _httpClient.PutAsync(txtURL.Text, jsonContent);


                        if (!response.IsSuccessStatusCode)
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Error {response.StatusCode}: {errorContent}");
                            return;
                        }



                        var responseContent = await HandleApiResponseAsync(response);

                        // Parse token from response
                        var jsonDoc = JsonDocument.Parse(responseContent);

                        if (jsonDoc != null)
                        {
                            if (jsonDoc.RootElement.TryGetProperty("data", out JsonElement postElement))
                            {
                                this.txtResponse.Text = postElement.GetString();
                            }

                        }

                    }

                    if (sMethod.Equals("DELETE"))
                    {
                        using var response = await _httpClient.DeleteAsync(txtURL.Text);


                        if (!response.IsSuccessStatusCode)
                        {
                            var errorContent = await response.Content.ReadAsStringAsync();
                            MessageBox.Show($"Error {response.StatusCode}: {errorContent}");
                            return;
                        }



                        var responseContent = await HandleApiResponseAsync(response);

                        // Parse token from response
                        var jsonDoc = JsonDocument.Parse(responseContent);

                        if (jsonDoc != null)
                        {
                            if (jsonDoc.RootElement.TryGetProperty("data", out JsonElement postElement))
                            {
                                this.txtResponse.Text = postElement.GetString();
                            }

                        }

                    }


                     

                }
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

        private void cboEnvironment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sBaseURL = "https://citi{0}.docebosaas.com/",
                   sClientID = "Docebo:{0}_ClientID",
                   sClientSecret = "Docebo:{0}_ClientSecret";

            switch (cboEnvironment.Text)
            {
                case "DEV":
                    sBaseURL = string.Format(sBaseURL, "sandbox2");
                    sClientID = string.Format(sClientID, "DEV");
                    sClientSecret = string.Format(sClientSecret, "DEV");
                    break;
                case "UAT":
                    sBaseURL = string.Format(sBaseURL, "uat");
                    sClientID = string.Format(sClientID, "UAT");
                    sClientSecret = string.Format(sClientSecret, "UAT");
                    break;
                case "PROD":
                    sBaseURL = sBaseURL.Replace("{0}", string.Empty);                     
                    sClientID = string.Format(sClientID, "UAT");
                    sClientSecret = string.Format(sClientSecret, "UAT");
                    break;
            }

            txtBaseURL .Text = sBaseURL;
            txtClientID.Text = _configuration[sClientID];   
            txtClientSecret.Text = _configuration[sClientSecret]; 

        }
    }
}
