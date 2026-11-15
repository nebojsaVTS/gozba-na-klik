using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GozbaNaKlik.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedSidStreets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Streets",
                columns: new[] { "Id", "Name", "SettlementId" },
                values: new object[,]
                {
                    { 100001, "Alekse Šantića", 1001 },
                    { 100002, "Arsenija Čarnojevića", 1001 },
                    { 100003, "Boračka", 1001 },
                    { 100004, "Boška Buhe", 1001 },
                    { 100005, "Bosutska", 1001 },
                    { 100006, "Braće Jerković", 1001 },
                    { 100007, "Braće Petrović", 1001 },
                    { 100008, "Branka Erića", 1001 },
                    { 100009, "Branka Radičevića", 1001 },
                    { 100010, "Čajkovska", 1001 },
                    { 100011, "Cankareva", 1001 },
                    { 100012, "Cara Dušana", 1001 },
                    { 100013, "Cara Lazara", 1001 },
                    { 100014, "Dr Dragutina Grčića", 1001 },
                    { 100015, "Dr Nikole Trumića", 1001 },
                    { 100016, "Đure Daničića", 1001 },
                    { 100017, "Đure Jakšića", 1001 },
                    { 100018, "Đure Kiša", 1001 },
                    { 100019, "Dušana Radovića", 1001 },
                    { 100020, "Filipa Višnjića", 1001 },
                    { 100021, "Fruškogorska", 1001 },
                    { 100022, "Ilije Bašičevića Bosilja", 1001 },
                    { 100023, "Ive Andrića", 1001 },
                    { 100024, "Ive Lole Ribara", 1001 },
                    { 100025, "Janka Čmelika", 1001 },
                    { 100026, "Janka Veselinovića", 1001 },
                    { 100027, "Jelene Ćetković", 1001 },
                    { 100028, "Jovana Dučića", 1001 },
                    { 100029, "Jozefa Marčoka", 1001 },
                    { 100030, "Julkin bunar", 1001 },
                    { 100031, "Karađorđeva", 1001 },
                    { 100032, "Kneza Miloša", 1001 },
                    { 100033, "Lazara Bibića", 1001 },
                    { 100034, "Laze Kostića", 1001 },
                    { 100035, "Lipovačka", 1001 },
                    { 100036, "Majke Jevrosime", 1001 },
                    { 100037, "Maksima Gorkog", 1001 },
                    { 100038, "Marka Oreškovića", 1001 },
                    { 100039, "Markov do", 1001 },
                    { 100040, "Masarikova", 1001 },
                    { 100041, "Matije Gupca", 1001 },
                    { 100042, "Mičurinova", 1001 },
                    { 100043, "Mihajla Pupina", 1001 },
                    { 100044, "Miloša Obilića", 1001 },
                    { 100045, "Mirka Bibića", 1001 },
                    { 100046, "Miroslava Antića", 1001 },
                    { 100047, "Mokranjčeva", 1001 },
                    { 100048, "Naselje istok", 1001 },
                    { 100049, "Naselje Jelice Stanivuković", 1001 },
                    { 100050, "Nikole Mikloša", 1001 },
                    { 100051, "Nikole Tesle", 1001 },
                    { 100052, "Nikole Vlaškog", 1001 },
                    { 100053, "Ognjena Price", 1001 },
                    { 100054, "Omladinska", 1001 },
                    { 100055, "Opaljenik", 1001 },
                    { 100056, "Opaljenik 2.", 1001 },
                    { 100057, "Paje Marganovića", 1001 },
                    { 100058, "Perice Stankovića", 1001 },
                    { 100059, "Petra Kočića", 1001 },
                    { 100060, "Pinkijeva", 1001 },
                    { 100061, "Proleterska", 1001 },
                    { 100062, "Prote Dušana Letića", 1001 },
                    { 100063, "Radnička", 1001 },
                    { 100064, "Šandorova", 1001 },
                    { 100065, "Save Kovačevića", 1001 },
                    { 100066, "Save Šumanovića", 1001 },
                    { 100067, "Savska", 1001 },
                    { 100068, "Selište", 1001 },
                    { 100069, "Selište 2.", 1001 },
                    { 100070, "Sime Tomovića", 1001 },
                    { 100071, "Simeona Piščevića", 1001 },
                    { 100072, "Školska", 1001 },
                    { 100073, "Slavka Merdanovića", 1001 },
                    { 100074, "Sremska", 1001 },
                    { 100075, "Sremskog fronta", 1001 },
                    { 100076, "Srete Slavujevića", 1001 },
                    { 100077, "Stevice Mandića", 1001 },
                    { 100078, "Svetog Save", 1001 },
                    { 100079, "Svetozara Markovića", 1001 },
                    { 100080, "Teodora Stanivukovića", 1001 },
                    { 100081, "Tolstojeva", 1001 },
                    { 100082, "Trg 29. novembra", 1001 },
                    { 100083, "Trg oslobođenja", 1001 },
                    { 100084, "Trg republike", 1001 },
                    { 100085, "Trg žrtava velikogospojinske racije", 1001 },
                    { 100086, "Vase Stajića", 1001 },
                    { 100087, "Veljka Paunovića", 1001 },
                    { 100088, "Veljka Vlahovića", 1001 },
                    { 100089, "Vladimira Nazora", 1001 },
                    { 100090, "Vojislava Ilića", 1001 },
                    { 100091, "Vojvođanska", 1001 },
                    { 100092, "Vojvođanskih brigada", 1001 },
                    { 100093, "Vojvode Stepe Stepanovića", 1001 },
                    { 100094, "Vuka Karadžića", 1001 },
                    { 100095, "Vukovarska", 1001 },
                    { 100096, "Zaselak Banovina", 1001 },
                    { 100097, "Zaselak Nastić salaš", 1001 },
                    { 100098, "Žikice Jovanovića Španca", 1001 },
                    { 100099, "Zlatka Šnajdera", 1001 },
                    { 100100, "Zmaj Jove Jovanovića", 1001 },
                    { 100101, "1. maja", 1001 },
                    { 100102, "2. sremskog odreda", 1001 },
                    { 100103, "6. decembra", 1001 },
                    { 100104, "7. jula", 1001 },
                    { 100105, "12. aprila", 1001 },
                    { 100106, "20. oktobra", 1001 },
                    { 101001, "Blaževac", 1002 },
                    { 101002, "Bruje", 1002 },
                    { 101003, "Cara Dušana", 1002 },
                    { 101004, "Čmelikova", 1002 },
                    { 101005, "Đipša", 1002 },
                    { 101006, "Fruškogorska", 1002 },
                    { 101007, "Grobljanska", 1002 },
                    { 101008, "Karađorđeva", 1002 },
                    { 101009, "Kralja Petra I", 1002 },
                    { 101010, "Manastirska", 1002 },
                    { 101011, "Masarikova", 1002 },
                    { 101012, "Milanke Branković", 1002 },
                    { 101013, "Moharač", 1002 },
                    { 101014, "Nikole Tesle", 1002 },
                    { 101015, "Pinkijeva", 1002 },
                    { 101016, "Proleterska", 1002 },
                    { 101017, "Šidska", 1002 },
                    { 101018, "Sremska", 1002 },
                    { 101019, "Svetog Save", 1002 },
                    { 101020, "Vojvođanska", 1002 },
                    { 101021, "Zadružna", 1002 },
                    { 101022, "Zaselak Kišelez", 1002 },
                    { 101023, "Zaselak Vorovo", 1002 },
                    { 101024, "Živojina Mišića", 1002 },
                    { 102001, "Bosutska", 1003 },
                    { 102002, "Cara Lazara", 1003 },
                    { 102003, "Desanke Maksimović", 1003 },
                    { 102004, "Fruškogorska", 1003 },
                    { 102005, "Grčkog prijateljstva", 1003 },
                    { 102006, "Miloša Obilića", 1003 },
                    { 102007, "Pašićeva", 1003 },
                    { 102008, "Republikanska", 1003 },
                    { 102009, "Sarajevska", 1003 },
                    { 102010, "Savska", 1003 },
                    { 102011, "Sremska", 1003 },
                    { 102012, "Srpskih vladara", 1003 },
                    { 102013, "Svetog Save", 1003 },
                    { 102014, "Vojvođanska", 1003 },
                    { 102015, "Vojvode Putnika", 1003 },
                    { 102016, "Vojvode Sinđelića", 1003 },
                    { 102017, "Vuka Karadžića", 1003 },
                    { 102018, "Zaselak Lazić", 1003 },
                    { 102019, "Zmaj Jovina", 1003 },
                    { 102020, "Železnička", 1003 },
                    { 103001, "Bosutska", 1004 },
                    { 103002, "Branka Radičevića", 1004 },
                    { 103003, "Fruškogorska", 1004 },
                    { 103004, "Grobljanska", 1004 },
                    { 103005, "Nikole Tesle", 1004 },
                    { 103006, "Proleterska", 1004 },
                    { 103007, "Sremski front", 1004 },
                    { 103008, "Svetog Nikole", 1004 },
                    { 103009, "Svetosavska", 1004 },
                    { 103010, "Svetozara Markovića", 1004 },
                    { 103011, "Termovul", 1004 },
                    { 103012, "Zaselak Dubrave", 1004 },
                    { 103013, "Zaselak Verencije", 1004 },
                    { 103014, "Zaselak Vranjak", 1004 },
                    { 103015, "Železnička", 1004 },
                    { 104001, "Batrovačka", 1005 },
                    { 104002, "Bosutska", 1005 },
                    { 104003, "Ive Andrića", 1005 },
                    { 104004, "Kralja Petra I", 1005 },
                    { 104005, "Mitrovačka", 1005 },
                    { 104006, "Morović", 1005 },
                    { 104007, "Nikole Tesle", 1005 },
                    { 104008, "Omladinska", 1005 },
                    { 104009, "Proleterska", 1005 },
                    { 104010, "Šidska", 1005 },
                    { 104011, "Svetog Save", 1005 },
                    { 104012, "Vuka Karadžića", 1005 },
                    { 104013, "Zaselak Vojna ustanova", 1005 },
                    { 105001, "Bosutska", 1006 },
                    { 105002, "Bosutski sokak", 1006 },
                    { 105003, "Dubrava", 1006 },
                    { 105004, "Filipa Višnjića", 1006 },
                    { 105005, "Fruškogorska", 1006 },
                    { 105006, "Grobljanska", 1006 },
                    { 105007, "Lipovačka", 1006 },
                    { 105008, "Radnička", 1006 },
                    { 105009, "Železnička", 1006 },
                    { 106001, "Bosutska", 1007 },
                    { 106002, "Fruškogorska", 1007 },
                    { 106003, "Ilača", 1007 },
                    { 106004, "Kablarovac", 1007 },
                    { 106005, "Lenjinova", 1007 },
                    { 106006, "Nikole Tesle", 1007 },
                    { 106007, "Proleterska", 1007 },
                    { 106008, "Prvomajska", 1007 },
                    { 106009, "Vojvođanska", 1007 },
                    { 106010, "Vuka Karadžića", 1007 },
                    { 107001, "Bena", 1008 },
                    { 107002, "Branka Radičevića", 1008 },
                    { 107003, "Fruškogorska", 1008 },
                    { 107004, "Palilula", 1008 },
                    { 107005, "Partizanska", 1008 },
                    { 107006, "Rusinska", 1008 },
                    { 107007, "Salaši", 1008 },
                    { 107008, "Sremska", 1008 },
                    { 107009, "Vojvođanska", 1008 },
                    { 107010, "Vuka Karadžića", 1008 },
                    { 107011, "Železnička", 1008 },
                    { 107012, "Zmaj Jove Jovanovića", 1008 },
                    { 108001, "Češće", 1009 },
                    { 108002, "Despotovac", 1009 },
                    { 108003, "Fruškogorska", 1009 },
                    { 108004, "Livade", 1009 },
                    { 108005, "Manastirska", 1009 },
                    { 108006, "Partizanska", 1009 },
                    { 108007, "Planinska", 1009 },
                    { 108008, "Šidska", 1009 },
                    { 108009, "Solunskih dobrovoljaca", 1009 },
                    { 108010, "Sremska", 1009 },
                    { 108011, "Starogradska", 1009 },
                    { 108012, "Svete Petke", 1009 },
                    { 108013, "Svetog Save", 1009 },
                    { 108014, "Telek", 1009 },
                    { 108015, "Vojvođanska", 1009 },
                    { 108016, "Zaselak Depadans", 1009 },
                    { 108017, "12. aprila", 1009 },
                    { 108018, "Lipovačka", 1009 },
                    { 109001, "Cara Lazara", 1010 },
                    { 109002, "Desanke Maksimović", 1010 },
                    { 109003, "Fruškogorska", 1010 },
                    { 109004, "Ive Lole Ribara", 1010 },
                    { 109005, "Jovana Raškovića", 1010 },
                    { 109006, "Karađorđeva", 1010 },
                    { 109007, "Lovačka", 1010 },
                    { 109008, "Proleterska", 1010 },
                    { 109009, "Save Šumanovića", 1010 },
                    { 109010, "Svetog Save", 1010 },
                    { 109011, "Železnička", 1010 },
                    { 110001, "Boračka", 1011 },
                    { 110002, "Branka Radičevića", 1011 },
                    { 110003, "Braće Jugovića", 1011 },
                    { 110004, "Vojvođanska", 1011 },
                    { 110005, "Vuka Karadžića", 1011 },
                    { 110006, "Damjana Jevrića", 1011 },
                    { 110007, "Karađorđeva", 1011 },
                    { 110008, "Kneza Lazara", 1011 },
                    { 110009, "Lipac", 1011 },
                    { 110010, "Miloša Obilića", 1011 },
                    { 110011, "Novosadska", 1011 },
                    { 110012, "Savska", 1011 },
                    { 110013, "Fruškogorska", 1011 },
                    { 111001, "Janka Čmelika", 1012 },
                    { 111002, "Pinkijeva", 1012 },
                    { 111003, "Savska", 1012 },
                    { 111004, "Nikole Tesle", 1012 },
                    { 111005, "Proleterska", 1012 },
                    { 111006, "Sremska", 1012 },
                    { 111007, "Branka Radičevića", 1012 },
                    { 111008, "Fruškogorska", 1012 },
                    { 111009, "Stevana Šarca", 1012 },
                    { 111010, "Nušićeva", 1012 },
                    { 111011, "JNA", 1012 },
                    { 111012, "Lenjinova", 1012 },
                    { 111013, "Salaši", 1012 },
                    { 112001, "Dr Jovana Raškovića", 1013 },
                    { 112002, "Karađorđeva", 1013 },
                    { 112003, "Nikole Pašića", 1013 },
                    { 112004, "Proleterska", 1013 },
                    { 112005, "Šidska", 1013 },
                    { 112006, "Sremska", 1013 },
                    { 112007, "Vojvođanska", 1013 },
                    { 112008, "Zmaj Jovina", 1013 },
                    { 113001, "Dunavska", 1014 },
                    { 113002, "Fruškogorska", 1014 },
                    { 113003, "Ive Lole Ribara", 1014 },
                    { 113004, "Maršala Tita", 1014 },
                    { 113005, "Matije Gupca", 1014 },
                    { 113006, "Moše Pijade", 1014 },
                    { 113007, "Vladimira Nazora", 1014 },
                    { 113008, "Zaselak Jezerski", 1014 },
                    { 114001, "Vojvođanska", 1015 },
                    { 114002, "Janka Čmelika", 1015 },
                    { 114003, "JNA", 1015 },
                    { 114004, "Masarikova", 1015 },
                    { 114005, "Sremska", 1015 },
                    { 114006, "Fruškogorska", 1015 },
                    { 114007, "Cerje", 1015 },
                    { 115001, "Planinska", 1016 },
                    { 115002, "Čapajeva", 1016 },
                    { 115003, "Podrumska", 1016 },
                    { 116001, "Potes Selište", 1017 },
                    { 116002, "Ive Lole Ribara", 1017 },
                    { 116003, "Bosutski kej", 1017 },
                    { 116004, "Matije Gupca", 1017 },
                    { 116005, "Šidska", 1017 },
                    { 116006, "Pinkijeva", 1017 },
                    { 116007, "JNA", 1017 },
                    { 117001, "Branka Radičevića", 1018 },
                    { 117002, "Fruškogorska", 1018 },
                    { 117003, "Sremska", 1018 },
                    { 117004, "Vuka Karadžića", 1018 },
                    { 118001, "JNA", 1019 },
                    { 118002, "Opaljenik", 1019 },
                    { 118003, "Vuka Karadžića", 1019 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100014);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100015);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100016);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100017);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100018);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100019);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100020);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100021);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100022);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100023);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100024);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100025);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100026);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100027);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100028);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100029);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100030);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100031);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100032);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100033);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100034);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100035);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100036);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100037);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100038);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100039);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100040);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100041);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100042);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100043);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100044);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100045);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100046);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100047);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100048);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100049);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100050);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100051);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100052);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100053);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100054);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100055);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100056);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100057);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100058);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100059);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100060);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100061);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100062);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100063);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100064);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100065);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100066);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100067);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100068);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100069);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100070);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100071);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100072);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100073);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100074);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100075);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100076);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100077);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100078);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100079);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100080);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100081);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100082);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100083);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100084);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100085);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100086);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100087);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100088);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100089);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100090);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100091);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100092);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100093);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100094);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100095);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100096);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100097);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100098);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100099);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100100);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100101);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100102);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100103);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100104);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100105);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 100106);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101014);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101015);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101016);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101017);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101018);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101019);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101020);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101021);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101022);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101023);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 101024);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102014);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102015);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102016);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102017);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102018);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102019);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 102020);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103014);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 103015);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 104013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 105009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 106010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 107012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108014);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108015);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108016);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108017);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 108018);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 109011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 110013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111009);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111010);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111011);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111012);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 111013);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 112008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 113008);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 114007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 115001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 115002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 115003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116005);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116006);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 116007);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 117001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 117002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 117003);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 117004);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 118001);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 118002);

            migrationBuilder.DeleteData(
                table: "Streets",
                keyColumn: "Id",
                keyValue: 118003);
        }
    }
}
