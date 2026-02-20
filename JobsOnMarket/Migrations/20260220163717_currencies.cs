using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JobsOnMarket.Migrations
{
    /// <inheritdoc />
    public partial class currencies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PriceCurrencyId",
                table: "JobOffer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BudgetCurrencyId",
                table: "Job",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Currency",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currency", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Currency",
                columns: new[] { "Id", "Code", "Name" },
                values: new object[,]
                {
                    { 1, "USD", "United States Dollar" },
                    { 2, "AFN", "Afghan Afghani" },
                    { 3, "ALL", "Albanian Lek" },
                    { 4, "AMD", "Armenian Dram" },
                    { 5, "ANG", "Netherlands Antillean Guilder" },
                    { 6, "AOA", "Angolan Kwanza" },
                    { 7, "ARS", "Argentine Peso" },
                    { 8, "AUD", "Australian Dollar" },
                    { 9, "AWG", "Aruban Florin" },
                    { 10, "AZN", "Azerbaijani Manat" },
                    { 11, "BAM", "Bosnia-Herzegovina Convertible Mark" },
                    { 12, "BBD", "Barbadian Dollar" },
                    { 13, "BDT", "Bangladeshi Taka" },
                    { 14, "BGN", "Bulgarian Lev" },
                    { 15, "BHD", "Bahraini Dinar" },
                    { 16, "BIF", "Burundian Franc" },
                    { 17, "BMD", "Bermudian Dollar" },
                    { 18, "BND", "Brunei Dollar" },
                    { 19, "BOB", "Bolivian Boliviano" },
                    { 20, "BRL", "Brazilian Real" },
                    { 21, "BSD", "Bahamian Dollar" },
                    { 22, "BTN", "Bhutanese Ngultrum" },
                    { 23, "BWP", "Botswana Pula" },
                    { 24, "BYN", "Belarusian Ruble" },
                    { 25, "BZD", "Belize Dollar" },
                    { 26, "CAD", "Canadian Dollar" },
                    { 27, "CDF", "Congolese Franc" },
                    { 28, "CHF", "Swiss Franc" },
                    { 29, "CLP", "Chilean Peso" },
                    { 30, "CNY", "Chinese Yuan" },
                    { 31, "COP", "Colombian Peso" },
                    { 32, "CRC", "Costa Rican Colon" },
                    { 33, "CUP", "Cuban Peso" },
                    { 34, "CVE", "Cape Verdean Escudo" },
                    { 35, "CZK", "Czech Koruna" },
                    { 36, "DJF", "Djiboutian Franc" },
                    { 37, "DKK", "Danish Krone" },
                    { 38, "DOP", "Dominican Peso" },
                    { 39, "DZD", "Algerian Dinar" },
                    { 40, "EGP", "Egyptian Pound" },
                    { 41, "ERN", "Eritrean Nakfa" },
                    { 42, "ETB", "Ethiopian Birr" },
                    { 43, "EUR", "Euro" },
                    { 44, "FJD", "Fijian Dollar" },
                    { 45, "FKP", "Falkland Islands Pound" },
                    { 46, "GBP", "British Pound Sterling" },
                    { 47, "GEL", "Georgian Lari" },
                    { 48, "GHS", "Ghanaian Cedi" },
                    { 49, "GIP", "Gibraltar Pound" },
                    { 50, "GMD", "Gambian Dalasi" },
                    { 51, "GNF", "Guinean Franc" },
                    { 52, "GTQ", "Guatemalan Quetzal" },
                    { 53, "GYD", "Guyanese Dollar" },
                    { 54, "HKD", "Hong Kong Dollar" },
                    { 55, "HNL", "Honduran Lempira" },
                    { 56, "HRK", "Croatian Kuna" },
                    { 57, "HTG", "Haitian Gourde" },
                    { 58, "HUF", "Hungarian Forint" },
                    { 59, "IdR", "Indonesian Rupiah" },
                    { 60, "ILS", "Israeli New Shekel" },
                    { 61, "INR", "Indian Rupee" },
                    { 62, "IQD", "Iraqi Dinar" },
                    { 63, "IRR", "Iranian Rial" },
                    { 64, "ISK", "Icelandic Krona" },
                    { 65, "JMD", "Jamaican Dollar" },
                    { 66, "JOD", "Jordanian Dinar" },
                    { 67, "JPY", "Japanese Yen" },
                    { 68, "KES", "Kenyan Shilling" },
                    { 69, "KGS", "Kyrgyzstani Som" },
                    { 70, "KHR", "Cambodian Riel" },
                    { 71, "KMF", "Comorian Franc" },
                    { 72, "KPW", "North Korean Won" },
                    { 73, "KRW", "South Korean Won" },
                    { 74, "KWD", "Kuwaiti Dinar" },
                    { 75, "KYD", "Cayman Islands Dollar" },
                    { 76, "KZT", "Kazakhstani Tenge" },
                    { 77, "LAK", "Lao Kip" },
                    { 78, "LBP", "Lebanese Pound" },
                    { 79, "LKR", "Sri Lankan Rupee" },
                    { 80, "LRD", "Liberian Dollar" },
                    { 81, "LSL", "Lesotho Loti" },
                    { 82, "LYD", "Libyan Dinar" },
                    { 83, "MAD", "Moroccan Dirham" },
                    { 84, "MDL", "Moldovan Leu" },
                    { 85, "MGA", "Malagasy Ariary" },
                    { 86, "MKD", "Macedonian Denar" },
                    { 87, "MMK", "Myanmar Kyat" },
                    { 88, "MNT", "Mongolian Tugrik" },
                    { 89, "MOP", "Macanese Pataca" },
                    { 90, "MRU", "Mauritanian Ouguiya" },
                    { 91, "MUR", "Mauritian Rupee" },
                    { 92, "MVR", "Maldivian Rufiyaa" },
                    { 93, "MWK", "Malawian Kwacha" },
                    { 94, "MXN", "Mexican Peso" },
                    { 95, "MYR", "Malaysian Ringgit" },
                    { 96, "MZN", "Mozambican Metical" },
                    { 97, "NAD", "Namibian Dollar" },
                    { 98, "NGN", "Nigerian Naira" },
                    { 99, "NIO", "Nicaraguan Córdoba" },
                    { 100, "NOK", "Norwegian Krone" },
                    { 101, "NPR", "Nepalese Rupee" },
                    { 102, "NZD", "New Zealand Dollar" },
                    { 103, "OMR", "Omani Rial" },
                    { 104, "PAB", "Panamanian Balboa" },
                    { 105, "PEN", "Peruvian Sol" },
                    { 106, "PGK", "Papua New Guinean Kina" },
                    { 107, "PHP", "Philippine Peso" },
                    { 108, "PKR", "Pakistani Rupee" },
                    { 109, "PLN", "Polish Zloty" },
                    { 110, "PYG", "Paraguayan Guarani" },
                    { 111, "QAR", "Qatari Riyal" },
                    { 112, "RON", "Romanian Leu" },
                    { 113, "RSD", "Serbian Dinar" },
                    { 114, "RUB", "Russian Ruble" },
                    { 115, "RWF", "Rwandan Franc" },
                    { 116, "SAR", "Saudi Riyal" },
                    { 117, "SBD", "Solomon Islands Dollar" },
                    { 118, "SCR", "Seychellois Rupee" },
                    { 119, "SDG", "Sudanese Pound" },
                    { 120, "SEK", "Swedish Krona" },
                    { 121, "SGD", "Singapore Dollar" },
                    { 122, "SHP", "Saint Helena Pound" },
                    { 123, "SLL", "Sierra Leonean Leone" },
                    { 124, "SOS", "Somali Shilling" },
                    { 125, "SRD", "Surinamese Dollar" },
                    { 126, "SSP", "South Sudanese Pound" },
                    { 127, "STN", "São Tomé and Príncipe Dobra" },
                    { 128, "SVC", "Salvadoran Colón" },
                    { 129, "SYP", "Syrian Pound" },
                    { 130, "SZL", "Swazi Lilangeni" },
                    { 131, "THB", "Thai Baht" },
                    { 132, "TJS", "Tajikistani Somoni" },
                    { 133, "TMT", "Turkmenistani Manat" },
                    { 134, "TND", "Tunisian Dinar" },
                    { 135, "TOP", "Tongan Paʻanga" },
                    { 136, "TRY", "Turkish Lira" },
                    { 137, "TTD", "TrinIdad and Tobago Dollar" },
                    { 138, "TWD", "New Taiwan Dollar" },
                    { 139, "TZS", "Tanzanian Shilling" },
                    { 140, "UAH", "Ukrainian Hryvnia" },
                    { 141, "UGX", "Ugandan Shilling" },
                    { 142, "AED", "United Arab Emirates Dirham" },
                    { 143, "UYU", "Uruguayan Peso" },
                    { 144, "UZS", "Uzbekistani Som" },
                    { 145, "VES", "Venezuelan Bolívar" },
                    { 146, "VND", "Vietnamese Dong" },
                    { 147, "VUV", "Vanuatu Vatu" },
                    { 148, "WST", "Samoan Tala" },
                    { 149, "XAF", "Central African CFA Franc" },
                    { 150, "XCD", "East Caribbean Dollar" },
                    { 151, "XOF", "West African CFA Franc" },
                    { 152, "XPF", "CFP Franc" },
                    { 153, "YER", "Yemeni Rial" },
                    { 154, "ZAR", "South African Rand" },
                    { 155, "ZMW", "Zambian Kwacha" },
                    { 156, "ZWL", "Zimbabwean Dollar" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_JobOffer_PriceCurrencyId",
                table: "JobOffer",
                column: "PriceCurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_Job_BudgetCurrencyId",
                table: "Job",
                column: "BudgetCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Job_Currency_BudgetCurrencyId",
                table: "Job",
                column: "BudgetCurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobOffer_Currency_PriceCurrencyId",
                table: "JobOffer",
                column: "PriceCurrencyId",
                principalTable: "Currency",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_Currency_BudgetCurrencyId",
                table: "Job");

            migrationBuilder.DropForeignKey(
                name: "FK_JobOffer_Currency_PriceCurrencyId",
                table: "JobOffer");

            migrationBuilder.DropTable(
                name: "Currency");

            migrationBuilder.DropIndex(
                name: "IX_JobOffer_PriceCurrencyId",
                table: "JobOffer");

            migrationBuilder.DropIndex(
                name: "IX_Job_BudgetCurrencyId",
                table: "Job");

            migrationBuilder.DropColumn(
                name: "PriceCurrencyId",
                table: "JobOffer");

            migrationBuilder.DropColumn(
                name: "BudgetCurrencyId",
                table: "Job");
        }
    }
}
