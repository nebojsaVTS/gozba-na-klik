using GozbaNaKlik.API.Models;
using Microsoft.EntityFrameworkCore;

namespace GozbaNaKlik.API.Data.Seed.Sid;

public static class SidStreetsSeed
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // ŠID - SettlementId 1001
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 100001, Name = "Alekse Šantića", SettlementId = 1001 },
            new Street { Id = 100002, Name = "Arsenija Čarnojevića", SettlementId = 1001 },
            new Street { Id = 100003, Name = "Boračka", SettlementId = 1001 },
            new Street { Id = 100004, Name = "Boška Buhe", SettlementId = 1001 },
            new Street { Id = 100005, Name = "Bosutska", SettlementId = 1001 },
            new Street { Id = 100006, Name = "Braće Jerković", SettlementId = 1001 },
            new Street { Id = 100007, Name = "Braće Petrović", SettlementId = 1001 },
            new Street { Id = 100008, Name = "Branka Erića", SettlementId = 1001 },
            new Street { Id = 100009, Name = "Branka Radičevića", SettlementId = 1001 },
            new Street { Id = 100010, Name = "Čajkovska", SettlementId = 1001 },
            new Street { Id = 100011, Name = "Cankareva", SettlementId = 1001 },
            new Street { Id = 100012, Name = "Cara Dušana", SettlementId = 1001 },
            new Street { Id = 100013, Name = "Cara Lazara", SettlementId = 1001 },
            new Street { Id = 100014, Name = "Dr Dragutina Grčića", SettlementId = 1001 },
            new Street { Id = 100015, Name = "Dr Nikole Trumića", SettlementId = 1001 },
            new Street { Id = 100016, Name = "Đure Daničića", SettlementId = 1001 },
            new Street { Id = 100017, Name = "Đure Jakšića", SettlementId = 1001 },
            new Street { Id = 100018, Name = "Đure Kiša", SettlementId = 1001 },
            new Street { Id = 100019, Name = "Dušana Radovića", SettlementId = 1001 },
            new Street { Id = 100020, Name = "Filipa Višnjića", SettlementId = 1001 },
            new Street { Id = 100021, Name = "Fruškogorska", SettlementId = 1001 },
            new Street { Id = 100022, Name = "Ilije Bašičevića Bosilja", SettlementId = 1001 },
            new Street { Id = 100023, Name = "Ive Andrića", SettlementId = 1001 },
            new Street { Id = 100024, Name = "Ive Lole Ribara", SettlementId = 1001 },
            new Street { Id = 100025, Name = "Janka Čmelika", SettlementId = 1001 },
            new Street { Id = 100026, Name = "Janka Veselinovića", SettlementId = 1001 },
            new Street { Id = 100027, Name = "Jelene Ćetković", SettlementId = 1001 },
            new Street { Id = 100028, Name = "Jovana Dučića", SettlementId = 1001 },
            new Street { Id = 100029, Name = "Jozefa Marčoka", SettlementId = 1001 },
            new Street { Id = 100030, Name = "Julkin bunar", SettlementId = 1001 },
            new Street { Id = 100031, Name = "Karađorđeva", SettlementId = 1001 },
            new Street { Id = 100032, Name = "Kneza Miloša", SettlementId = 1001 },
            new Street { Id = 100033, Name = "Lazara Bibića", SettlementId = 1001 },
            new Street { Id = 100034, Name = "Laze Kostića", SettlementId = 1001 },
            new Street { Id = 100035, Name = "Lipovačka", SettlementId = 1001 },
            new Street { Id = 100036, Name = "Majke Jevrosime", SettlementId = 1001 },
            new Street { Id = 100037, Name = "Maksima Gorkog", SettlementId = 1001 },
            new Street { Id = 100038, Name = "Marka Oreškovića", SettlementId = 1001 },
            new Street { Id = 100039, Name = "Markov do", SettlementId = 1001 },
            new Street { Id = 100040, Name = "Masarikova", SettlementId = 1001 },
            new Street { Id = 100041, Name = "Matije Gupca", SettlementId = 1001 },
            new Street { Id = 100042, Name = "Mičurinova", SettlementId = 1001 },
            new Street { Id = 100043, Name = "Mihajla Pupina", SettlementId = 1001 },
            new Street { Id = 100044, Name = "Miloša Obilića", SettlementId = 1001 },
            new Street { Id = 100045, Name = "Mirka Bibića", SettlementId = 1001 },
            new Street { Id = 100046, Name = "Miroslava Antića", SettlementId = 1001 },
            new Street { Id = 100047, Name = "Mokranjčeva", SettlementId = 1001 },
            new Street { Id = 100048, Name = "Naselje istok", SettlementId = 1001 },
            new Street { Id = 100049, Name = "Naselje Jelice Stanivuković", SettlementId = 1001 },
            new Street { Id = 100050, Name = "Nikole Mikloša", SettlementId = 1001 },
            new Street { Id = 100051, Name = "Nikole Tesle", SettlementId = 1001 },
            new Street { Id = 100052, Name = "Nikole Vlaškog", SettlementId = 1001 },
            new Street { Id = 100053, Name = "Ognjena Price", SettlementId = 1001 },
            new Street { Id = 100054, Name = "Omladinska", SettlementId = 1001 },
            new Street { Id = 100055, Name = "Opaljenik", SettlementId = 1001 },
            new Street { Id = 100056, Name = "Opaljenik 2.", SettlementId = 1001 },
            new Street { Id = 100057, Name = "Paje Marganovića", SettlementId = 1001 },
            new Street { Id = 100058, Name = "Perice Stankovića", SettlementId = 1001 },
            new Street { Id = 100059, Name = "Petra Kočića", SettlementId = 1001 },
            new Street { Id = 100060, Name = "Pinkijeva", SettlementId = 1001 },
            new Street { Id = 100061, Name = "Proleterska", SettlementId = 1001 },
            new Street { Id = 100062, Name = "Prote Dušana Letića", SettlementId = 1001 },
            new Street { Id = 100063, Name = "Radnička", SettlementId = 1001 },
            new Street { Id = 100064, Name = "Šandorova", SettlementId = 1001 },
            new Street { Id = 100065, Name = "Save Kovačevića", SettlementId = 1001 },
            new Street { Id = 100066, Name = "Save Šumanovića", SettlementId = 1001 },
            new Street { Id = 100067, Name = "Savska", SettlementId = 1001 },
            new Street { Id = 100068, Name = "Selište", SettlementId = 1001 },
            new Street { Id = 100069, Name = "Selište 2.", SettlementId = 1001 },
            new Street { Id = 100070, Name = "Sime Tomovića", SettlementId = 1001 },
            new Street { Id = 100071, Name = "Simeona Piščevića", SettlementId = 1001 },
            new Street { Id = 100072, Name = "Školska", SettlementId = 1001 },
            new Street { Id = 100073, Name = "Slavka Merdanovića", SettlementId = 1001 },
            new Street { Id = 100074, Name = "Sremska", SettlementId = 1001 },
            new Street { Id = 100075, Name = "Sremskog fronta", SettlementId = 1001 },
            new Street { Id = 100076, Name = "Srete Slavujevića", SettlementId = 1001 },
            new Street { Id = 100077, Name = "Stevice Mandića", SettlementId = 1001 },
            new Street { Id = 100078, Name = "Svetog Save", SettlementId = 1001 },
            new Street { Id = 100079, Name = "Svetozara Markovića", SettlementId = 1001 },
            new Street { Id = 100080, Name = "Teodora Stanivukovića", SettlementId = 1001 },
            new Street { Id = 100081, Name = "Tolstojeva", SettlementId = 1001 },
            new Street { Id = 100082, Name = "Trg 29. novembra", SettlementId = 1001 },
            new Street { Id = 100083, Name = "Trg oslobođenja", SettlementId = 1001 },
            new Street { Id = 100084, Name = "Trg republike", SettlementId = 1001 },
            new Street { Id = 100085, Name = "Trg žrtava velikogospojinske racije", SettlementId = 1001 },
            new Street { Id = 100086, Name = "Vase Stajića", SettlementId = 1001 },
            new Street { Id = 100087, Name = "Veljka Paunovića", SettlementId = 1001 },
            new Street { Id = 100088, Name = "Veljka Vlahovića", SettlementId = 1001 },
            new Street { Id = 100089, Name = "Vladimira Nazora", SettlementId = 1001 },
            new Street { Id = 100090, Name = "Vojislava Ilića", SettlementId = 1001 },
            new Street { Id = 100091, Name = "Vojvođanska", SettlementId = 1001 },
            new Street { Id = 100092, Name = "Vojvođanskih brigada", SettlementId = 1001 },
            new Street { Id = 100093, Name = "Vojvode Stepe Stepanovića", SettlementId = 1001 },
            new Street { Id = 100094, Name = "Vuka Karadžića", SettlementId = 1001 },
            new Street { Id = 100095, Name = "Vukovarska", SettlementId = 1001 },
            new Street { Id = 100096, Name = "Zaselak Banovina", SettlementId = 1001 },
            new Street { Id = 100097, Name = "Zaselak Nastić salaš", SettlementId = 1001 },
            new Street { Id = 100098, Name = "Žikice Jovanovića Španca", SettlementId = 1001 },
            new Street { Id = 100099, Name = "Zlatka Šnajdera", SettlementId = 1001 },
            new Street { Id = 100100, Name = "Zmaj Jove Jovanovića", SettlementId = 1001 },
            new Street { Id = 100101, Name = "1. maja", SettlementId = 1001 },
            new Street { Id = 100102, Name = "2. sremskog odreda", SettlementId = 1001 },
            new Street { Id = 100103, Name = "6. decembra", SettlementId = 1001 },
            new Street { Id = 100104, Name = "7. jula", SettlementId = 1001 },
            new Street { Id = 100105, Name = "12. aprila", SettlementId = 1001 },
            new Street { Id = 100106, Name = "20. oktobra", SettlementId = 1001 }
        );

        // ERDEVIK - SettlementId 1002
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 101001, Name = "Blaževac", SettlementId = 1002 },
            new Street { Id = 101002, Name = "Bruje", SettlementId = 1002 },
            new Street { Id = 101003, Name = "Cara Dušana", SettlementId = 1002 },
            new Street { Id = 101004, Name = "Čmelikova", SettlementId = 1002 },
            new Street { Id = 101005, Name = "Đipša", SettlementId = 1002 },
            new Street { Id = 101006, Name = "Fruškogorska", SettlementId = 1002 },
            new Street { Id = 101007, Name = "Grobljanska", SettlementId = 1002 },
            new Street { Id = 101008, Name = "Karađorđeva", SettlementId = 1002 },
            new Street { Id = 101009, Name = "Kralja Petra I", SettlementId = 1002 },
            new Street { Id = 101010, Name = "Manastirska", SettlementId = 1002 },
            new Street { Id = 101011, Name = "Masarikova", SettlementId = 1002 },
            new Street { Id = 101012, Name = "Milanke Branković", SettlementId = 1002 },
            new Street { Id = 101013, Name = "Moharač", SettlementId = 1002 },
            new Street { Id = 101014, Name = "Nikole Tesle", SettlementId = 1002 },
            new Street { Id = 101015, Name = "Pinkijeva", SettlementId = 1002 },
            new Street { Id = 101016, Name = "Proleterska", SettlementId = 1002 },
            new Street { Id = 101017, Name = "Šidska", SettlementId = 1002 },
            new Street { Id = 101018, Name = "Sremska", SettlementId = 1002 },
            new Street { Id = 101019, Name = "Svetog Save", SettlementId = 1002 },
            new Street { Id = 101020, Name = "Vojvođanska", SettlementId = 1002 },
            new Street { Id = 101021, Name = "Zadružna", SettlementId = 1002 },
            new Street { Id = 101022, Name = "Zaselak Kišelez", SettlementId = 1002 },
            new Street { Id = 101023, Name = "Zaselak Vorovo", SettlementId = 1002 },
            new Street { Id = 101024, Name = "Živojina Mišića", SettlementId = 1002 }
        );

        // KUKUJEVCI - SettlementId 1003
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 102001, Name = "Bosutska", SettlementId = 1003 },
            new Street { Id = 102002, Name = "Cara Lazara", SettlementId = 1003 },
            new Street { Id = 102003, Name = "Desanke Maksimović", SettlementId = 1003 },
            new Street { Id = 102004, Name = "Fruškogorska", SettlementId = 1003 },
            new Street { Id = 102005, Name = "Grčkog prijateljstva", SettlementId = 1003 },
            new Street { Id = 102006, Name = "Miloša Obilića", SettlementId = 1003 },
            new Street { Id = 102007, Name = "Pašićeva", SettlementId = 1003 },
            new Street { Id = 102008, Name = "Republikanska", SettlementId = 1003 },
            new Street { Id = 102009, Name = "Sarajevska", SettlementId = 1003 },
            new Street { Id = 102010, Name = "Savska", SettlementId = 1003 },
            new Street { Id = 102011, Name = "Sremska", SettlementId = 1003 },
            new Street { Id = 102012, Name = "Srpskih vladara", SettlementId = 1003 },
            new Street { Id = 102013, Name = "Svetog Save", SettlementId = 1003 },
            new Street { Id = 102014, Name = "Vojvođanska", SettlementId = 1003 },
            new Street { Id = 102015, Name = "Vojvode Putnika", SettlementId = 1003 },
            new Street { Id = 102016, Name = "Vojvode Sinđelića", SettlementId = 1003 },
            new Street { Id = 102017, Name = "Vuka Karadžića", SettlementId = 1003 },
            new Street { Id = 102018, Name = "Zaselak Lazić", SettlementId = 1003 },
            new Street { Id = 102019, Name = "Zmaj Jovina", SettlementId = 1003 },
            new Street { Id = 102020, Name = "Železnička", SettlementId = 1003 }
        );

        // ADAŠEVCI - SettlementId 1004
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 103001, Name = "Bosutska", SettlementId = 1004 },
            new Street { Id = 103002, Name = "Branka Radičevića", SettlementId = 1004 },
            new Street { Id = 103003, Name = "Fruškogorska", SettlementId = 1004 },
            new Street { Id = 103004, Name = "Grobljanska", SettlementId = 1004 },
            new Street { Id = 103005, Name = "Nikole Tesle", SettlementId = 1004 },
            new Street { Id = 103006, Name = "Proleterska", SettlementId = 1004 },
            new Street { Id = 103007, Name = "Sremski front", SettlementId = 1004 },
            new Street { Id = 103008, Name = "Svetog Nikole", SettlementId = 1004 },
            new Street { Id = 103009, Name = "Svetosavska", SettlementId = 1004 },
            new Street { Id = 103010, Name = "Svetozara Markovića", SettlementId = 1004 },
            new Street { Id = 103011, Name = "Termovul", SettlementId = 1004 },
            new Street { Id = 103012, Name = "Zaselak Dubrave", SettlementId = 1004 },
            new Street { Id = 103013, Name = "Zaselak Verencije", SettlementId = 1004 },
            new Street { Id = 103014, Name = "Zaselak Vranjak", SettlementId = 1004 },
            new Street { Id = 103015, Name = "Železnička", SettlementId = 1004 }
        );

        // MOROVIĆ - SettlementId 1005
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 104001, Name = "Batrovačka", SettlementId = 1005 },
            new Street { Id = 104002, Name = "Bosutska", SettlementId = 1005 },
            new Street { Id = 104003, Name = "Ive Andrića", SettlementId = 1005 },
            new Street { Id = 104004, Name = "Kralja Petra I", SettlementId = 1005 },
            new Street { Id = 104005, Name = "Mitrovačka", SettlementId = 1005 },
            new Street { Id = 104006, Name = "Morović", SettlementId = 1005 },
            new Street { Id = 104007, Name = "Nikole Tesle", SettlementId = 1005 },
            new Street { Id = 104008, Name = "Omladinska", SettlementId = 1005 },
            new Street { Id = 104009, Name = "Proleterska", SettlementId = 1005 },
            new Street { Id = 104010, Name = "Šidska", SettlementId = 1005 },
            new Street { Id = 104011, Name = "Svetog Save", SettlementId = 1005 },
            new Street { Id = 104012, Name = "Vuka Karadžića", SettlementId = 1005 },
            new Street { Id = 104013, Name = "Zaselak Vojna ustanova", SettlementId = 1005 }
        );

        // VIŠNJIĆEVO - SettlementId 1006
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 105001, Name = "Bosutska", SettlementId = 1006 },
            new Street { Id = 105002, Name = "Bosutski sokak", SettlementId = 1006 },
            new Street { Id = 105003, Name = "Dubrava", SettlementId = 1006 },
            new Street { Id = 105004, Name = "Filipa Višnjića", SettlementId = 1006 },
            new Street { Id = 105005, Name = "Fruškogorska", SettlementId = 1006 },
            new Street { Id = 105006, Name = "Grobljanska", SettlementId = 1006 },
            new Street { Id = 105007, Name = "Lipovačka", SettlementId = 1006 },
            new Street { Id = 105008, Name = "Radnička", SettlementId = 1006 },
            new Street { Id = 105009, Name = "Železnička", SettlementId = 1006 }
        );

        // VAŠICA - SettlementId 1007
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 106001, Name = "Bosutska", SettlementId = 1007 },
            new Street { Id = 106002, Name = "Fruškogorska", SettlementId = 1007 },
            new Street { Id = 106003, Name = "Ilača", SettlementId = 1007 },
            new Street { Id = 106004, Name = "Kablarovac", SettlementId = 1007 },
            new Street { Id = 106005, Name = "Lenjinova", SettlementId = 1007 },
            new Street { Id = 106006, Name = "Nikole Tesle", SettlementId = 1007 },
            new Street { Id = 106007, Name = "Proleterska", SettlementId = 1007 },
            new Street { Id = 106008, Name = "Prvomajska", SettlementId = 1007 },
            new Street { Id = 106009, Name = "Vojvođanska", SettlementId = 1007 },
            new Street { Id = 106010, Name = "Vuka Karadžića", SettlementId = 1007 }
        );

        // BAČINCI - SettlementId 1008
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 107001, Name = "Bena", SettlementId = 1008 },
            new Street { Id = 107002, Name = "Branka Radičevića", SettlementId = 1008 },
            new Street { Id = 107003, Name = "Fruškogorska", SettlementId = 1008 },
            new Street { Id = 107004, Name = "Palilula", SettlementId = 1008 },
            new Street { Id = 107005, Name = "Partizanska", SettlementId = 1008 },
            new Street { Id = 107006, Name = "Rusinska", SettlementId = 1008 },
            new Street { Id = 107007, Name = "Salaši", SettlementId = 1008 },
            new Street { Id = 107008, Name = "Sremska", SettlementId = 1008 },
            new Street { Id = 107009, Name = "Vojvođanska", SettlementId = 1008 },
            new Street { Id = 107010, Name = "Vuka Karadžića", SettlementId = 1008 },
            new Street { Id = 107011, Name = "Železnička", SettlementId = 1008 },
            new Street { Id = 107012, Name = "Zmaj Jove Jovanovića", SettlementId = 1008 }
        );

        // BERKASOVO - SettlementId 1009
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 108001, Name = "Češće", SettlementId = 1009 },
            new Street { Id = 108002, Name = "Despotovac", SettlementId = 1009 },
            new Street { Id = 108003, Name = "Fruškogorska", SettlementId = 1009 },
            new Street { Id = 108004, Name = "Livade", SettlementId = 1009 },
            new Street { Id = 108005, Name = "Manastirska", SettlementId = 1009 },
            new Street { Id = 108006, Name = "Partizanska", SettlementId = 1009 },
            new Street { Id = 108007, Name = "Planinska", SettlementId = 1009 },
            new Street { Id = 108008, Name = "Šidska", SettlementId = 1009 },
            new Street { Id = 108009, Name = "Solunskih dobrovoljaca", SettlementId = 1009 },
            new Street { Id = 108010, Name = "Sremska", SettlementId = 1009 },
            new Street { Id = 108011, Name = "Starogradska", SettlementId = 1009 },
            new Street { Id = 108012, Name = "Svete Petke", SettlementId = 1009 },
            new Street { Id = 108013, Name = "Svetog Save", SettlementId = 1009 },
            new Street { Id = 108014, Name = "Telek", SettlementId = 1009 },
            new Street { Id = 108015, Name = "Vojvođanska", SettlementId = 1009 },
            new Street { Id = 108016, Name = "Zaselak Depadans", SettlementId = 1009 },
            new Street { Id = 108017, Name = "12. aprila", SettlementId = 1009 },
            new Street { Id = 108018, Name = "Lipovačka", SettlementId = 1009 }
        );

        // GIBARAC - SettlementId 1010
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 109001, Name = "Cara Lazara", SettlementId = 1010 },
            new Street { Id = 109002, Name = "Desanke Maksimović", SettlementId = 1010 },
            new Street { Id = 109003, Name = "Fruškogorska", SettlementId = 1010 },
            new Street { Id = 109004, Name = "Ive Lole Ribara", SettlementId = 1010 },
            new Street { Id = 109005, Name = "Jovana Raškovića", SettlementId = 1010 },
            new Street { Id = 109006, Name = "Karađorđeva", SettlementId = 1010 },
            new Street { Id = 109007, Name = "Lovačka", SettlementId = 1010 },
            new Street { Id = 109008, Name = "Proleterska", SettlementId = 1010 },
            new Street { Id = 109009, Name = "Save Šumanovića", SettlementId = 1010 },
            new Street { Id = 109010, Name = "Svetog Save", SettlementId = 1010 },
            new Street { Id = 109011, Name = "Železnička", SettlementId = 1010 }
        );

        // JAMENA - SettlementId 1011
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 110001, Name = "Boračka", SettlementId = 1011 },
            new Street { Id = 110002, Name = "Branka Radičevića", SettlementId = 1011 },
            new Street { Id = 110003, Name = "Braće Jugovića", SettlementId = 1011 },
            new Street { Id = 110004, Name = "Vojvođanska", SettlementId = 1011 },
            new Street { Id = 110005, Name = "Vuka Karadžića", SettlementId = 1011 },
            new Street { Id = 110006, Name = "Damjana Jevrića", SettlementId = 1011 },
            new Street { Id = 110007, Name = "Karađorđeva", SettlementId = 1011 },
            new Street { Id = 110008, Name = "Kneza Lazara", SettlementId = 1011 },
            new Street { Id = 110009, Name = "Lipac", SettlementId = 1011 },
            new Street { Id = 110010, Name = "Miloša Obilića", SettlementId = 1011 },
            new Street { Id = 110011, Name = "Novosadska", SettlementId = 1011 },
            new Street { Id = 110012, Name = "Savska", SettlementId = 1011 },
            new Street { Id = 110013, Name = "Fruškogorska", SettlementId = 1011 }
        );

        // BINGULA - SettlementId 1012
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 111001, Name = "Janka Čmelika", SettlementId = 1012 },
            new Street { Id = 111002, Name = "Pinkijeva", SettlementId = 1012 },
            new Street { Id = 111003, Name = "Savska", SettlementId = 1012 },
            new Street { Id = 111004, Name = "Nikole Tesle", SettlementId = 1012 },
            new Street { Id = 111005, Name = "Proleterska", SettlementId = 1012 },
            new Street { Id = 111006, Name = "Sremska", SettlementId = 1012 },
            new Street { Id = 111007, Name = "Branka Radičevića", SettlementId = 1012 },
            new Street { Id = 111008, Name = "Fruškogorska", SettlementId = 1012 },
            new Street { Id = 111009, Name = "Stevana Šarca", SettlementId = 1012 },
            new Street { Id = 111010, Name = "Nušićeva", SettlementId = 1012 },
            new Street { Id = 111011, Name = "JNA", SettlementId = 1012 },
            new Street { Id = 111012, Name = "Lenjinova", SettlementId = 1012 },
            new Street { Id = 111013, Name = "Salaši", SettlementId = 1012 }
        );

        // ILINCI - SettlementId 1013
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 112001, Name = "Dr Jovana Raškovića", SettlementId = 1013 },
            new Street { Id = 112002, Name = "Karađorđeva", SettlementId = 1013 },
            new Street { Id = 112003, Name = "Nikole Pašića", SettlementId = 1013 },
            new Street { Id = 112004, Name = "Proleterska", SettlementId = 1013 },
            new Street { Id = 112005, Name = "Šidska", SettlementId = 1013 },
            new Street { Id = 112006, Name = "Sremska", SettlementId = 1013 },
            new Street { Id = 112007, Name = "Vojvođanska", SettlementId = 1013 },
            new Street { Id = 112008, Name = "Zmaj Jovina", SettlementId = 1013 }
        );

        // SOT - SettlementId 1014
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 113001, Name = "Dunavska", SettlementId = 1014 },
            new Street { Id = 113002, Name = "Fruškogorska", SettlementId = 1014 },
            new Street { Id = 113003, Name = "Ive Lole Ribara", SettlementId = 1014 },
            new Street { Id = 113004, Name = "Maršala Tita", SettlementId = 1014 },
            new Street { Id = 113005, Name = "Matije Gupca", SettlementId = 1014 },
            new Street { Id = 113006, Name = "Moše Pijade", SettlementId = 1014 },
            new Street { Id = 113007, Name = "Vladimira Nazora", SettlementId = 1014 },
            new Street { Id = 113008, Name = "Zaselak Jezerski", SettlementId = 1014 }
        );

        // LJUBA - SettlementId 1015
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 114001, Name = "Vojvođanska", SettlementId = 1015 },
            new Street { Id = 114002, Name = "Janka Čmelika", SettlementId = 1015 },
            new Street { Id = 114003, Name = "JNA", SettlementId = 1015 },
            new Street { Id = 114004, Name = "Masarikova", SettlementId = 1015 },
            new Street { Id = 114005, Name = "Sremska", SettlementId = 1015 },
            new Street { Id = 114006, Name = "Fruškogorska", SettlementId = 1015 },
            new Street { Id = 114007, Name = "Cerje", SettlementId = 1015 }
        );

        // BIKIĆ DO - SettlementId 1016
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 115001, Name = "Planinska", SettlementId = 1016 },
            new Street { Id = 115002, Name = "Čapajeva", SettlementId = 1016 },
            new Street { Id = 115003, Name = "Podrumska", SettlementId = 1016 }
        );

        // BATROVCI - SettlementId 1017
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 116001, Name = "Potes Selište", SettlementId = 1017 },
            new Street { Id = 116002, Name = "Ive Lole Ribara", SettlementId = 1017 },
            new Street { Id = 116003, Name = "Bosutski kej", SettlementId = 1017 },
            new Street { Id = 116004, Name = "Matije Gupca", SettlementId = 1017 },
            new Street { Id = 116005, Name = "Šidska", SettlementId = 1017 },
            new Street { Id = 116006, Name = "Pinkijeva", SettlementId = 1017 },
            new Street { Id = 116007, Name = "JNA", SettlementId = 1017 }
        );

        // MOLOVIN - SettlementId 1018
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 117001, Name = "Branka Radičevića", SettlementId = 1018 },
            new Street { Id = 117002, Name = "Fruškogorska", SettlementId = 1018 },
            new Street { Id = 117003, Name = "Sremska", SettlementId = 1018 },
            new Street { Id = 117004, Name = "Vuka Karadžića", SettlementId = 1018 }
        );

        // PRIVINA GLAVA - SettlementId 1019
        modelBuilder.Entity<Street>().HasData(
            new Street { Id = 118001, Name = "JNA", SettlementId = 1019 },
            new Street { Id = 118002, Name = "Opaljenik", SettlementId = 1019 },
            new Street { Id = 118003, Name = "Vuka Karadžića", SettlementId = 1019 }
        );

    }
}