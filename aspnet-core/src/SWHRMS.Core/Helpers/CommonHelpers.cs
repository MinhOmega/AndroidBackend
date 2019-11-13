using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace SWHRMS.Helpers
{
    public static class CommonHelpers
    {
        private static readonly Regex Pattern = new Regex(@"^\(?(086|096|097|098|032|033|034|035|036|037|038|039|091|094|088|083|084|085|081|082|089|090|093|070|079|077|076|078|092|056|058|099|059)\)?([0-9]{7})$", RegexOptions.Compiled);
        private static readonly string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random _rdm = new Random();
        /// <summary>
        /// Regex phone in Vietnam
        /// </summary>
        /// <param name="phone"></param>
        /// <returns>bool</returns>
        public static bool IsValidPhoneNumber(string strPhone)
        {
            if (string.IsNullOrEmpty(strPhone))
            {
                return false;
            }
            return Pattern.IsMatch(strPhone);
        }
        /// <summary>
        /// Rand dome
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static string RandomString(int digits = 10)
        {
            var stringChars = new char[digits];
            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = alphaNumeric[_rdm.Next(alphaNumeric.Length)];
            }
            var finalString = new String(stringChars);
            return finalString;
        }
        public static string CreateSlug(string text)
        {
            for (int i = 32; i < 48; i++)
            {

                text = text.Replace(((char)i).ToString(), " ");

            }
            //First to lower case
            text = text.ToLowerInvariant();
            //Replace spaces
            text = Regex.Replace(text, @"\s", "-", RegexOptions.Compiled);
            //Replace double occurences of - or _
            text = Regex.Replace(text, @"([-_]){2,}", "$1", RegexOptions.Compiled);
            Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

            string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

            return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }
        public static string StripHTML(string input, int maxlength = 30)
        {
            if (input == "" && input == null)
            {
                return "";
            }
            return SubText(Regex.Replace(input, "<[a-zA-Z/]*?>", String.Empty), 30);
        }
        public static string SubText(string input, int maxlength = 30)
        {
            if (input.Length <= maxlength)
            {
                return input;
            }
            else
            {
                return input.Substring(1, maxlength) + "...";
            }
        }
        public static string DateFormatToString(string date, string format = "yyyy-MM-ddTHH:mm:ss", string output = "dd/MM/yyyy")
        {
            try
            {
                if (string.IsNullOrEmpty(date))
                {
                    return "";
                }
                //2019-03-07 13:16:12.76 +07:00
                DateTime dt = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                return dt.ToString(output);
            }
            catch
            {
                return "";
            }
        }
        public static string DateFormat(DateTime CreationTime)
        {
            return CreationTime.ToString("dd/MM/yyyy HH:mm:ss");
        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
        public static string DateFormatToData(string date, string format = "dd/MM/yyyy", string formatOuput = "yyyy-MM-dd")
        {
            try
            {
                //2019-03-07 13:16:12.76 +07:00
                DateTime dt = DateTime.ParseExact(date, format, CultureInfo.InvariantCulture);
                return dt.ToString(formatOuput);
            }
            catch
            {
                return null;
            }
        }
        public static string RelativeTime(DateTime theDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            // var s1 = _localizationManager.GetString("TNFLoyalty", "NewTask");

            var ts = new TimeSpan(DateTime.Now.Ticks - theDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";

            if (delta < 2 * MINUTE)
                return "a minute ago";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " minutes ago";

            if (delta < 90 * MINUTE)
                return "an hour ago";

            if (delta < 24 * HOUR)
                return ts.Hours + " hours ago";

            if (delta < 48 * HOUR)
                return "yesterday";

            if (delta < 30 * DAY)
                return ts.Days + " days ago";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }

        public static string RelativeTimeVi(DateTime theDate)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            // var s1 = _localizationManager.GetString("TNFLoyalty", "NewTask");

            var ts = new TimeSpan(DateTime.Now.Ticks - theDate.Ticks);
            double delta = Math.Abs(ts.TotalSeconds);

            if (delta < 1 * MINUTE)
                return ts.Seconds == 1 ? "một giây trước" : ts.Seconds + " giây trước";

            if (delta < 2 * MINUTE)
                return "một phút trước";

            if (delta < 45 * MINUTE)
                return ts.Minutes + " phút trước";

            if (delta < 90 * MINUTE)
                return "một giờ trước";

            if (delta < 24 * HOUR)
                return ts.Hours + " giờ trước";

            if (delta < 48 * HOUR)
                return "hôm qua";

            if (delta < 30 * DAY)
                return ts.Days + " ngày trước";

            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "một tháng trước" : months + " tháng trước";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "một năm trước" : years + " năm trước";
            }
        }
    }
}
