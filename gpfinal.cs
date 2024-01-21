using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

class Uçak
{
    public string Model { get; set; }
    public string Marka { get; set; }
    public string SeriNo { get; set; }
    public int KoltukKapasitesi { get; set; }
}

class Lokasyon
{
    public string Ülke { get; set; }
    public string Şehir { get; set; }
    public string Havaalanı { get; set; }
    public bool Aktif { get; set; }
}

class Uçuş
{
    public Lokasyon KalkışYeri { get; set; }
    public Lokasyon VarışYeri { get; set; }
    public DateTime Saat { get; set; }
    public Uçak UçakBilgisi { get; set; }
}

class Rezervasyon
{
    public Uçuş UçuşBilgisi { get; set; }
    public string Ad { get; set; }
    public string Soyad { get; set; }
    public int Yaş { get; set; }
}

class Program
{
    static List<Uçak> uçakListesi = new List<Uçak>();
    static List<Lokasyon> lokasyonListesi = new List<Lokasyon>();
    static List<Uçuş> uçuşListesi = new List<Uçuş>();
    static List<Rezervasyon> rezervasyonListesi = new List<Rezervasyon>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("1. Uçak Ekle");
            Console.WriteLine("2. Lokasyon Ekle");
            Console.WriteLine("3. Uçuş Oluştur");
            Console.WriteLine("4. Rezervasyon Yap");
            Console.WriteLine("5. Çıkış");
            Console.Write("Seçiminiz: ");

            string secim = Console.ReadLine();

            switch (secim)
            {
                case "1":
                    UçakEkle();
                    break;
                case "2":
                    LokasyonEkle();
                    break;
                case "3":
                    UçuşOluştur();
                    break;
                case "4":
                    RezervasyonYap();
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Geçersiz seçim. Lütfen tekrar deneyin.");
                    break;
            }
        }
    }

    static void UçakEkle()
    {
        Uçak uçak = new Uçak();
        Console.Write("Uçak Modeli: ");
        uçak.Model = Console.ReadLine();
        Console.Write("Uçak Markası: ");
        uçak.Marka = Console.ReadLine();
        Console.Write("Uçak Seri No: ");
        uçak.SeriNo = Console.ReadLine();
        Console.Write("Koltuk Kapasitesi: ");
        uçak.KoltukKapasitesi = Convert.ToInt32(Console.ReadLine());

        uçakListesi.Add(uçak);
        Console.WriteLine("Uçak başarıyla eklendi.");
    }

    static void LokasyonEkle()
    {
        Lokasyon lokasyon = new Lokasyon();
        Console.Write("Ülke: ");
        lokasyon.Ülke = Console.ReadLine();
        Console.Write("Şehir: ");
        lokasyon.Şehir = Console.ReadLine();
        Console.Write("Havaalanı: ");
        lokasyon.Havaalanı = Console.ReadLine();
        Console.Write("Aktif mi? (true/false): ");
        lokasyon.Aktif = Convert.ToBoolean(Console.ReadLine());

        lokasyonListesi.Add(lokasyon);
        Console.WriteLine("Lokasyon başarıyla eklendi.");
    }

    static void UçuşOluştur()
    {
        Uçuş uçuş = new Uçuş();
        Console.WriteLine("Kalkış Yeri:");
        uçuş.KalkışYeri = LokasyonSeç();
        Console.WriteLine("Varış Yeri:");
        uçuş.VarışYeri = LokasyonSeç();
        Console.Write("Uçuş Saati (YYYY-MM-DD HH:mm): ");
        uçuş.Saat = DateTime.Parse(Console.ReadLine());
        Console.WriteLine("Uçak Bilgisi:");
        uçuş.UçakBilgisi = UçakSeç();

        uçuşListesi.Add(uçuş);
        Console.WriteLine("Uçuş başarıyla oluşturuldu.");
    }

    static void RezervasyonYap()
    {
        Rezervasyon rezervasyon = new Rezervasyon();
        Console.WriteLine("Uçuş Seç:");
        rezervasyon.UçuşBilgisi = UçuşSeç();
        Console.Write("Ad: ");
        rezervasyon.Ad = Console.ReadLine();
        Console.Write("Soyad: ");
        rezervasyon.Soyad = Console.ReadLine();
        Console.Write("Yaş: ");
        rezervasyon.Yaş = Convert.ToInt32(Console.ReadLine());

        if (rezervasyon.UçuşBilgisi.UçakBilgisi.KoltukKapasitesi > rezervasyonListesi.Count(r => r.UçuşBilgisi == rezervasyon.UçuşBilgisi))
        {
            rezervasyonListesi.Add(rezervasyon);
            Console.WriteLine("Rezervasyon başarıyla oluşturuldu.");
        }
        else
        {
            Console.WriteLine("Üzgünüz, uçak kapasitesi dolu.");
        }
    }

    static Lokasyon LokasyonSeç()
    {
        Console.WriteLine("Lokasyonları Listele:");
        for (int i = 0; i < lokasyonListesi.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {lokasyonListesi[i].Ülke} - {lokasyonListesi[i].Şehir} - {lokasyonListesi[i].Havaalanı} - Aktif: {lokasyonListesi[i].Aktif}");
        }

        Console.Write("Seçiminiz (1-{0}): ", lokasyonListesi.Count);
        int index = Convert.ToInt32(Console.ReadLine()) - 1;
        return lokasyonListesi[index];
    }

    static Uçak UçakSeç()
    {
        Console.WriteLine("Uçakları Listele:");
        for (int i = 0; i < uçakListesi.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {uçakListesi[i].Model} - {uçakListesi[i].Marka} - {uçakListesi[i].SeriNo} - Koltuk Kapasitesi: {uçakListesi[i].KoltukKapasitesi}");
        }

        Console.Write("Seçiminiz (1-{0}): ", uçakListesi.Count);
        int index = Convert.ToInt32(Console.ReadLine()) - 1;
        return uçakListesi[index];
    }

    static Uçuş UçuşSeç()
    {
        Console.WriteLine("Uçuşları Listele:");
        for (int i = 0; i < uçuşListesi.Count; i++)
        {
            Console.WriteLine($"{i + 1}. Kalkış: {uçuşListesi[i].KalkışYeri.Ülke} - {uçuşListesi[i].KalkışYeri.Şehir} - {uçuşListesi[i].KalkışYeri.Havaalanı}, " +
                              $"Varış: {uçuşListesi[i].VarışYeri.Ülke} - {uçuşListesi[i].VarışYeri.Şehir} - {uçuşListesi[i].VarışYeri.Havaalanı}, " +
                              $"Saat: {uçuşListesi[i].Saat.ToString("yyyy-MM-dd HH:mm")}, Uçak: {uçuşListesi[i].UçakBilgisi.Model}");
        }

        Console.Write("Seçiminiz (1-{0}): ", uçuşListesi.Count);
        int index = Convert.ToInt32(Console.ReadLine()) - 1;
        return uçuşListesi[index];
    }
}
