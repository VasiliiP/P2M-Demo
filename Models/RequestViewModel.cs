using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace Play2Money.Models {
    public class RequestViewModel {
        const string APP_UID = "APP_UID";
        const string APP_KEY = "APP_KEY";

        [Required]
        [DisplayName ("Player Uid")]
        public string Id { get; set; }

        [Required]
        [DisplayName ("Points")]
        public int P { get; set; }

        [Required]
        [DisplayName ("GameUid")]
        public string G { get; set; }

        [Required]
        [DisplayName ("Hash")]
        public string H { get; set; }

        public bool IsValid => Validate ();

        private bool Validate () {
            if (!string.Equals (G, APP_UID))
                return false;

            var hash = GetMd5Hash (APP_KEY + ":" + P);

            return string.Equals (H, hash);
        }

        private static string GetMd5Hash (string input) {
            byte[] data = MD5.Create ().ComputeHash (Encoding.UTF8.GetBytes (input));
            StringBuilder sBuilder = new StringBuilder ();
            for (int i = 0; i < data.Length; i++) {
                sBuilder.Append (data[i].ToString ("x2"));
            }

            return sBuilder.ToString ();
        }
    }
}