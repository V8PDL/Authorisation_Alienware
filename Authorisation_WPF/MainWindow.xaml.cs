using System;
using System.Collections.Generic;
using System.Windows;
using System.Net.Http;
using System.Threading.Tasks;
    
namespace Authorisation_WPF
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Timers.Timer timer;
        delegate void Try_Login_Delegate(object sender, RoutedEventArgs e);
        static int Prev_Index = 0;
        static double Prev_Multiplier = 0;

        private async Task<string> Log_inAsync(string username, string password)
        {
            string url = "https://eu.alienwarearena.com/login_check";
            string token = "wHTCo924ZlPDfL4TUdpCgKyef0aW1TaUSwVoR6VC4M4";
            string recaptha_token = "03AGdBq27IL8k0pLGYtN5g5YGA8SCeomYISU_v2V7CxE2hh08GLSexD0SxrBHDQGRruReZ4EmQnG0aEYyEqPR0_sb3y79Gdwo6peK28FAduAijfMuw0G_IsvVM909VTD3aBh2yecoD6_ooqX5kBrVSQVqNc8qNW5dWBDRAI35iF43_7w7mKAjgwVC8tpmBy8-YIQsteUnCB0LKuwFM55ea3bB9u_up9AzQaDnm5SufRPnCyMKa72weBjGvggPC6tsfMGzgR0FfTMw_kyUfYlQNYUgCatje81N122f1-C87APmJItIpn9uQz4DuEEro7srT-O17Eokz8gvH-rB8kg63eGloP46SGomsBFl3ZprsYKsAGXdvs5APme2l-eG2csW4hsmd9IQVpU74MFBG9MP2NJcyfdv1JNgcmweZFDt-VZ6QWZA-BJqUcxYK2qzVxkZey44NA2nKgRJw-CbILTYzigDUXFH7XCCzRg";
            string timezone = "Europe/Minsk";
            string status_url = "https://www.alienwarearena.com/api/v1/users/arp/status";

            Dictionary<string, string> headers = new Dictionary<string, string>()
            {
                { "authority", "eu.alienwarearena.com" },
                { "method", "POST" },
                { "path", "/login_check" },
                { "scheme", "https" },
                { "accept", "*/*" },
                { "accept-encoding", "gzip, deflate, br" },
                { "accept-language", "ru" },
                { "cookie", "__utma=116440082.1610556481.1606568580.1606568580.1606568580.1; __utmc=116440082; __utmz=116440082.1606568580.1.1.utmcsr=(direct)|utmccn=(direct)|utmcmd=(none); __utmt=1; _fbp=fb.1.1606568584309.9954624; rumCki=false; AMCVS_4DD80861515CAB990A490D45%40AdobeOrg=1; _cls_v=2f6f3360-5b83-4a87-b14d-58f2a6b4e987; _cls_s=2d7d08b0-65ff-4a70-873a-b41aa69866d2:0; cidlid=%3A%3A; s_dl=1; sessionTime=2020%2C10%2C28%2C16%2C3%2C8%2C417; s_cc=true; _gcl_au=1.1.131687863.1606568589; AMCV_4DD80861515CAB990A490D45%40AdobeOrg=1585540135%7CMCIDTS%7C18595%7CMCMID%7C78802428877414247844400406297686838792%7CMCAAMLH-1607173387%7C6%7CMCAAMB-1607173387%7CRKhpRz8krg2tLO6pguXWp5olkAcUniQYPHaMWWgdJ3xzPWQmdj0y%7CMCOPTOUT-1606575788s%7CNONE%7CMCAID%7CNONE%7CMCSYNCSOP%7C411-18602%7CvVersion%7C4.4.0; ipe_s=04d57d76-8490-1be3-6cd4-929a63e0ef0e; user_dob=1994-05-11; PHPSESSID=uj62514tstkvgg6m29fnblusis; gpv_pn=eu.alienwarearena.com%2Flogin; s_depth=5; __utmb=116440082.11.10.1606568580; s_hwp=null%7C%7Cnull%7C%7C28%3A11%3A2020%3A16%3A10%7C%7CN%7C%7CN%7C%7Cnull%7C%7C7%7C%7Cnull%7C%7Cnull%7C%7CN%7C%7Cnull%7C%7Cnull%7C%7Cnull; s_ppv=eu.alienwarearena.com%2Flogin%2C88%2C88%2C610; s_sq=dellglobalonline%3D%2526c.%2526a.%2526activitymap.%2526page%253Deu.alienwarearena.com%25252Flogin%2526link%253DLogin%2526region%253Dlogin-form%2526pageIDType%253D1%2526.activitymap%2526.a%2526.c%2526pid%253Deu.alienwarearena.com%25252Flogin%2526pidt%253D1%2526oid%253DLogin%2526oidt%253D3%2526ot%253DSUBMIT" },
                { "origin", "https://eu.alienwarearena.com" },
                { "referer", "https://eu.alienwarearena.com/login" },
                { "sec-fetch-dest", "empty" },
                { "sec-fetch-mode", "cors" },
                { "sec-fetch-site", "same-origin" },
                { "user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/87.0.4280.67 Safari/537.36 Edg/87.0.664.47" },
                { "x-requested-with", "XMLHttpRequest" }
            };
            Dictionary<string, string> values = new Dictionary<string, string>()
            {
                { "_username", username},
                { "_password", password},
                {"_recaptcha_token", recaptha_token },
                {"_token", token },
                {"timezone", timezone }
            };
            HttpClient h = new HttpClient();
            var content = new FormUrlEncodedContent(values);
            foreach (var header in headers)
                h.DefaultRequestHeaders.Add(header.Key, header.Value);
            await h.PostAsync(url, content);

            var response = await h.PostAsync(status_url, null);

            var status_string = await response.Content.ReadAsStringAsync();

            if (status_string.Contains("error"))
                return null;
            string status = "";
            int level_index = status_string.IndexOf("\"level\":") + "\"level\":".Length - 1;
            int points_index = status_string.IndexOf("\"points\":") + "\"points\":".Length - 1;
            int day_count_index = status_string.IndexOf("{\"current_day\":{\"count\":\"") +
                "{\"current_day\":{\"count\":\"".Length;
            if (level_index < 0 || points_index < 0 || day_count_index < 0)
                return null;
            status += status_string.Substring(
                level_index + 1, status_string.IndexOf(',', level_index) - 1 - level_index);
            status += " ";
            status += status_string.Substring(
                points_index + 1, status_string.IndexOf(',', points_index) - 1 - points_index);
            status += " ";
            status += status_string.Substring(
                day_count_index, status_string.IndexOf(',', day_count_index) - 1 - day_count_index);

            return status;
        }

        public MainWindow()
        {
            InitializeComponent();
        }
        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e) =>
            Dispatcher.Invoke(new Try_Login_Delegate(Auth_Button_Click), new object[] { null, null });

        private async void Auth_Button_Click(object sender, RoutedEventArgs e)
        {
            string username = Username_TextBox.Text;
            string password = Password_TextBox.Text;
            timer.Stop();
            Change_Enability(false);
            string result = await Log_inAsync(username, password);
            if (result != null)
            {
                Auth_Result_Label.Content = "Successfull";
                //Result_Textblock.Text = result;
                string[] results = result.Split(' ');
                Result_Textblock.Text = "Level: " + results[0] + Environment.NewLine;
                Result_Textblock.Text += "Points: " + results[1] + Environment.NewLine;
                Result_Textblock.Text += "Day count: " + results[2] + Environment.NewLine;
            }
            else
            {
                Auth_Result_Label.Content = "Unsuccessfull";
                Result_Textblock.Text = "";
            }

            Auth_Time_Label.Content = System.DateTime.Now.ToString("dd/MM HH:mm:ss");

            Change_Enability(true);

            Set_Timer();
            timer.Start();
        }

        private void Change_Enability(bool value)
        {
            Username_TextBox.IsEnabled = Password_TextBox.IsEnabled =
                Timeout_TextBox.IsEnabled = Auth_Button.IsEnabled = value;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new System.Timers.Timer();
            Prev_Index = 2;
            Prev_Multiplier = 3600000;
            timer.Interval = 12 * Prev_Multiplier;
            Set_Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Set_Timer()
        {
            double multiplier = 1;
            double.TryParse(Timeout_TextBox.Text, out double timeout);
            if (timeout > 0)
            {
                switch (Time_ComboBox.SelectedIndex)
                {
                    case 0:
                        multiplier = 1000;    
                        break;
                    case 1:
                        multiplier = 60000;
                        break;
                    case 2:
                        multiplier = 3600000;
                        break;
                    case 3:
                        multiplier = 86400000;
                        break;
                    default:
                        break;
                }

                Prev_Multiplier = multiplier;
                Prev_Index = Time_ComboBox.SelectedIndex;
                timer.Interval = timeout * multiplier;
            }
            Timeout_TextBox.Text = (timer.Interval / Prev_Multiplier).ToString();
            Time_ComboBox.SelectedIndex = Prev_Index;
        }
    }
}