using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models
{
    public class Country : Named, ICountry
    {
        public Country()
        {
            Name = "Unknown";
            Abbreviation = string.Empty;
            Code = "XA";
        }

        private Country(object id) : this()
        {
            Initialize(id);
        }

        public string Code { get; set; }

        static Country()
        {
            CurrentId = DefaultId;
            AddCountry(string.Empty, string.Empty);
            AddCountry("Afghanistan", "AF");
            AddCountry("Åland Islands", "AX");
            AddCountry("Albania", "AL");
            AddCountry("Algeria", "DZ");
            AddCountry("American Samoa", "AS");
            AddCountry("Andorra", "AD");
            AddCountry("Angola", "AO");
            AddCountry("Anguilla", "AI");
            AddCountry("Antarctica", "AQ");
            AddCountry("Antigua and Barbuda", "AG");
            AddCountry("Argentina", "AR");
            AddCountry("Armenia", "AM");
            AddCountry("Aruba", "AW");
            AddCountry("Australia", "AU");
            AddCountry("Austria", "AT");
            AddCountry("Azerbaijan", "AZ");
            AddCountry("Bahamas", "BS");
            AddCountry("Bahrain", "BH");
            AddCountry("Bangladesh", "BD");
            AddCountry("Barbados", "BB");
            AddCountry("Belarus", "BY");
            AddCountry("Belgium", "BE");
            AddCountry("Belize", "BZ");
            AddCountry("Benin", "BJ");
            AddCountry("Bermuda", "BM");
            AddCountry("Bhutan", "BT");
            AddCountry("Bolivia, Plurinational State of", "BO");
            AddCountry("Bosnia and Herzegovina", "BA");
            AddCountry("Botswana", "BW");
            AddCountry("Bouvet Island", "BV");
            AddCountry("Brazil", "BR");
            AddCountry("British Indian Ocean Territory", "IO");
            AddCountry("Brunei Darussalam", "BN");
            AddCountry("Bulgaria", "BG");
            AddCountry("Burkina Faso", "BF");
            AddCountry("Burundi", "BI");
            AddCountry("Cambodia", "KH");
            AddCountry("Cameroon", "CM");
            AddCountry("Canada", "CA");
            AddCountry("Cape Verde", "CV");
            AddCountry("Cayman Islands", "KY");
            AddCountry("Central African Republic", "CF");
            AddCountry("Chad", "TD");
            AddCountry("Chile", "CL");
            AddCountry("China", "CN");
            AddCountry("Christmas Island", "CX");
            AddCountry("Cocos (Keeling) Islands", "CC");
            AddCountry("Colombia", "CO");
            AddCountry("Comoros", "KM");
            AddCountry("Congo", "CG");
            AddCountry("Congo, The Democratic Republic of the", "CD");
            AddCountry("Cook Islands", "CK");
            AddCountry("Costa Rica", "CR");
            AddCountry("Côte d'Ivoire", "CI");
            AddCountry("Croatia", "HR");
            AddCountry("Cuba", "CU");
            AddCountry("Cyprus", "CY");
            AddCountry("Czech Republic", "CZ");
            AddCountry("Denmark", "DK");
            AddCountry("Djibouti", "DJ");
            AddCountry("Dominica", "DM");
            AddCountry("Dominican Republic", "DO");
            AddCountry("Ecuador", "EC");
            AddCountry("Egypt", "EG");
            AddCountry("El Salvador", "SV");
            AddCountry("Equatorial Guinea", "GQ");
            AddCountry("Eritrea", "ER");
            AddCountry("Estonia", "EE");
            AddCountry("Ethiopa", "ET");
            AddCountry("Falkland Islands (Malvinas)", "FK");
            AddCountry("Faroe Islands", "FO");
            AddCountry("Fiji", "FJ");
            AddCountry("Finland", "FI");
            AddCountry("France", "FR");
            AddCountry("French Guiana", "GF");
            AddCountry("French Polynesia", "PF");
            AddCountry("French Southern Territories", "TF");
            AddCountry("Gabon", "GA");
            AddCountry("Gambia", "GM");
            AddCountry("Georgia", "GE");
            AddCountry("Germany", "DE");
            AddCountry("Ghana", "GH");
            AddCountry("Gibraltar", "GI");
            AddCountry("Greece", "GR");
            AddCountry("Greenland", "GL");
            AddCountry("Grenada", "GD");
            AddCountry("Guadeloupe", "GP");
            AddCountry("Guam", "GU");
            AddCountry("Guatemala", "GT");
            AddCountry("Guernsey", "GG");
            AddCountry("Guinea", "GN");
            AddCountry("Guinea-Bissau", "GW");
            AddCountry("Guyana", "GY");
            AddCountry("Haiti", "HT");
            AddCountry("Heard Island and McDonald Islands", "HM");
            AddCountry("Holy See (Vatican City State)", "VA");
            AddCountry("Honduras", "HN");
            AddCountry("Hong Kong", "HK");
            AddCountry("Hungary", "HU");
            AddCountry("Iceland", "IS");
            AddCountry("India", "IN");
            AddCountry("Indonesia", "ID");
            AddCountry("Iran, Islamic Republic of", "IR");
            AddCountry("Iraq", "IQ");
            AddCountry("Ireland", "IE");
            AddCountry("Isle of Man", "IM");
            AddCountry("Israel", "IL");
            AddCountry("Italy", "IT");
            AddCountry("Jamaica", "JM");
            AddCountry("Japan", "JP");
            AddCountry("Jersey", "JE");
            AddCountry("Jordan", "JO");
            AddCountry("Kazakhstan", "KZ");
            AddCountry("Kenya", "KE");
            AddCountry("Kiribati", "KI");
            AddCountry("Korea, Democratic People's Republic of", "KP");
            AddCountry("Korea, Republic of (South Korea)", "KR");
            AddCountry("Kuwait", "KW");
            AddCountry("Kyrgyzstan", "KG");
            AddCountry("Loa People's Democratic Republic", "LA");
            AddCountry("Latvia", "LV");
            AddCountry("Lebanon", "LB");
            AddCountry("Lesotho", "LS");
            AddCountry("Liberia", "LR");
            AddCountry("Libyan Arab Jamahiriya", "LY");
            AddCountry("Liechtenstein", "LI");
            AddCountry("Lithuania", "LT");
            AddCountry("Luxemboug", "LU");
            AddCountry("Macao", "MO");
            AddCountry("Macedonia, The Former Yugoslav Republic of", "MK");
            AddCountry("Madagascar", "MG");
            AddCountry("Malawi", "MW");
            AddCountry("Malaysia", "MY");
            AddCountry("Maldives", "MV");
            AddCountry("Mali", "ML");
            AddCountry("Malta", "MT");
            AddCountry("Marshall Islands", "MH");
            AddCountry("Martinique", "MQ");
            AddCountry("Mauritania", "MR");
            AddCountry("Mauritius", "MU");
            AddCountry("Mayotte", "YT");
            AddCountry("Mexico", "MX");
            AddCountry("Micronesia, Federated State of", "FM");
            AddCountry("Moldova, Republic of", "MD");
            AddCountry("Monaco", "MC");
            AddCountry("Mongolia", "MN");
            AddCountry("Montenegro", "ME");
            AddCountry("Montserrat", "MS");
            AddCountry("Morocco", "MA");
            AddCountry("Mozambique", "MZ");
            AddCountry("Myanmar", "MM");
            AddCountry("Namibia", "NA");
            AddCountry("Nauru", "NR");
            AddCountry("Nepal", "NP");
            AddCountry("Netherlands", "NL");
            AddCountry("Netherlands Antilles", "AN");
            AddCountry("New Caledonia", "NC");
            AddCountry("New Zealand", "NZ");
            AddCountry("Nicaragua", "NI");
            AddCountry("Niger", "NE");
            AddCountry("Nigeria", "NG");
            AddCountry("Niue", "NU");
            AddCountry("Norfolk Island", "NF");
            AddCountry("Northern Mariana Islands", "MP");
            AddCountry("Norway", "NO");
            AddCountry("Oman", "OM");
            AddCountry("Pakistan", "PK");
            AddCountry("Palau", "PW");
            AddCountry("Palestinian Territory, Occupied", "PS");
            AddCountry("Panama", "PA");
            AddCountry("Papua New Guinea", "PG");
            AddCountry("Paraguay", "PY");
            AddCountry("Peru", "PE");
            AddCountry("Philippines", "PH");
            AddCountry("Pitcairn", "PN");
            AddCountry("Poland", "PL");
            AddCountry("Portugal", "PT");
            AddCountry("Puerto Rico", "PR");
            AddCountry("Qatar", "QA");
            AddCountry("Réunion", "RE");
            AddCountry("Romania", "RO");
            AddCountry("Russian Federation", "RU");
            AddCountry("Rwanda", "RW");
            AddCountry("Saint Barthélemy", "BL");
            AddCountry("Saint Helena, Ascension and Tristan da Cunha", "SH");
            AddCountry("Saint Kitts and Nevis", "KN");
            AddCountry("Saint Lucia", "LC");
            AddCountry("Saint Martin", "MF");
            AddCountry("Saint Pierre and Miquelon", "PM");
            AddCountry("Saint Vincent and the Grenadines", "VC");
            AddCountry("Samoa", "WS");
            AddCountry("San Marino", "SM");
            AddCountry("Sao Tome and Principe", "ST");
            AddCountry("Saudi Arabia", "SA");
            AddCountry("Senegal", "SN");
            AddCountry("Serbia", "RS");
            AddCountry("Seychelles", "SC");
            AddCountry("Sierra Leone", "SL");
            AddCountry("Singapore", "SG");
            AddCountry("Slovakia", "SK");
            AddCountry("Slovenia", "SI");
            AddCountry("Solomon Islands", "SB");
            AddCountry("Somalia", "SO");
            AddCountry("South Africa", "ZA");
            AddCountry("South Georgia and the South Sandwich Islands", "GS");
            AddCountry("Spain", "ES");
            AddCountry("Sri Lanka", "LK");
            AddCountry("Sudan", "SD");
            AddCountry("Suriname", "SR");
            AddCountry("Svalbard and Jan Mayen", "SJ");
            AddCountry("Swaziland", "SZ");
            AddCountry("Sweden", "SE");
            AddCountry("Switzerland", "CH");
            AddCountry("Syrian Arab Republic", "SY");
            AddCountry("Taiwan, Province of China", "TW");
            AddCountry("Tajikistan", "TJ");
            AddCountry("Tanzania, United Republic of", "TZ");
            AddCountry("Thailand", "TH");
            AddCountry("Timor-Leste", "TL");
            AddCountry("Togo", "TG");
            AddCountry("Tokelau", "TK");
            AddCountry("Tonga", "TO");
            AddCountry("Trinidad and Tobago", "TT");
            AddCountry("Tunisia", "TN");
            AddCountry("Turkey", "TR");
            AddCountry("Turkmenistan", "TM");
            AddCountry("Turks and Caicos Islands", "TC");
            AddCountry("Tuvalu", "TV");
            AddCountry("Uganda", "UG");
            AddCountry("Ukraine", "UA");
            AddCountry("United Arab Emirates", "AE");
            AddCountry("United Kingdom", "GB");
            AddCountry("United States of America", "US");
            AddCountry("United States Minor Outlying Islands", "UM");
            AddCountry("Uruguay", "UY");
            AddCountry("Uzbekistan", "UZ");
            AddCountry("Vanuatu", "VU");
            AddCountry("Venezuela, Bolivarian Republic of", "VE");
            AddCountry("Viet Nam", "VN");
            AddCountry("Virgin Islands, British", "VG");
            AddCountry("Virgin Islands, United States", "VI");
            AddCountry("Wallis and Futuna", "WF");
            AddCountry("Western Sahara", "EH");
            AddCountry("Yemen", "YE");
            AddCountry("Zambia", "ZM");
            AddCountry("Zimbabwe", "ZW");
        }

        private static readonly IDictionary<object, ICountry> _countries = new Dictionary<object, ICountry>();
        private static long CurrentId;
        private const long DefaultId = 1L;

        private static void AddCountry(string name, string code)
        {
            var country = new Country(CurrentId);
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(code))
            {
                country.Name = name;
                country.Code = code;
            }

            _countries.Add(country.Id, country); 
            CurrentId++;
        }

        public static ICountry GetDefault()
        {
            return GetOne(DefaultId);
        }

        public static ICountry GetOne(object id)
        {
            return (_countries.ContainsKey(id)) ? _countries[id] : null;
        }

        public static ICollection<ICountry> GetAll()
        {
            return _countries.Values;
        }
    }
}
