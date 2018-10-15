using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;


namespace Luttop_2015
{
    public static class cari
    {
        public static object gelir(string tarih)
        {
            log.Bagla.Open();
            log.Adaptor = new MySqlDataAdapter("select (select sum(c_satisfiyat) as ürün from cikis where c_satistarih between 01 and 31 and (c_satistarih like '%" + "/" + tarih + "') ) +  ( select sum(s_ucret) as servis from servis where s_cikistarih between  01 and 31 and (s_cikistarih like '%" + "/" + tarih + "') ) as toplam_gelir  , (select sum(c_satisfiyat) from cikis where c_satistarih  between 01 and 31  and (c_satistarih like '%" + "/" + tarih + "')) as ürün , sum(s_ucret) as servis from servis where s_cikistarih between  01 and 31 and (s_cikistarih like '%" + "/" + tarih + "')", log.Bagla);
            log.Ds = new DataSet(); 
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            log.Bagla.Close();
            return log.Bs;
        }
        public static object gider(string tarih)
        {
            log.Bagla.Open();
            log.Adaptor = new MySqlDataAdapter("select (select sum(p_maas) from personel ) + (select sum(u_alisfiyati)from urun where u_alistarihi between 01 and 31 and (u_alistarihi like '%" + "/" + tarih + "')) +(select sum(y_maas) from yonetici ) , (select sum(p_maas) from personel ),(select sum(u_alisfiyati)from urun where u_alistarihi between 01 and 31 and (u_alistarihi like '%" + "/" + tarih + "') ) ,(select sum(y_maas) from yonetici )  ", log.Bagla);
            log.Ds = new DataSet();
            log.Bs = new BindingSource();
            log.Adaptor.Fill(log.Ds, "veri");
            log.Bs.DataSource = log.Ds.Tables["veri"];
            log.Bagla.Close();
            return log.Bs;
        }
    
    }
    public static class log
    {
        public static MySqlConnection Bagla = new MySqlConnection("server=localhost; database=luttop; user=root; password=; charset=latin5; pooling = false; convert zero datetime=True");
        public static MySqlDataAdapter Adaptor;
        public static MySqlCommand komut;
        public static DataSet Ds;
        public static BindingSource Bs;
        
        static string kayit = "LOG BAŞLANGIC";
        static string yol = @"C:\Users\Levent\Documents\log.txt";

        

        private static void logislemi(string islem)
        {   string tarih = DateTime.Now.ToString();
            StreamWriter SW = File.AppendText(yol);
            SW.WriteLine("LOG :[" + tarih + islem);
            SW.Close();
        }


        public static void log_giris(string ad,string soyad,string ID)
        {
            if (!File.Exists(yol))
            {
                File.Create(yol);
                kayit = "] GİRİŞ YAPILDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad;
                logislemi(kayit);
            }
            else
            {
                kayit = "] GİRİŞ YAPILDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad;
                logislemi(kayit);
            }
    
        }

        public static void urun_ekleme(string ad, string soyad, string ID, string u_kod)
        {
            kayit = "] ÜRÜN EKLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad +" ÜRÜN KODU:"+ u_kod;
            logislemi(kayit);
        }
        public static void urun_duzenle(string ad, string soyad, string ID, string u_ID)
        {
            kayit = "] ÜRÜN DÜZENLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ÜRÜN ID:" + u_ID;
            logislemi(kayit);
        }
        public static void urun_arama(string ad, string soyad, string ID, string arama)
        {
            kayit = "] ÜRÜN ARANDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ARANAN KELİME:" + arama;
            logislemi(kayit);
        }
        public static void urun_silme(string ad, string soyad, string ID, string u_ID)
        {
            kayit = "] ÜRÜN SİLİNDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ÜRÜN ID:" + u_ID;
            logislemi(kayit);
        }
        public static void urun_satis_duzenle(string ad, string soyad, string ID, string u_ID)
        {
            kayit = "] SATILAN ÜRÜN DÜZENLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ÜRÜN ID:" + u_ID;
            logislemi(kayit);
        }
        public static void urun_satis_arama(string ad, string soyad, string ID, string arama)
        {
            kayit = "] SATILAN ÜRÜN ARANDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ARANAN KELİME:" + arama;
            logislemi(kayit);
        }
        public static void urun_satis_tamamla(string ad, string soyad, string ID, string u_ID)
        {
            kayit = "] ÜRÜN SATILDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ÜRÜN ID:" + u_ID;
            logislemi(kayit);
        }
        public static void musteri_ekleme(string ad, string soyad, string ID, string m_mail)
        {
            kayit = "] MÜŞTERİ EKLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " MÜŞTERİ E-MAİL:" + m_mail;
            logislemi(kayit);
        }
        public static void musteri_duzenle(string ad, string soyad, string ID, string m_ID)
        {
            kayit = "] MÜŞTERİ DÜZENLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " MÜŞTERİ ID:" + m_ID;
            logislemi(kayit);
        }
        public static void musteri_arama(string ad, string soyad, string ID, string arama)
        {
            kayit = "] MÜŞTERİ ARANDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ARANAN KELİME:" + arama;
            logislemi(kayit);
        }
        public static void musteri_silme(string ad, string soyad, string ID, string m_ID)
        {
            kayit = "] MÜŞTERİ SİLİNDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " MÜŞTERİ ID:" + m_ID;
            logislemi(kayit);
        }
        public static void personel_ekleme(string ad, string soyad, string ID, string p_TC)
        {
            kayit = "] PERSONEL EKLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " PERSONEL TC:" + p_TC;
            logislemi(kayit);
        }
        public static void personel_duzenle(string ad, string soyad, string ID, string p_ID)
        {
            kayit = "] PERSONEL DÜZENLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " PERSONEL ID:" + p_ID;
            logislemi(kayit);
        }
        public static void personel_arama(string ad, string soyad, string ID, string arama)
        {
            kayit = "] PERSONEL ARANDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ARANAN KELİME:" + arama;
            logislemi(kayit);
        }
        public static void personel_silme(string ad, string soyad, string ID, string p_ID)
        {
            kayit = "] PERSONEL SİLİNDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " PERSONEL ID:" + p_ID;
            logislemi(kayit);
        }
        public static void servis_kayit(string ad, string soyad, string ID, string u_kod)
        {
            kayit = "] SERİVS ÜRÜN KAYIT -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ÜRÜN KODU:" + u_kod;
            logislemi(kayit);
        }
        public static void servis_duzenle(string ad, string soyad, string ID, string s_ID)
        {
            kayit = "] SERİVS ÜRÜN DÜZENLENDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " SERVİS ID:" + s_ID;
            logislemi(kayit);
        }
        public static void servis_arama(string ad, string soyad, string ID, string arama)
        {
            kayit = "] SERİVS ÜRÜN ARANDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " ARANAN KELİME:" + arama;
            logislemi(kayit);
        }
        public static void servis_silme(string ad, string soyad, string ID, string s_ID)
        {
            kayit = "] SERVİS ÜRÜN SİLİNDİ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " SERVİS ID:" + s_ID;
            logislemi(kayit);
        }
        public static void servis_cikis(string ad, string soyad, string ID, string s_ID)
        {
            kayit = "] SERVİS ÜRÜN ÇIKIŞ -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad + " SERVİS ID:" + s_ID;
            logislemi(kayit);
        }
        public static void cari_gelir(string ad, string soyad, string ID)
        {
            kayit = "] GELİR ARMASI YAPILDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad ;
            logislemi(kayit);
        }
        public static void cari_gider(string ad, string soyad, string ID)
        {
            kayit = "] GİDER ARAMASI YAPILDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad ;
            logislemi(kayit);
        }
        public static void log_cikis(string ad, string soyad, string ID)
        {
            kayit = "] ÇIKIŞ YAPILDI -> ID:" + ID + " AD:" + ad + " SOYAD:" + soyad;
            logislemi(kayit);
        }
    }


}
