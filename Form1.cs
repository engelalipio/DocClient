using System.Net;
using System.Net.Http.Headers;
using System.Web;
namespace DoceboClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void cmdGo_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();

            string strURL = string.Empty,
                   strClientID = string.Empty,
                   strClientSecret = string.Empty,
                   strGrantType = string.Empty,
                   strScope = string.Empty,
                   strUserName = string.Empty,
                   strPassword = string.Empty;


            try
            {
                if (string.IsNullOrEmpty(txtURL.Text))
                {
                    MessageBox.Show("Please enter URL");
                    return;
                }
                else
                {
                    strURL = txtURL.Text;
                    strClientID = txtClientID.Text;
                    strClientSecret = txtClientSecret.Text;
                    strGrantType = "password";
                    strScope = "api";
                    strUserName = "admin.user01";
                    strPassword = "Docebo&92";

                }

                /*
                 
                curl -X POST https://<yoursubdomain.docebosaas.com>/oauth2/token \
                   -F client_id=<your_client_id> \
                   -F client_secret=<your_client_secret> \
                   -F grant_type=password \
                   -F scope=api \
                   -F username=<your_username> \
                   -F password=<your_password>
                 
                 */

                // call web service

                /* 
                WebProxy proxy = new WebProxy();
                proxy.Address = new Uri("http://proxyserver:8080");
                proxy.Credentials = new NetworkCredential("username", "password");
                HttpClient.DefaultProxy = proxy;
                */

                client.BaseAddress = new Uri(strURL);
                // add post data                // add post data
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_API_KEY");
                var postData = new List<KeyValuePair<string, string>>();
                postData.Add(new KeyValuePair<string, string>("client_id", strClientID));
                postData.Add(new KeyValuePair<string, string>("client_secret", strClientSecret));
                postData.Add(new KeyValuePair<string, string>("grant_type", strGrantType));
                postData.Add(new KeyValuePair<string, string>("scope", strScope));
                postData.Add(new KeyValuePair<string, string>("username", strUserName));
                postData.Add(new KeyValuePair<string, string>("password", strPassword));
                FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage apiResponse = client.PostAsync(strURL, content).Result;
                if (apiResponse != null)
                {
                    var strResponse = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(strResponse))
                    {
                        Console.WriteLine(strResponse);
                        txtResponse.Text = strResponse;

                        strResponse.Split(new char[] { ',' }).ToList().ForEach(x =>
                        {
                            if (x.Contains("access_token"))
                            {
                                txtToken.Text = x.Split(new char[] { ':' })[1].Replace("\"", "");
                            }
                        });
                        /*
                         {
                            "access_token":"6e3635091e4473b418997f6ed37a174096cbe553",
                            "expires_in":3600,
                            "token_type":"Bearer",
                            "scope":"api",
                            "refresh_token":"ca74ad57679f251d1ed0969d76ea1c26868e843c"
                          }
                         */
                    }
                }
                else
                {
                    MessageBox.Show(apiResponse.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }





        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            var client = new HttpClient();

            string strURL  = "https://citisandbox2.docebosaas.com/learn/v1/catalog?all_catalogs=1",
                   strToken = string.Empty;


            try
            {
                if (string.IsNullOrEmpty(txtToken.Text))
                {
                    MessageBox.Show("Please enter a valid Token");
                    return;
                }
                
                strToken = txtToken.Text;

                client.BaseAddress = new Uri(strURL);
                // add post data                // add post data
                client.DefaultRequestHeaders.Accept.Clear();
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strToken);
                 
                HttpResponseMessage apiResponse = client.GetAsync(strURL).Result;
                if (apiResponse != null)
                {
                    var strResponse = apiResponse.Content.ReadAsStringAsync().Result;
                    if (!string.IsNullOrEmpty(strResponse))
                    {
                        Console.WriteLine(strResponse);
                        txtResponse.Text = strResponse;

                        /*
                         {
                            "access_token":"6e3635091e4473b418997f6ed37a174096cbe553",
                            "expires_in":3600,
                            "token_type":"Bearer",
                            "scope":"api",
                            "refresh_token":"ca74ad57679f251d1ed0969d76ea1c26868e843c"
                          }
                         */
                    }
                }
                else
                {
                    MessageBox.Show(apiResponse.StatusCode.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
