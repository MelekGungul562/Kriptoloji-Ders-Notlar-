using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
// System.Net.Mail yerine MailKit kullanacağız
// using System.Net.Mail; 
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;

namespace KRiptoloji_odev_deneme
{
    public partial class Form1 : Form
    {
        // Türkçe Alfabe Tanımlaması (29 Harf)
        const string ALFABE = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZ";
        const string DORT_KARE_ALFABE = "ABCÇDEFGĞHIİJKLMNOÖPRSŞTUÜVYZX";

        private Panel pnlHillMatris;
        private TextBox[,] txtHill = new TextBox[3, 3];
        private const int BUTON_NORMAL_Y = 150;
        private const int BUTON_HILL_Y = 192;
        
        // Kimlik Bilgileri
        const string EMAIL_ADDRESS = "tarcinlicorek964@gmail.com";
        const string APP_PASSWORD = "ddddwgyvfsdgdbow"; // 16 haneli uygulama şifresi (boşluksuz)

        public Form1()
        {
            InitializeComponent();
            // Güvenlik protokolü ayarı (Gmail bağlantısı için önemli)
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            HillMatrisAlaniOlustur();
        }

        private void HillMatrisAlaniOlustur()
        {
            pnlHillMatris = new Panel();
            pnlHillMatris.Name = "pnlHillMatris";
            pnlHillMatris.Location = new Point(484, 106);
            pnlHillMatris.Size = new Size(172, 80);
            pnlHillMatris.Visible = false;

            int[] varsayilan = { 6, 22, 5, 11, 16, 10, 19, 13, 21 };
            int index = 0;

            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    TextBox kutu = new TextBox();
                    kutu.Name = $"txtHill_{satir}_{sutun}";
                    kutu.Size = new Size(50, 22);
                    kutu.Location = new Point(sutun * 56, satir * 26);
                    kutu.TextAlign = HorizontalAlignment.Center;
                    kutu.Text = varsayilan[index++].ToString();
                    txtHill[satir, sutun] = kutu;
                    pnlHillMatris.Controls.Add(kutu);
                }
            }

            this.Controls.Add(pnlHillMatris);
            pnlHillMatris.BringToFront();
        }

        private string HillMatrisiMetneDonustur()
        {
            List<string> sayilar = new List<string>();

            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    sayilar.Add(txtHill[satir, sutun].Text.Trim());
                }
            }

            return string.Join(" ", sayilar);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Yöntemleri ekle
            cmbYontem.Items.Clear();
            cmbYontem.Items.Add("1. Kaydırmalı Şifreleme (Sezar)");
            cmbYontem.Items.Add("2. Doğrusal Şifreleme (Affine)");
            cmbYontem.Items.Add("3. Yer Değiştirme Şifreleme");
            cmbYontem.Items.Add("4. Sayı Anahtarlı Şifreleme");
            cmbYontem.Items.Add("5. Permütasyon Şifreleme");
            cmbYontem.Items.Add("6. Rota Şifreleme");
            cmbYontem.Items.Add("7. Zigzag Şifreleme");
            cmbYontem.Items.Add("8. Hill Şifreleme (3x3 Matris)");
            cmbYontem.Items.Add("9. Vigenere Şifreleme");
            cmbYontem.Items.Add("10. 4 Kare Matris Şifreleme");
            cmbYontem.SelectedIndex = 0;

            // Yöntem değiştiğinde anahtar alanlarını uygun şekilde göster.
            cmbYontem.SelectedIndexChanged += cmbYontem_SelectedIndexChanged;
            AnahtarAlanlariniGuncelle();
        }

        private void cmbYontem_SelectedIndexChanged(object sender, EventArgs e)
        {
            AnahtarAlanlariniGuncelle();
        }

        private void AnahtarAlanlariniGuncelle()
        {
            bool hillSecili = cmbYontem.SelectedIndex == 7;
            bool vigenereSecili = cmbYontem.SelectedIndex == 8;
            bool dortKareSecili = cmbYontem.SelectedIndex == 9;

            if (hillSecili)
            {
                label3.Text = "Anahtar Matris (3x3):";
                txtAnahtar1.Visible = false;
                lblAnahtar2.Visible = false;
                txtAnahtar2.Visible = false;
                pnlHillMatris.Visible = true;
                btnSifrele.Location = new Point(btnSifrele.Location.X, BUTON_HILL_Y);
                btnCoz.Location = new Point(btnCoz.Location.X, BUTON_HILL_Y);
            }
            else if (vigenereSecili)
            {
                label3.Text = "Anahtar Kelime:";
                txtAnahtar1.Visible = true;
                lblAnahtar2.Visible = false;
                txtAnahtar2.Visible = false;
                pnlHillMatris.Visible = false;
                btnSifrele.Location = new Point(btnSifrele.Location.X, BUTON_NORMAL_Y);
                btnCoz.Location = new Point(btnCoz.Location.X, BUTON_NORMAL_Y);
            }
            else if (dortKareSecili)
            {
                label3.Text = "4 Kare Matris: Görseldeki sabit örnek";
                txtAnahtar1.Visible = false;
                lblAnahtar2.Visible = false;
                txtAnahtar2.Visible = false;
                pnlHillMatris.Visible = false;
                btnSifrele.Location = new Point(btnSifrele.Location.X, BUTON_NORMAL_Y);
                btnCoz.Location = new Point(btnCoz.Location.X, BUTON_NORMAL_Y);
            }
            else
            {
                label3.Text = "Anahtar 1 / Kelime:";
                txtAnahtar1.Visible = true;
                lblAnahtar2.Visible = true;
                lblAnahtar2.Text = "Anahtar 2:";
                txtAnahtar2.Visible = true;
                pnlHillMatris.Visible = false;
                btnSifrele.Location = new Point(btnSifrele.Location.X, BUTON_NORMAL_Y);
                btnCoz.Location = new Point(btnCoz.Location.X, BUTON_NORMAL_Y);
            }
        }

        // --- YARDIMCI METOTLAR ---

        private string MetniTemizle(string hamMetin)
        {
            if (string.IsNullOrEmpty(hamMetin)) return "";
            string temiz = "";
            hamMetin = hamMetin.ToUpper(new System.Globalization.CultureInfo("tr-TR"));
            foreach (char c in hamMetin)
            {
                if (ALFABE.Contains(c)) temiz += c;
            }
            return temiz;
        }

        private int ModInverse(int a, int m)
        {
            for (int x = 1; x < m; x++)
            {
                if (((a % m) * (x % m)) % m == 1) return x;
            }
            return -1;
        }

        private int GCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        private int Mod29(int sayi)
        {
            int sonuc = sayi % 29;
            if (sonuc < 0) sonuc += 29;
            return sonuc;
        }

        private bool HillAnahtariniAyristir(string anahtarStr, out int[,] anahtarMatris)
        {
            anahtarMatris = new int[3, 3];

            // Kullanıcının yazdığı tüm sayıları tek tek topla.
            List<int> sayilar = new List<int>();
            char[] ayiricilar = new char[] { ' ', ';', ',', '\t', '\r', '\n', '|', '/' };
            string[] parcalar = anahtarStr.Split(ayiricilar, StringSplitOptions.RemoveEmptyEntries);

            foreach (string parca in parcalar)
            {
                if (int.TryParse(parca, out int deger))
                {
                    // Hill matrisi mod 29 üzerinde çalıştığı için elemanları modla.
                    sayilar.Add(Mod29(deger));
                }
            }

            // 3x3 matris için tam olarak 9 sayı gerekir.
            if (sayilar.Count != 9)
            {
                return false;
            }

            // Sayıları satır satır matrise yerleştir.
            int index = 0;
            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    anahtarMatris[satir, sutun] = sayilar[index++];
                }
            }

            return true;
        }

        private int[,] HillTersMatris(int[,] matris)
        {
            // 3x3 matrisin determinantını hesapla.
            int det =
                matris[0, 0] * (matris[1, 1] * matris[2, 2] - matris[1, 2] * matris[2, 1]) -
                matris[0, 1] * (matris[1, 0] * matris[2, 2] - matris[1, 2] * matris[2, 0]) +
                matris[0, 2] * (matris[1, 0] * matris[2, 1] - matris[1, 1] * matris[2, 0]);

            det = Mod29(det);

            // Determinantın 29 ile aralarında asal olması gerekir.
            if (det == 0 || GCD(det, 29) != 1)
            {
                return null;
            }

            // Determinantın mod 29 tersini al.
            int detTersi = ModInverse(det, 29);
            if (detTersi == -1)
            {
                return null;
            }

            // Cofactor matrisini hesapla.
            int[,] kofaktor = new int[3, 3];
            kofaktor[0, 0] = Mod29(matris[1, 1] * matris[2, 2] - matris[1, 2] * matris[2, 1]);
            kofaktor[0, 1] = Mod29(-(matris[1, 0] * matris[2, 2] - matris[1, 2] * matris[2, 0]));
            kofaktor[0, 2] = Mod29(matris[1, 0] * matris[2, 1] - matris[1, 1] * matris[2, 0]);
            kofaktor[1, 0] = Mod29(-(matris[0, 1] * matris[2, 2] - matris[0, 2] * matris[2, 1]));
            kofaktor[1, 1] = Mod29(matris[0, 0] * matris[2, 2] - matris[0, 2] * matris[2, 0]);
            kofaktor[1, 2] = Mod29(-(matris[0, 0] * matris[2, 1] - matris[0, 1] * matris[2, 0]));
            kofaktor[2, 0] = Mod29(matris[0, 1] * matris[1, 2] - matris[0, 2] * matris[1, 1]);
            kofaktor[2, 1] = Mod29(-(matris[0, 0] * matris[1, 2] - matris[0, 2] * matris[1, 0]));
            kofaktor[2, 2] = Mod29(matris[0, 0] * matris[1, 1] - matris[0, 1] * matris[1, 0]);

            // Ters matris için kofaktör matrisinin transpozunu al.
            int[,] tersMatris = new int[3, 3];
            for (int satir = 0; satir < 3; satir++)
            {
                for (int sutun = 0; sutun < 3; sutun++)
                {
                    tersMatris[satir, sutun] = Mod29(detTersi * kofaktor[sutun, satir]);
                }
            }

            return tersMatris;
        }

        // --- ALGORİTMALAR ---

        // 1. Kaydırmalı (Sezar)
        private string KaydirmaliSifrele(string metin, int k)
        {
            string sonuc = "";
            foreach (char c in metin)
            {
                int idx = ALFABE.IndexOf(c);
                sonuc += ALFABE[(idx + k) % 29];
            }
            return sonuc;
        }

        private string KaydirmaliCoz(string metin, int k)
        {
            string sonuc = "";
            foreach (char c in metin)
            {
                int idx = ALFABE.IndexOf(c);
                int yeniIdx = (idx - k) % 29;
                if (yeniIdx < 0) yeniIdx += 29;
                sonuc += ALFABE[yeniIdx];
            }
            return sonuc;
        }

        // 2. Doğrusal (Affine)
        private string DogrusalSifrele(string metin, int a, int b)
        {
            if (GCD(a, 29) != 1) return "HATA: Anahtar A (Anahtar 1) 29 ile aralarında asal olmalı!";
            string sonuc = "";
            foreach (char c in metin)
            {
                int x = ALFABE.IndexOf(c);
                int y = (a * x + b) % 29;
                sonuc += ALFABE[y];
            }
            return sonuc;
        }

        private string DogrusalCoz(string metin, int a, int b)
        {
            if (GCD(a, 29) != 1) return "HATA: Anahtar A geçersiz!";
            int a_inv = ModInverse(a, 29);
            string sonuc = "";
            foreach (char c in metin)
            {
                int y = ALFABE.IndexOf(c); 
                int val = (y - b) % 29;
                if (val < 0) val += 29;
                int x = (a_inv * val) % 29;
                sonuc += ALFABE[x];
            }
            return sonuc;
        }

        // 3. Yer Değiştirme (Basit Yer Değiştirme / Keyword Substitution)
        // Kullanıcı "Yer Değiştirme" diyerek genellikle harflerin karıştırıldığı bir şifrelemeyi kasteder.
        // Anahtar kelimedeki harfler başa gelir, sonra alfabenin kalan harfleri eklenir.        
        private string YerDegistirmeSifrele(string metin, string anahtar)
        {
            // 1. Anahtarı temizle ve tekrarlayan harfleri uçur
            anahtar = MetniTemizle(anahtar);
            if (string.IsNullOrEmpty(anahtar)) return metin; // Anahtar yoksa şifreleme

            string yeniAlfabe = "";
            // Anahtardaki harfleri ekle (zaten ekliyse ekleme)
            foreach (char c in anahtar) 
            {
                if (!yeniAlfabe.Contains(c)) yeniAlfabe += c;
            }
            
            // Alfabeden, anahtarda olmayan diğer harfleri sırasıyla ekle
            foreach (char c in ALFABE) 
            {
                if (!yeniAlfabe.Contains(c)) yeniAlfabe += c;
            }

            // Şifreleme: Düz metindeki her harfi bul, yeni alfabedeki karşılığını al
            string sonuc = "";
            foreach (char c in metin)
            {
                int idx = ALFABE.IndexOf(c);
                if (idx != -1)
                    sonuc += yeniAlfabe[idx];
                else
                    sonuc += c; // Alfabede yoksa aynen kalsın
            }
            return sonuc;
        }

        private string YerDegistirmeCoz(string metin, string anahtar)
        {
            // Şifreleme ile aynı mantıkla yeni alfabeyi oluştur
            anahtar = MetniTemizle(anahtar);
            if (string.IsNullOrEmpty(anahtar)) return metin;

            string yeniAlfabe = "";
            foreach (char c in anahtar) 
            {
                if (!yeniAlfabe.Contains(c)) yeniAlfabe += c;
            }
            foreach (char c in ALFABE) 
            {
                if (!yeniAlfabe.Contains(c)) yeniAlfabe += c;
            }

            // Çözme: Şifreli harfi yeni alfabede bul, standart alfabedeki karşılığını al
            string sonuc = "";
            foreach (char c in metin)
            {
                int idx = yeniAlfabe.IndexOf(c);
                if (idx != -1)
                    sonuc += ALFABE[idx];
                else
                    sonuc += c;
            }
            return sonuc;
        }

        // 4. Sayı Anahtarlı
        private string SayiAnahtarliSifrele(string metin, string anahtarStr)
        {
            List<int> keys = new List<int>();
            foreach(char c in anahtarStr) if(char.IsDigit(c)) keys.Add(int.Parse(c.ToString()));
            if (keys.Count == 0) return "HATA: Anahtar sadece sayılardan oluşmalı!";

            string sonuc = "";
            int keyIdx = 0;
            foreach (char c in metin)
            {
                int x = ALFABE.IndexOf(c);
                int k = keys[keyIdx % keys.Count];
                int y = (x + k) % 29;
                sonuc += ALFABE[y];
                keyIdx++;
            }
            return sonuc;
        }

        private string SayiAnahtarliCoz(string metin, string anahtarStr)
        {
            List<int> keys = new List<int>();
            foreach(char c in anahtarStr) if(char.IsDigit(c)) keys.Add(int.Parse(c.ToString()));
            if (keys.Count == 0) return "HATA: Anahtar sadece sayılardan oluşmalı!";

            string sonuc = "";
            int keyIdx = 0;
            foreach (char c in metin)
            {
                int y = ALFABE.IndexOf(c);
                int k = keys[keyIdx % keys.Count];
                int x = (y - k) % 29;
                if (x < 0) x += 29;
                sonuc += ALFABE[x];
                keyIdx++;
            }
            return sonuc;
        }

        // 5. Permütasyon
        private string PermutasyonSifrele(string metin, string anahtar)
        {
            anahtar = MetniTemizle(anahtar);
            int cols = anahtar.Length;
            if (cols == 0) return "HATA: Anahtar giriniz.";
            int rows = (int)Math.Ceiling((double)metin.Length / cols);
            
            char[,] grid = new char[rows, cols];
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                {
                    int idx = i * cols + j;
                    grid[i, j] = (idx < metin.Length) ? metin[idx] : 'X';
                }

            var sortedKey = anahtar.Select((c, i) => new { Char = c, Index = i }).OrderBy(x => x.Char).ToList();
            string sonuc = "";
            foreach (var item in sortedKey)
            {
                for (int r = 0; r < rows; r++) sonuc += grid[r, item.Index];
            }
            return sonuc;
        }

        private string PermutasyonCoz(string metin, string anahtar)
        {
            anahtar = MetniTemizle(anahtar);
            int cols = anahtar.Length;
            if (cols == 0) return "HATA: Anahtar giriniz.";
            int rows = metin.Length / cols;
            
            char[,] grid = new char[rows, cols];
            var sortedKey = anahtar.Select((c, i) => new { Char = c, Index = i }).OrderBy(x => x.Char).ToList();

            int currentIdx = 0;
            foreach (var item in sortedKey)
            {
                for (int r = 0; r < rows; r++)
                {
                    if (currentIdx < metin.Length) grid[r, item.Index] = metin[currentIdx++];
                }
            }

            string sonuc = "";
            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++) sonuc += grid[i, j];
            
            return sonuc.TrimEnd('X'); 
        }

        // 6. Rota Şifreleme (Spiral)
        private string RotaSifrele(string metin, int cols)
        {
             if(cols <= 0) return "HATA: Sütun sayısı (Anahtar 1) pozitif olmalı.";
             int rows = (int)Math.Ceiling((double)metin.Length / cols);
             char[,] grid = new char[rows, cols];

             for(int i=0; i<rows; i++)
                for(int j=0; j<cols; j++) {
                    int idx = i*cols + j;
                    grid[i,j] = (idx < metin.Length) ? metin[idx] : 'X';
                }
            
            string sonuc = "";
            int top = 0, bottom = rows - 1, left = 0, right = cols - 1;
            int direction = 0; 
            while (top <= bottom && left <= right) {
                if(direction == 0) {
                    for(int i=left; i<=right; i++) sonuc += grid[top, i];
                    top++;
                } else if(direction == 1) {
                    for(int i=top; i<=bottom; i++) sonuc += grid[i, right];
                    right--;
                } else if(direction == 2) {
                    for(int i=right; i>=left; i--) sonuc += grid[bottom, i];
                    bottom--;
                } else if(direction == 3) {
                    for(int i=bottom; i>=top; i--) sonuc += grid[i, left];
                    left++;
                }
                direction = (direction + 1) % 4;
            }
            return sonuc;
        }

        private string RotaCoz(string metin, int cols)
        {
             if(cols <= 0) return "HATA: Sütun sayısı pozitif olmalı.";
             int rows = (int)Math.Ceiling((double)metin.Length / cols);
             char[,] grid = new char[rows, cols];
             int idx = 0;
             int top = 0, bottom = rows - 1, left = 0, right = cols - 1;
             int direction = 0;

             while (top <= bottom && left <= right && idx < metin.Length) {
                if(direction == 0) {
                    for(int i=left; i<=right; i++) grid[top, i] = metin[idx++];
                    top++;
                } else if(direction == 1) {
                    for(int i=top; i<=bottom; i++) grid[i, right] = metin[idx++];
                    right--;
                } else if(direction == 2) {
                    for(int i=right; i>=left; i--) grid[bottom, i] = metin[idx++];
                    bottom--;
                } else if(direction == 3) {
                    for(int i=bottom; i>=top; i--) grid[i, left] = metin[idx++];
                    left++;
                }
                direction = (direction + 1) % 4;
            }

            string sonuc = "";
            for(int i=0; i<rows; i++)
                for(int j=0; j<cols; j++) sonuc += grid[i, j];
            
            return sonuc.TrimEnd('X');
        }

        // 7. Zigzag
        private string ZigzagSifrele(string metin, int rails)
        {
            if (rails < 2) return metin;
            List<char>[] fence = new List<char>[rails];
            for (int i = 0; i < rails; i++) fence[i] = new List<char>();

            int rail = 0;
            bool down = false;
            foreach (char c in metin)
            {
                fence[rail].Add(c);
                if (rail == 0 || rail == rails - 1) down = !down;
                rail += down ? 1 : -1;
            }
            string sonuc = "";
            foreach (var row in fence) foreach (char c in row) sonuc += c;
            return sonuc;
        }

        private string ZigzagCoz(string metin, int rails)
        {
            if (rails < 2) return metin;
            char[,] grid = new char[rails, metin.Length];
            for(int r=0; r<rails; r++) for(int c=0; c<metin.Length; c++) grid[r,c] = '\0';

            int rail = 0;
            bool down = false;
            for (int i = 0; i < metin.Length; i++)
            {
                 grid[rail, i] = '*';
                 if (rail == 0 || rail == rails - 1) down = !down;
                 rail += down ? 1 : -1;
            }

            int index = 0;
            for (int r = 0; r < rails; r++)
                for (int c = 0; c < metin.Length; c++)
                    if (grid[r, c] == '*' && index < metin.Length) grid[r, c] = metin[index++];

            string sonuc = "";
            rail = 0; down = false;
            for (int i = 0; i < metin.Length; i++)
            {
                sonuc += grid[rail, i];
                if (rail == 0 || rail == rails - 1) down = !down;
                rail += down ? 1 : -1;
            }
            return sonuc;
        }

        // 8. Hill (3x3 Matris)
        private string HillSifrele(string metin, string anahtarStr)
        {
            // 3x3 matris anahtarını ayrıştır.
            if (!HillAnahtariniAyristir(anahtarStr, out int[,] anahtarMatris))
            {
                return "HATA: Hill için 3x3 matris anahtarı giriniz. Örnek: 6 22 5;11 16 10;19 13 21";
            }

            // Metnin uzunluğunu 3'e tamamlamak için sonuna A ekle.
            while (metin.Length % 3 != 0)
            {
                metin += ALFABE[0];
            }

            StringBuilder sonuc = new StringBuilder();

            // Metni 3 harfli bloklar halinde işle.
            for (int i = 0; i < metin.Length; i += 3)
            {
                // Her harfi alfabe içindeki sayısal değerine çevir.
                int[] blok = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    blok[j] = ALFABE.IndexOf(metin[i + j]);
                }

                // Anahtar matris ile vektörü çarp.
                for (int satir = 0; satir < 3; satir++)
                {
                    int toplam = 0;
                    for (int sutun = 0; sutun < 3; sutun++)
                    {
                        toplam += anahtarMatris[satir, sutun] * blok[sutun];
                    }

                    // Sonucu tekrar harfe çevir ve ekle.
                    sonuc.Append(ALFABE[Mod29(toplam)]);
                }
            }

            return sonuc.ToString();
        }

        private string HillCoz(string metin, string anahtarStr)
        {
            // 3x3 matris anahtarını ayrıştır.
            if (!HillAnahtariniAyristir(anahtarStr, out int[,] anahtarMatris))
            {
                return "HATA: Hill için 3x3 matris anahtarı giriniz. Örnek: 6 22 5;11 16 10;19 13 21";
            }

            // Matrisin mod 29 tersini hesapla.
            int[,] tersMatris = HillTersMatris(anahtarMatris);
            if (tersMatris == null)
            {
                return "HATA: Anahtar matrisin mod 29 ters matrisi yok. Determinant 29 ile aralarında asal olmalı.";
            }

            // Şifreli metin 3 harfli bloklar halinde çözülür.
            if (metin.Length % 3 != 0)
            {
                return "HATA: Hill çözme için şifreli metin uzunluğu 3'ün katı olmalı.";
            }

            StringBuilder sonuc = new StringBuilder();

            // Her bloğu ters matris ile çarp.
            for (int i = 0; i < metin.Length; i += 3)
            {
                // Şifreli harfleri sayıya çevir.
                int[] blok = new int[3];
                for (int j = 0; j < 3; j++)
                {
                    blok[j] = ALFABE.IndexOf(metin[i + j]);
                }

                // Ters matris ile çarpıp orijinal harfleri üret.
                for (int satir = 0; satir < 3; satir++)
                {
                    int toplam = 0;
                    for (int sutun = 0; sutun < 3; sutun++)
                    {
                        toplam += tersMatris[satir, sutun] * blok[sutun];
                    }

                    sonuc.Append(ALFABE[Mod29(toplam)]);
                }
            }

            return sonuc.ToString();
        }

        // 9. Vigenere
        private string VigenereSifrele(string metin, string anahtar)
        {
            anahtar = MetniTemizle(anahtar);
            if (string.IsNullOrEmpty(anahtar)) return "HATA: Vigenere için anahtar kelime giriniz.";

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < metin.Length; i++)
            {
                int m = ALFABE.IndexOf(metin[i]);
                int k = ALFABE.IndexOf(anahtar[i % anahtar.Length]);
                sonuc.Append(ALFABE[(m + k) % ALFABE.Length]);
            }

            return sonuc.ToString();
        }

        private string VigenereCoz(string metin, string anahtar)
        {
            anahtar = MetniTemizle(anahtar);
            if (string.IsNullOrEmpty(anahtar)) return "HATA: Vigenere için anahtar kelime giriniz.";

            StringBuilder sonuc = new StringBuilder();
            for (int i = 0; i < metin.Length; i++)
            {
                int c = ALFABE.IndexOf(metin[i]);
                int k = ALFABE.IndexOf(anahtar[i % anahtar.Length]);
                sonuc.Append(ALFABE[Mod29(c - k)]);
            }

            return sonuc.ToString();
        }

        // 10. 4 Kare (Four-Square) Matris
        private char[,] DortKareMatrisiOlustur(string anahtar)
        {
            anahtar = MetniTemizle(anahtar);
            string karakterler = "";

            foreach (char c in anahtar)
            {
                if (DORT_KARE_ALFABE.Contains(c) && !karakterler.Contains(c))
                {
                    karakterler += c;
                }
            }

            foreach (char c in DORT_KARE_ALFABE)
            {
                if (!karakterler.Contains(c))
                {
                    karakterler += c;
                }
            }

            char[,] matris = new char[6, 5];
            int index = 0;
            for (int satir = 0; satir < 6; satir++)
            {
                for (int sutun = 0; sutun < 5; sutun++)
                {
                    matris[satir, sutun] = karakterler[index++];
                }
            }

            return matris;
        }

        private bool MatrisKonumuBul(char[,] matris, char harf, out int satir, out int sutun)
        {
            satir = -1;
            sutun = -1;
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matris[i, j] == harf)
                    {
                        satir = i;
                        sutun = j;
                        return true;
                    }
                }
            }
            return false;
        }

        private char[,] Standart6x5MatrisiOlustur()
        {
            char[,] matris = new char[6, 5];
            int index = 0;

            for (int satir = 0; satir < 6; satir++)
            {
                for (int sutun = 0; sutun < 5; sutun++)
                {
                    matris[satir, sutun] = DORT_KARE_ALFABE[index++];
                }
            }

            return matris;
        }

        private char[,] MatrisKopyala(char[,] kaynak)
        {
            char[,] hedef = new char[6, 5];
            for (int satir = 0; satir < 6; satir++)
            {
                for (int sutun = 0; sutun < 5; sutun++)
                {
                    hedef[satir, sutun] = kaynak[satir, sutun];
                }
            }
            return hedef;
        }

        private void MatriseKarakterYerlestir(char[,] matris, int hedefSatir, int hedefSutun, char karakter)
        {
            if (matris[hedefSatir, hedefSutun] == karakter)
            {
                return;
            }

            if (!MatrisKonumuBul(matris, karakter, out int mevcutSatir, out int mevcutSutun))
            {
                return;
            }

            char temp = matris[hedefSatir, hedefSutun];
            matris[hedefSatir, hedefSutun] = karakter;
            matris[mevcutSatir, mevcutSutun] = temp;
        }

        private void DortKareSabitMatrisleriHazirla(out char[,] standart, out char[,] solAlt, out char[,] sagUst)
        {
            standart = Standart6x5MatrisiOlustur();
            solAlt = MatrisKopyala(standart);
            sagUst = MatrisKopyala(standart);

            // Görseldeki örneğe göre sabit eşleşmeleri sağlayan yerleşimler.
            MatriseKarakterYerlestir(sagUst, 0, 4, 'V');
            MatriseKarakterYerlestir(sagUst, 3, 0, 'S');
            MatriseKarakterYerlestir(sagUst, 4, 1, 'H');
            MatriseKarakterYerlestir(sagUst, 4, 0, 'I');
            MatriseKarakterYerlestir(sagUst, 1, 0, 'Ü');

            MatriseKarakterYerlestir(solAlt, 1, 0, 'U');
            MatriseKarakterYerlestir(solAlt, 0, 3, 'N');
            MatriseKarakterYerlestir(solAlt, 4, 4, 'Ğ');
            MatriseKarakterYerlestir(solAlt, 0, 4, 'J');
            MatriseKarakterYerlestir(solAlt, 0, 1, 'T');
        }

        private string DortKareSifrele(string metin)
        {
            DortKareSabitMatrisleriHazirla(out char[,] standart, out char[,] solAlt, out char[,] sagUst);

            if (metin.Length % 2 != 0)
            {
                metin += 'X';
            }

            StringBuilder sonuc = new StringBuilder();

            for (int i = 0; i < metin.Length; i += 2)
            {
                char a = metin[i];
                char b = metin[i + 1];

                if (!MatrisKonumuBul(standart, a, out int aSatir, out int aSutun) ||
                    !MatrisKonumuBul(standart, b, out int bSatir, out int bSutun))
                {
                    return "HATA: 4 Kare için metinde geçersiz karakter var.";
                }

                sonuc.Append(sagUst[aSatir, bSutun]);
                sonuc.Append(solAlt[bSatir, aSutun]);
            }

            return sonuc.ToString();
        }

        private string DortKareCoz(string metin)
        {
            if (metin.Length % 2 != 0)
            {
                return "HATA: 4 Kare çözme için metin uzunluğu çift olmalı.";
            }

            DortKareSabitMatrisleriHazirla(out char[,] standart, out char[,] solAlt, out char[,] sagUst);

            StringBuilder sonuc = new StringBuilder();

            for (int i = 0; i < metin.Length; i += 2)
            {
                char c1 = metin[i];
                char c2 = metin[i + 1];

                if (!MatrisKonumuBul(sagUst, c1, out int c1Satir, out int c1Sutun) ||
                    !MatrisKonumuBul(solAlt, c2, out int c2Satir, out int c2Sutun))
                {
                    return "HATA: 4 Kare için metinde geçersiz karakter var.";
                }

                sonuc.Append(standart[c1Satir, c2Sutun]);
                sonuc.Append(standart[c2Satir, c1Sutun]);
            }

            return sonuc.ToString().TrimEnd('X');
        }

        // --- BUTON İŞLEMLERİ ---

        private void btnSifrele_Click(object sender, EventArgs e)
        {
            try
            {
                string metin = MetniTemizle(txtGiris.Text);
                string k1 = txtAnahtar1.Text;
                string k2 = txtAnahtar2.Text;
                string sonuc = "";

                if (string.IsNullOrEmpty(metin))
                {
                    MessageBox.Show("Lütfen şifrelenecek metni giriniz.");
                    return;
                }

                switch (cmbYontem.SelectedIndex)
                {
                    case 0: sonuc = KaydirmaliSifrele(metin, int.TryParse(k1, out int s1) ? s1 : 0); break;
                    case 1: sonuc = DogrusalSifrele(metin, int.TryParse(k1, out int a) ? a : 1, int.TryParse(k2, out int b) ? b : 0); break;
                    case 2: sonuc = YerDegistirmeSifrele(metin, k1); break;
                    case 3: sonuc = SayiAnahtarliSifrele(metin, k1); break;
                    case 4: sonuc = PermutasyonSifrele(metin, k1); break;
                    case 5: sonuc = RotaSifrele(metin, int.TryParse(k1, out int r) ? r : 1); break;
                    case 6: sonuc = ZigzagSifrele(metin, int.TryParse(k1, out int z) ? z : 1); break;
                    case 7: sonuc = HillSifrele(metin, HillMatrisiMetneDonustur()); break;
                    case 8: sonuc = VigenereSifrele(metin, k1); break;
                    case 9: sonuc = DortKareSifrele(metin); break;
                }
                txtCikti.Text = sonuc;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnCoz_Click(object sender, EventArgs e)
        {
            try
            {
                string metin = txtGiris.Text;
                // Çözme işlemi için metni temizlemiyoruz, çünkü kullanıcı şifreli metni yapıştırmış olabilir
                // Ancak yine de büyük harfe çevirmek faydalı olabilir
                metin = MetniTemizle(metin); 
                
                string k1 = txtAnahtar1.Text;
                string k2 = txtAnahtar2.Text;
                string sonuc = "";

                if (string.IsNullOrEmpty(metin))
                {
                     MessageBox.Show("Lütfen çözülecek metni giriniz.");
                     return;
                }

                switch (cmbYontem.SelectedIndex)
                {
                    case 0: sonuc = KaydirmaliCoz(metin, int.TryParse(k1, out int s1) ? s1 : 0); break;
                    case 1: sonuc = DogrusalCoz(metin, int.TryParse(k1, out int a) ? a : 1, int.TryParse(k2, out int b) ? b : 0); break;
                    case 2: sonuc = YerDegistirmeCoz(metin, k1); break;
                    case 3: sonuc = SayiAnahtarliCoz(metin, k1); break;
                    case 4: sonuc = PermutasyonCoz(metin, k1); break;
                    case 5: sonuc = RotaCoz(metin, int.TryParse(k1, out int r) ? r : 1); break;
                    case 6: sonuc = ZigzagCoz(metin, int.TryParse(k1, out int z) ? z : 1); break;
                    case 7: sonuc = HillCoz(metin, HillMatrisiMetneDonustur()); break;
                    case 8: sonuc = VigenereCoz(metin, k1); break;
                    case 9: sonuc = DortKareCoz(metin); break;
                }
                txtCikti.Text = sonuc;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnGonder_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. ADIM: Alıcı E-posta Adresi Kontrolü
                // Bu kısım "addresses parametresi boş olamaz" hatasını kesin olarak çözer.
                if (string.IsNullOrWhiteSpace(txtAliciEmail.Text))
                {
                    MessageBox.Show("Lütfen geçerli bir alıcı e-posta adresi giriniz.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtCikti.Text))
                {
                    MessageBox.Show("Gönderilecek şifreli metin bulunamadı. Önce şifreleme yapınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Kriptoloji Gönderici", EMAIL_ADDRESS));
                message.To.Add(new MailboxAddress("", txtAliciEmail.Text.Trim())); // Trim() boşlukları temizler
                
                // ÖDEV KURALI: Konu başlığı genel olmalı, ipucu içermemeli.
                message.Subject = "Sifreli Mesaj"; 

                // ÖDEV KURALI: Gövde SADECE şifreli metni içermeli.
                // "plain" formatı seçilerek HTML vb. eklenmesi engellenir.
                message.Body = new TextPart("plain")
                {
                    Text = txtCikti.Text
                };

                using (var client = new SmtpClient())
                {
                    // 587 portu ve StartTls modern güvenlik standartlarıdır
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    
                    // Kimlik doğrulama
                    client.Authenticate(EMAIL_ADDRESS, APP_PASSWORD);

                    client.Send(message);
                    client.Disconnect(true);
                }
                MessageBox.Show("Şifreli metin başarıyla gönderildi!");
            }
            catch (Exception ex) 
            { 
                MessageBox.Show("Gönderim Hatası: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            }
        }

        private void btnIndir_Click(object sender, EventArgs e)
        {
            try
            {
                using (var client = new ImapClient())
                {
                    // Gmail IMAP sunucusuna bağlan (Port 993, SSL/TLS)
                    client.Connect("imap.gmail.com", 993, true);
                    
                    // Kimlik doğrulama
                    client.Authenticate(EMAIL_ADDRESS, APP_PASSWORD);

                    var inbox = client.Inbox;
                    inbox.Open(FolderAccess.ReadOnly);

                    if (inbox.Count > 0)
                    {
                        // En son gelen e-postayı al (indeks 0'dan başlar, son mesaj Count - 1'dir)
                        var msg = inbox.GetMessage(inbox.Count - 1);
                        
                        // E-posta gövdesini al (Önce TextBody, yoksa HtmlBody)
                        string indirilenMetin = !string.IsNullOrEmpty(msg.TextBody) ? msg.TextBody : msg.HtmlBody;
                        
                        // Metindeki gereksiz boşlukları temizle
                        indirilenMetin = indirilenMetin.Trim();

                        // Şifreli metni giriş kutusuna (txtGiris) aktar ki "ÇÖZ" butonuna basınca çözebilsin
                        txtGiris.Text = indirilenMetin;

                        MessageBox.Show("Son e-posta indirildi ve Giriş kutusuna aktarıldı.\nKonu: " + msg.Subject, "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Gelen kutusu boş!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    client.Disconnect(true);
                }
            }
            catch (Exception ex) 
            { 
                 MessageBox.Show("E-posta Alma Hatası: " + ex.Message + "\nİnternet bağlantınızı kontrol edin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtGiris_TextChanged(object sender, EventArgs e)
        {
            // Metin değiştiğinde yapılacak işlemler (gerekirse)
        }
    }
}
