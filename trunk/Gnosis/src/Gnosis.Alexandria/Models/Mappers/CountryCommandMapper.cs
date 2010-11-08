using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Gnosis.Alexandria.Models.Interfaces;

namespace Gnosis.Alexandria.Models.Mappers
{
    public class CountryCommandMapper : CommandMapper<ICountry>
    {
        public CountryCommandMapper(IFactory<ICommandBuilder> factory)
            : base(factory, "Country")
        {
        }

        protected override ICommand GetInitializeCommand(ICommandBuilder builder)
        {
            return builder.Append(
@"create table if not exists Country (
	Id integer not null primary key autoincrement,
	Name text not null unique,
    NameHash text not null unique,
    Abbreviation text not null default '',
	Code text not null unique
);
create index if not exists Country_index_Name on Country (Name);
create index if not exists Country_index_NameHash on Country (NameHash);
create index if not exists Country_index_Code on Country (Code);
insert or ignore into Country (Name, NameHash, Code) values ('Unknown', 'UNKNOWN', 'XA');
insert or ignore into Country (Name, NameHash, Code) values ('Afghanistan', 'AFGHANISTAN', 'AF');
insert or ignore into Country (Name, NameHash, Code) values ('Åland Islands', 'ALANDISLANDS','AX');
insert or ignore into Country (Name, NameHash, Code) values ('Albania', 'ALBANIA','AL');
insert or ignore into Country (Name, NameHash, Code) values ('Algeria', 'ALGERIA','DZ');
insert or ignore into Country (Name, NameHash, Code) values ('American Samoa', 'AMERICANSAMOA','AS');
insert or ignore into Country (Name, NameHash, Code) values ('Andorra', 'ANDORRA','AD');
insert or ignore into Country (Name, NameHash, Code) values ('Angola','ANGOLA','AO');
insert or ignore into Country (Name, NameHash, Code) values ('Anguilla','ANGUILLA','AI');
insert or ignore into Country (Name, NameHash, Code) values ('Antarctica','ANTARCTICA','AQ');
insert or ignore into Country (Name, NameHash, Code) values ('Antigua and Barbuda','ANTIGUABARBUDA','AG');
insert or ignore into Country (Name, NameHash, Code) values ('Argentina', 'ARGENTINA','AR');
insert or ignore into Country (Name, NameHash, Code) values ('Armenia', 'ARMENIA','AM');
insert or ignore into Country (Name, NameHash, Code) values ('Aruba', 'ARUBA','AW');
insert or ignore into Country (Name, NameHash, Code) values ('Australia', 'AUSTRALIA','AU');
insert or ignore into Country (Name, NameHash, Code) values ('Austria','AUSTRIA','AT');
insert or ignore into Country (Name, NameHash, Code) values ('Azerbaijan','AZERBAIJAN','AZ');
insert or ignore into Country (Name, NameHash, Code) values ('Bahamas','BAHAMAS','BS');
insert or ignore into Country (Name, NameHash, Code) values ('Bahrain','BAHRAIN','BH');
insert or ignore into Country (Name, NameHash, Code) values ('Bangladesh','BANGLADESH','BD');
insert or ignore into Country (Name, NameHash, Code) values ('Barbados','BARBADOS','BB');
insert or ignore into Country (Name, NameHash, Code) values ('Belarus','BELARUS','BY');
insert or ignore into Country (Name, NameHash, Code) values ('Belgium','BELGIUM','BE');
insert or ignore into Country (Name, NameHash, Code) values ('Belize','BELIZE','BZ');
insert or ignore into Country (Name, NameHash, Code) values ('Benin','BENIN','BJ');
insert or ignore into Country (Name, NameHash, Code) values ('Bermuda','BERMUDA','BM');
insert or ignore into Country (Name, NameHash, Code) values ('Bhutan','BHUTAN','BT');
insert or ignore into Country (Name, NameHash, Code) values ('Bolivia, Plurinational State of','BOLIVIAPLURINATIONALSTATE','BO');
insert or ignore into Country (Name, NameHash, Code) values ('Bosnia and Herzegovina','BOSNIAHERZEGOVINA','BA');
insert or ignore into Country (Name, NameHash, Code) values ('Botswana','BOTSWANA','BW');
insert or ignore into Country (Name, NameHash, Code) values ('Bouvet Island','BOUVETISLAND','BV');
insert or ignore into Country (Name, NameHash, Code) values ('Brazil','BRAZIL','BR');
insert or ignore into Country (Name, NameHash, Code) values ('British Indian Ocean Territory','BRITISHINDIANOCEANTERRITORY','IO');
insert or ignore into Country (Name, NameHash, Code) values ('Brunei Darussalam', 'BRUNEIDARUSSALAM','BN');
insert or ignore into Country (Name, NameHash, Code) values ('Bulgaria','BULGARIA','BG');
insert or ignore into Country (Name, NameHash, Code) values ('Burkina Faso','BURKINAFASO','BF');
insert or ignore into Country (Name, NameHash, Code) values ('Burundi','BURUNDI','BI');
insert or ignore into Country (Name, NameHash, Code) values ('Cambodia','CAMBODIA','KH');
insert or ignore into Country (Name, NameHash, Code) values ('Cameroon','CAMEROON','CM');
insert or ignore into Country (Name, NameHash, Code) values ('Canada','CANADA','CA');
insert or ignore into Country (Name, NameHash, Code) values ('Cape Verde','CAPEVERDE','CV');
insert or ignore into Country (Name, NameHash, Code) values ('Cayman Islands','CAYMANISLANDS','KY');
insert or ignore into Country (Name, NameHash, Code) values ('Central African Republic','CENTRALAFRICANREPUBLIC','CF');
insert or ignore into Country (Name, NameHash, Code) values ('Chad','CHAD','TD');
insert or ignore into Country (Name, NameHash, Code) values ('Chile','CHILE','CL');
insert or ignore into Country (Name, NameHash, Code) values ('China','CHINA','CN');
insert or ignore into Country (Name, NameHash, Code) values ('Christmas Island','CHRISTMASISLAND','CX');
insert or ignore into Country (Name, NameHash, Code) values ('Cocos (Keeling) Islands','COCOSKEELINGISLANDS','CC');
insert or ignore into Country (Name, NameHash, Code) values ('Colombia','COLOMBIA','CO');
insert or ignore into Country (Name, NameHash, Code) values ('Comoros','COMOROS','KM');
insert or ignore into Country (Name, NameHash, Code) values ('Congo','CONGO','CG');
insert or ignore into Country (Name, NameHash, Code) values ('Congo, The Democratic Republic of the','CONGODEMOCRATICREPUBLIC','CD');
insert or ignore into Country (Name, NameHash, Code) values ('Cook Islands','COOKISLANDS','CK');
insert or ignore into Country (Name, NameHash, Code) values ('Costa Rica','COSTARICA','CR');
insert or ignore into Country (Name, NameHash, Code) values ('Côte d''Ivoire','COTEDIVOIRE','CI');
insert or ignore into Country (Name, NameHash, Code) values ('Croatia','CROATIA','HR');
insert or ignore into Country (Name, NameHash, Code) values ('Cuba','CUBA','CU');
insert or ignore into Country (Name, NameHash, Code) values ('Cyprus','CYPRUS','CY');
insert or ignore into Country (Name, NameHash, Code) values ('Czech Republic','CZECHREPUBLIC','CZ');
insert or ignore into Country (Name, NameHash, Code) values ('Denmark','DENMARK','DK');
insert or ignore into Country (Name, NameHash, Code) values ('Djibouti','DJIBOUTI','DJ');
insert or ignore into Country (Name, NameHash, Code) values ('Dominica','DOMINICA','DM');
insert or ignore into Country (Name, NameHash, Code) values ('Dominican Republic','DOMINICANREPUBLIC','DO');
insert or ignore into Country (Name, NameHash, Code) values ('Ecuador','ECUADOR','EC');
insert or ignore into Country (Name, NameHash, Code) values ('Egypt','EGYPT','EG');
insert or ignore into Country (Name, NameHash, Code) values ('El Salvador','SALVADOR','SV');
insert or ignore into Country (Name, NameHash, Code) values ('Equatorial Guinea','EQUATORIALGUINEA','GQ');
insert or ignore into Country (Name, NameHash, Code) values ('Eritrea','ERITREA','ER');
insert or ignore into Country (Name, NameHash, Code) values ('Estonia','ESTONIA','EE');
insert or ignore into Country (Name, NameHash, Code) values ('Ethiopa','ETHIOPIA','ET');
insert or ignore into Country (Name, NameHash, Code) values ('Falkland Islands (Malvinas)','FALKLANDISLANDSMALVINAS','FK');
insert or ignore into Country (Name, NameHash, Code) values ('Faroe Islands','FAROEISLANDS','FO');
insert or ignore into Country (Name, NameHash, Code) values ('Fiji','FIJI','FJ');
insert or ignore into Country (Name, NameHash, Code) values ('Finland','FINLAND','FI');
insert or ignore into Country (Name, NameHash, Code) values ('France','FRANCE','FR');
insert or ignore into Country (Name, NameHash, Code) values ('French Guiana','FRENCHGUIANA','GF');
insert or ignore into Country (Name, NameHash, Code) values ('French Polynesia','FRENCHPOLYNESIA','PF');
insert or ignore into Country (Name, NameHash, Code) values ('French Southern Territories','FRENCHSOUTHTERRITORIES','TF');
insert or ignore into Country (Name, NameHash, Code) values ('Gabon','GABON','GA');
insert or ignore into Country (Name, NameHash, Code) values ('Gambia','GAMBIA','GM');
insert or ignore into Country (Name, NameHash, Code) values ('Georgia','GEORGIA','GE');
insert or ignore into Country (Name, NameHash, Code) values ('Germany','GERMANY','DE');
insert or ignore into Country (Name, NameHash, Code) values ('Ghana','GHANA','GH');
insert or ignore into Country (Name, NameHash, Code) values ('Gibraltar','GIBRALTAR','GI');
insert or ignore into Country (Name, NameHash, Code) values ('Greece','GREECE','GR');
insert or ignore into Country (Name, NameHash, Code) values ('Greenland','GREENLAND','GL');
insert or ignore into Country (Name, NameHash, Code) values ('Grenada','GRENADA','GD');
insert or ignore into Country (Name, NameHash, Code) values ('Guadeloupe','GUADELOUPE','GP');
insert or ignore into Country (Name, NameHash, Code) values ('Guam','GUAM','GU');
insert or ignore into Country (Name, NameHash, Code) values ('Guatemala','GUATEMALA','GT');
insert or ignore into Country (Name, NameHash, Code) values ('Guernsey','GUERNSEY','GG');
insert or ignore into Country (Name, NameHash, Code) values ('Guinea','GUINEA','GN');
insert or ignore into Country (Name, NameHash, Code) values ('Guinea-Bissau','GUINEABISSAU','GW');
insert or ignore into Country (Name, NameHash, Code) values ('Guyana','GUYANA','GY');
insert or ignore into Country (Name, NameHash, Code) values ('Haiti','HAITI','HT');
insert or ignore into Country (Name, NameHash, Code) values ('Heard Island and McDonald Islands','HEARDISLANDMCDONALDISLANDS','HM');
insert or ignore into Country (Name, NameHash, Code) values ('Holy See (Vatican City State)','HOLYSEEVATICANCITYSTATE','VA');
insert or ignore into Country (Name, NameHash, Code) values ('Honduras','HONDURAS','HN');
insert or ignore into Country (Name, NameHash, Code) values ('Hong Kong','HONGKONG','HK');
insert or ignore into Country (Name, NameHash, Code) values ('Hungary','HUNGARY','HU');
insert or ignore into Country (Name, NameHash, Code) values ('Iceland','ICELAND','IS');
insert or ignore into Country (Name, NameHash, Code) values ('India','INDIA','IN');
insert or ignore into Country (Name, NameHash, Code) values ('Indonesia','INDONESIA','ID');
insert or ignore into Country (Name, NameHash, Code) values ('Iran, Islamic Republic of','IRANISLAMICREPUBLIC','IR');
insert or ignore into Country (Name, NameHash, Code) values ('Iraq','IRAQ','IQ');
insert or ignore into Country (Name, NameHash, Code) values ('Ireland','IRELAND','IE');
insert or ignore into Country (Name, NameHash, Code) values ('Isle of Man','ISLEMAN','IM');
insert or ignore into Country (Name, NameHash, Code) values ('Israel','ISRAEL','IL');
insert or ignore into Country (Name, NameHash, Code) values ('Italy','ITALY','IT');
insert or ignore into Country (Name, NameHash, Code) values ('Jamaica','JAMAICA','JM');
insert or ignore into Country (Name, NameHash, Code) values ('Japan','JAPAN','JP');
insert or ignore into Country (Name, NameHash, Code) values ('Jersey','JERSEY','JE');
insert or ignore into Country (Name, NameHash, Code) values ('Jordan','JORDAN','JO');
insert or ignore into Country (Name, NameHash, Code) values ('Kazakhstan','KAZAKHSTAN','KZ');
insert or ignore into Country (Name, NameHash, Code) values ('Kenya','KENYA','KE');
insert or ignore into Country (Name, NameHash, Code) values ('Kiribati','KIRIBATI','KI');
insert or ignore into Country (Name, NameHash, Code) values ('Korea, Democratic People''s Republic of','KOREADEMOCRATICPEOPLESREPUBLIC','KP');
insert or ignore into Country (Name, NameHash, Code) values ('Korea, Republic of (South Korea)','KOREAREPUBLICSOUTHKOREA','KR');
insert or ignore into Country (Name, NameHash, Code) values ('Kuwait','KUWAIT','KW');
insert or ignore into Country (Name, NameHash, Code) values ('Kyrgyzstan','KYRGYZSTAN','KG');
insert or ignore into Country (Name, NameHash, Code) values ('Loa People''s Democratic Republic','LAOPEOPLESDEMOCRATICREPUBLIC','LA');
insert or ignore into Country (Name, NameHash, Code) values ('Latvia','LATVIA','LV');
insert or ignore into Country (Name, NameHash, Code) values ('Lebanon','LEBANON','LB');
insert or ignore into Country (Name, NameHash, Code) values ('Lesotho','LESOTHO','LS');
insert or ignore into Country (Name, NameHash, Code) values ('Liberia','LIBERIA','LR');
insert or ignore into Country (Name, NameHash, Code) values ('Libyan Arab Jamahiriya','LIBYANARABJAMAHIRIYA','LY');
insert or ignore into Country (Name, NameHash, Code) values ('Liechtenstein','LIECHTENSTEIN','LI');
insert or ignore into Country (Name, NameHash, Code) values ('Lithuania','LITHUANIA','LT');
insert or ignore into Country (Name, NameHash, Code) values ('Luxemboug','LUXEMBOURG','LU');
insert or ignore into Country (Name, NameHash, Code) values ('Macao','MACAO','MO');
insert or ignore into Country (Name, NameHash, Code) values ('Macedonia, The Former Yugoslav Republic of','MACEDONIAFORMERYUGOSLAVREPUBLIC','MK');
insert or ignore into Country (Name, NameHash, Code) values ('Madagascar','MADAGASCAR','MG');
insert or ignore into Country (Name, NameHash, Code) values ('Malawi','MALAWI','MW');
insert or ignore into Country (Name, NameHash, Code) values ('Malaysia','MALAYSIA','MY');
insert or ignore into Country (Name, NameHash, Code) values ('Maldives','MALDIVES','MV');
insert or ignore into Country (Name, NameHash, Code) values ('Mali','MALI','ML');
insert or ignore into Country (Name, NameHash, Code) values ('Malta','MALTA','MT');
insert or ignore into Country (Name, NameHash, Code) values ('Marshall Islands','MARSHALLISLANDS','MH');
insert or ignore into Country (Name, NameHash, Code) values ('Martinique', 'MARTINIQUE','MQ');
insert or ignore into Country (Name, NameHash, Code) values ('Mauritania','MAURITANIA','MR');
insert or ignore into Country (Name, NameHash, Code) values ('Mauritius','MAURITIUS','MU');
insert or ignore into Country (Name, NameHash, Code) values ('Mayotte','MAYOTTE','YT');
insert or ignore into Country (Name, NameHash, Code) values ('Mexico','MEXICO','MX');
insert or ignore into Country (Name, NameHash, Code) values ('Micronesia, Federated State of','MICRONESIAFEDERATEDSTATES','FM');
insert or ignore into Country (Name, NameHash, Code) values ('Moldova, Republic of','MOLDOVAREPUBLIC','MD');
insert or ignore into Country (Name, NameHash, Code) values ('Monaco','MONACO','MC');
insert or ignore into Country (Name, NameHash, Code) values ('Mongolia','MONGOLIA','MN');
insert or ignore into Country (Name, NameHash, Code) values ('Montenegro','MONTENEGRO','ME');
insert or ignore into Country (Name, NameHash, Code) values ('Montserrat','MONTSERRAT','MS');
insert or ignore into Country (Name, NameHash, Code) values ('Morocco','MOROCCO','MA');
insert or ignore into Country (Name, NameHash, Code) values ('Mozambique','MOZAMBIQUE','MZ');
insert or ignore into Country (Name, NameHash, Code) values ('Myanmar','MYANMAR','MM');
insert or ignore into Country (Name, NameHash, Code) values ('Namibia','NAMIBIA','NA');
insert or ignore into Country (Name, NameHash, Code) values ('Nauru','NAURU','NR');
insert or ignore into Country (Name, NameHash, Code) values ('Nepal','NEPAL','NP');
insert or ignore into Country (Name, NameHash, Code) values ('Netherlands','NETHERLANDS','NL');
insert or ignore into Country (Name, NameHash, Code) values ('Netherlands Antilles','NETHERLANDSANTILLES','AN');
insert or ignore into Country (Name, NameHash, Code) values ('New Caledonia','NEWCALEDONIA','NC');
insert or ignore into Country (Name, NameHash, Code) values ('New Zealand','NEWZEALAND','NZ');
insert or ignore into Country (Name, NameHash, Code) values ('Nicaragua','NICARAGUA','NI');
insert or ignore into Country (Name, NameHash, Code) values ('Niger','NIGER','NE');
insert or ignore into Country (Name, NameHash, Code) values ('Nigeria','NIGERIA','NG');
insert or ignore into Country (Name, NameHash, Code) values ('Niue','NIUE','NU');
insert or ignore into Country (Name, NameHash, Code) values ('Norfolk Island','NORFOLKISLAND','NF');
insert or ignore into Country (Name, NameHash, Code) values ('Northern Mariana Islands','NORTHMARIANAISLANDS','MP');
insert or ignore into Country (Name, NameHash, Code) values ('Norway','NORWAY','NO');
insert or ignore into Country (Name, NameHash, Code) values ('Oman','OMAN','OM');
insert or ignore into Country (Name, NameHash, Code) values ('Pakistan','PAKISTAN','PK');
insert or ignore into Country (Name, NameHash, Code) values ('Palau','PALAU','PW');
insert or ignore into Country (Name, NameHash, Code) values ('Palestinian Territory, Occupied','PALESTINIANTERRITORYOCCUPIED','PS');
insert or ignore into Country (Name, NameHash, Code) values ('Panama','PANAMA','PA');
insert or ignore into Country (Name, NameHash, Code) values ('Papua New Guinea','PAPUANEWGUINEA','PG');
insert or ignore into Country (Name, NameHash, Code) values ('Paraguay','PARAGUAY','PY');
insert or ignore into Country (Name, NameHash, Code) values ('Peru','PERU','PE');
insert or ignore into Country (Name, NameHash, Code) values ('Philippines','PHILIPPINES','PH');
insert or ignore into Country (Name, NameHash, Code) values ('Pitcairn','PITCAIRN','PN');
insert or ignore into Country (Name, NameHash, Code) values ('Poland','POLAND','PL');
insert or ignore into Country (Name, NameHash, Code) values ('Portugal','PORTUGAL','PT');
insert or ignore into Country (Name, NameHash, Code) values ('Puerto Rico','PUERTORICO','PR');
insert or ignore into Country (Name, NameHash, Code) values ('Qatar','QATAR','QA');
insert or ignore into Country (Name, NameHash, Code) values ('Réunion','REUNION','RE');
insert or ignore into Country (Name, NameHash, Code) values ('Romania','ROMANIA','RO');
insert or ignore into Country (Name, NameHash, Code) values ('Russian Federation','RUSSIANFEDERATION','RU');
insert or ignore into Country (Name, NameHash, Code) values ('Rwanda','RWANDA','RW');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Barthélemy','SAINTBARTHELEMY','BL');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Helena, Ascension and Tristan da Cunha','SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA','SH');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Kitts and Nevis','SAINTKITTSNEVIS','KN');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Lucia','SAINTLUCIA','LC');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Martin','SAINTMARTIN','MF');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Pierre and Miquelon','SAINTPIERREMIQUELON','PM');
insert or ignore into Country (Name, NameHash, Code) values ('Saint Vincent and the Grenadines','SAINTVINCENTGRENADINES','VC');
insert or ignore into Country (Name, NameHash, Code) values ('Samoa','SAMOA','WS');
insert or ignore into Country (Name, NameHash, Code) values ('San Marino','SANMARINO','SM');
insert or ignore into Country (Name, NameHash, Code) values ('Sao Tome and Principe','SAOTOMEPRINCIPE','ST');
insert or ignore into Country (Name, NameHash, Code) values ('Saudi Arabia','SAUDIARABIA','SA');
insert or ignore into Country (Name, NameHash, Code) values ('Senegal','SENEGAL','SN');
insert or ignore into Country (Name, NameHash, Code) values ('Serbia','SERBIA','RS');
insert or ignore into Country (Name, NameHash, Code) values ('Seychelles','SEYCHELLES','SC');
insert or ignore into Country (Name, NameHash, Code) values ('Sierra Leone','SIERRALEONE','SL');
insert or ignore into Country (Name, NameHash, Code) values ('Singapore','SINGAPORE','SG');
insert or ignore into Country (Name, NameHash, Code) values ('Slovakia','SLOVAKIA','SK');
insert or ignore into Country (Name, NameHash, Code) values ('Slovenia','SLOVENIA','SI');
insert or ignore into Country (Name, NameHash, Code) values ('Solomon Islands','SOLOMONISLANDS','SB');
insert or ignore into Country (Name, NameHash, Code) values ('Somalia','SOMALIA','SO');
insert or ignore into Country (Name, NameHash, Code) values ('South Africa','SOUTHAFRICA','ZA');
insert or ignore into Country (Name, NameHash, Code) values ('South Georgia and the South Sandwich Islands','SOUTHGEORGIASOUTHSANDWICHISLANDS','GS');
insert or ignore into Country (Name, NameHash, Code) values ('Spain','SPAIN','ES');
insert or ignore into Country (Name, NameHash, Code) values ('Sri Lanka','SRILANKA','LK');
insert or ignore into Country (Name, NameHash, Code) values ('Sudan','SUDAN','SD');
insert or ignore into Country (Name, NameHash, Code) values ('Suriname','SURINAME','SR');
insert or ignore into Country (Name, NameHash, Code) values ('Svalbard and Jan Mayen','SVALBARDJANMAYEN','SJ');
insert or ignore into Country (Name, NameHash, Code) values ('Swaziland','SWAZILAND','SZ');
insert or ignore into Country (Name, NameHash, Code) values ('Sweden','SWEDEN','SE');
insert or ignore into Country (Name, NameHash, Code) values ('Switzerland','SWITZERLAND','CH');
insert or ignore into Country (Name, NameHash, Code) values ('Syrian Arab Republic','SYRIANARABREPUBLIC','SY');
insert or ignore into Country (Name, NameHash, Code) values ('Taiwan, Province of China','TAIWANPROVINCECHINA','TW');
insert or ignore into Country (Name, NameHash, Code) values ('Tajikistan','TAJIKISTAN','TJ');
insert or ignore into Country (Name, NameHash, Code) values ('Tanzania, United Republic of','TANZANIAUNITEDREPUBLIC','TZ');
insert or ignore into Country (Name, NameHash, Code) values ('Thailand','THAILAND','TH');
insert or ignore into Country (Name, NameHash, Code) values ('Timor-Leste','TIMORLESTE','TL');
insert or ignore into Country (Name, NameHash, Code) values ('Togo','TOGO','TG');
insert or ignore into Country (Name, NameHash, Code) values ('Tokelau','TOKELAU','TK');
insert or ignore into Country (Name, NameHash, Code) values ('Tonga','TONGA','TO');
insert or ignore into Country (Name, NameHash, Code) values ('Trinidad and Tobago','TRINIDADTOBAGO','TT');
insert or ignore into Country (Name, NameHash, Code) values ('Tunisia','TUNISIA','TN');
insert or ignore into Country (Name, NameHash, Code) values ('Turkey','TURKEY','TR');
insert or ignore into Country (Name, NameHash, Code) values ('Turkmenistan','TURKMENISTAN','TM');
insert or ignore into Country (Name, NameHash, Code) values ('Turks and Caicos Islands','TURKSCAICOSISLANDS','TC');
insert or ignore into Country (Name, NameHash, Code) values ('Tuvalu','TUVALU','TV');
insert or ignore into Country (Name, NameHash, Code) values ('Uganda','UGANDA','UG');
insert or ignore into Country (Name, NameHash, Code) values ('Ukraine','UKRAINE','UA');
insert or ignore into Country (Name, NameHash, Code) values ('United Arab Emirates','UNITEDARABEMIRATES','AE');
insert or ignore into Country (Name, NameHash, Code) values ('United Kingdom','UNITEDKINGDOM','GB');
insert or ignore into Country (Name, NameHash, Code) values ('United States of America', 'UNITEDSTATESAMERICA','US');
insert or ignore into Country (Name, NameHash, Code) values ('United States Minor Outlying Islands','UNITEDSTATESMINOROUTLYINGISLANDS','UM');
insert or ignore into Country (Name, NameHash, Code) values ('Uruguay','URUGUAY','UY');
insert or ignore into Country (Name, NameHash, Code) values ('Uzbekistan','UZBEKISTAN','UZ');
insert or ignore into Country (Name, NameHash, Code) values ('Vanuatu','VANUATU','VU');
insert or ignore into Country (Name, NameHash, Code) values ('Venezuela, Bolivarian Republic of','VENEZUELABOLIVARIANREPUBLIC','VE');
insert or ignore into Country (Name, NameHash, Code) values ('Viet Nam','VIETNAM','VN');
insert or ignore into Country (Name, NameHash, Code) values ('Virgin Islands, British','VIRGINISLANDSBRITISH','VG');
insert or ignore into Country (Name, NameHash, Code) values ('Virgin Islands, U.S.','VIRGINISLANDSUS','VI');
insert or ignore into Country (Name, NameHash, Code) values ('Wallis and Futuna','WALLISFUTUNA','WF');
insert or ignore into Country (Name, NameHash, Code) values ('Western Sahara','WESTSAHARA','EH');
insert or ignore into Country (Name, NameHash, Code) values ('Yemen','YEMEN','YE');
insert or ignore into Country (Name, NameHash, Code) values ('Zambia','ZAMBIA','ZM');
insert or ignore into Country (Name, NameHash, Code) values ('Zimbabwe','ZIMBABWE','ZW');")
            .ToCommand();
 	    }

        protected override IDictionary<string, object> GetPersistenceMap(ICountry model)
        {
            return new Dictionary<string, object> { 
                {"Id", model.Id},
                {"Name", model.Name},
                {"NameHash", model.NameHash},
                {"Abbreviation", model.Abbreviation},
                {"Code", model.Code}
            };
        }
    }
}
