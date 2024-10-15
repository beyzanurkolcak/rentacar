using System;
using System.Collections.Generic;

namespace AracYonetimSistemi
{
    class Program
    {
        static void Main(string[] args)
        {
            AracYonetimi aracYonetimi = new AracYonetimi();
            while (true)
            {
                Console.WriteLine("--- Araç Yönetim Sistemi ---");
                Console.WriteLine("1. Araç Ekle");
                Console.WriteLine("2. Araçları Listele");
                Console.WriteLine("3. Araç Sil");
                Console.WriteLine("4. Araç Sat");
                Console.WriteLine("5. Araç Kirala");
                Console.WriteLine("6. Çıkış");
                Console.Write("Seçiminizi yapın: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        Console.Write("Araç Türü (Araba/Kamyonet): ");
                        string aracTuru = Console.ReadLine();
                        Console.Write("Marka: ");
                        string marka = Console.ReadLine();
                        Console.Write("Model: ");
                        string model = Console.ReadLine();
                        Console.Write("Fiyat: ");
                        decimal fiyat = decimal.Parse(Console.ReadLine());

                        aracYonetimi.AracEkle(new Arac(aracTuru, marka, model, fiyat));
                        Console.WriteLine("Araç başarıyla eklendi!\n");
                        break;

                    case "2":
                        aracYonetimi.AraclariListele();
                        break;

                    case "3":
                        Console.Write("Silmek istediğiniz aracın ID'sini girin: ");
                        int silinecekId = int.Parse(Console.ReadLine());
                        aracYonetimi.AracSil(silinecekId);
                        break;

                    case "4":
                        Console.Write("Satın almak istediğiniz aracın ID'sini girin: ");
                        int satId = int.Parse(Console.ReadLine());
                        Console.Write("İndirim yüzdelerinden birini seçin (10, 20, 30): ");
                        int indirimYuzdesi = int.Parse(Console.ReadLine());
                        aracYonetimi.AracSat(satId, indirimYuzdesi);
                        break;

                    case "5":
                        Console.Write("Kiralamak istediğiniz aracın ID'sini girin: ");
                        int kiraId = int.Parse(Console.ReadLine());
                        Console.Write("Kiralama süresi (gün): ");
                        int kiraSuresi = int.Parse(Console.ReadLine());
                        aracYonetimi.AracKirala(kiraId, kiraSuresi);
                        break;

                    case "6":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;

                    default:
                        Console.WriteLine("Geçersiz seçim, lütfen tekrar deneyin.\n");
                        break;
                }
            }
        }
    }

    class Arac
    {
        public int Id { get; }
        public string AracTuru { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public decimal Fiyat { get; set; }
        public static int IdSayaci = 1;

        public Arac(string aracTuru, string marka, string model, decimal fiyat)
        {
            Id = IdSayaci++;
            AracTuru = aracTuru;
            Marka = marka;
            Model = model;
            Fiyat = fiyat;
        }

        public override string ToString()
        {
            return $"ID: {Id}, Tür: {AracTuru}, Marka: {Marka}, Model: {Model}, Fiyat: {Fiyat:C}";
        }
    }

    class AracYonetimi
    {
        private List<Arac> araclar = new List<Arac>();

        public void AracEkle(Arac arac)
        {
            araclar.Add(arac);
        }

        public void AraclariListele()
        {
            if (araclar.Count == 0)
            {
                Console.WriteLine("Sistemde kayıtlı araç yok.\n");
            }
            else
            {
                Console.WriteLine("Sistemdeki Araçlar:");
                foreach (var arac in araclar)
                {
                    Console.WriteLine(arac.ToString());
                }
                Console.WriteLine();
            }
        }

        public void AracSil(int id)
        {
            var arac = araclar.Find(a => a.Id == id);

            if (arac != null)
            {
                araclar.Remove(arac);
                Console.WriteLine("Araç başarıyla silindi.\n");
            }
            else
            {
                Console.WriteLine("Girilen ID'ye sahip araç bulunamadı.\n");
            }
        }

        public void AracSat(int id, int indirimYuzdesi)
        {
            var arac = araclar.Find(a => a.Id == id);

            if (arac != null)
            {
                decimal indirimTutari = arac.Fiyat * indirimYuzdesi / 100;
                decimal sonFiyat = arac.Fiyat - indirimTutari;
                Console.WriteLine($"Araç başarıyla satıldı! İndirim: %{indirimYuzdesi}, Satış Fiyatı: {sonFiyat:C}\n");
                araclar.Remove(arac);
            }
            else
            {
                Console.WriteLine("Girilen ID'ye sahip araç bulunamadı.\n");
            }
        }

        public void AracKirala(int id, int kiraSuresi)
        {
            var arac = araclar.Find(a => a.Id == id);

            if (arac != null)
            {
                int indirimYuzdesi = kiraSuresi > 10 ? 20 : 10;
                decimal indirimTutari = arac.Fiyat * indirimYuzdesi / 100;
                decimal kiraTutari = arac.Fiyat - indirimTutari;
                Console.WriteLine($"Araç başarıyla kiralandı! Kiralama süresi: {kiraSuresi} gün, İndirim: %{indirimYuzdesi}, Kiralama Ücreti: {kiraTutari:C}\n");
            }
            else
            {
                Console.WriteLine("Girilen ID'ye sahip araç bulunamadı.\n");
            }
        }
    }
}
