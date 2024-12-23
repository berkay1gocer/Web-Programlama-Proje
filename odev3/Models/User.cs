namespace odev3.Models
{
    public class User
    {
        public int Id { get; set; }          // Kullanıcı ID'si
        public string Username { get; set; } // Kullanıcı adı
        public string Password { get; set; } // Şifre
        public string Role { get; set; } = "Musteri";   // Kullanıcı rolü

    }
}
