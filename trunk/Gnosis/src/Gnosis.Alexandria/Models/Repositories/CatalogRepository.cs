using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Gnosis.Alexandria.Models.Repositories
{
    /*
    public class CatalogRepository
        : RepositoryBase
    {
        public CatalogRepository()
        {
            AddBuilder<ICountry>(allCountriesBuilder);
        }

        private static readonly ICommandBuilder allCountriesBuilder = new SelectTextBuilder("Country");

        #region Overrides

        protected override void InitializeRepository()
        {
            var countries = GetCollection<ICountry>(allCountriesBuilder);
            Country.InitializeCache(countries);
        }

        protected override string Name
        {
            get { return "Catalog"; }
        }

        protected override string GetInitializeCommandText()
        {
            return
@"
create table if not exists ContentType (
	Id integer not null primary key autoincrement,
	Name text not null unique,
	Code text not null unique
);
create index if not exists ContentType_index_Code on ContentType (Code);
insert or ignore into ContentType (Name, Code) values ('Application', 'application');
insert or ignore into ContentType (Name, Code) values ('Audio', 'audio');
insert or ignore into ContentType (Name, Code) values ('Image', 'image');
insert or ignore into ContentType (Name, Code) values ('Message', 'message');
insert or ignore into ContentType (Name, Code) values ('3D Model', 'model');
insert or ignore into ContentType (Name, Code) values ('Multipart', 'multipart');
insert or ignore into ContentType (Name, Code) values ('Text', 'text');
insert or ignore into ContentType (Name, Code) values ('Video', 'video');

create table if not exists MediaType (
	Id integer not null primary key autoincrement,
    ContentType integer not null default 1,
	Name text not null unique,
	ContentSubtype text not null,
	constraint MediaType_unique_ContentTypeContentSubtype unique (ContentType, ContentSubtype)
);
create index if not exists MediaType_index_ContentType on MediaType (ContentType);
insert or ignore into MediaType (Name, ContentSubtype) values ('Unknown', 'unknown');
insert or ignore into MediaType (ContentType, Name, ContentSubtype) values (2, 'MPEG Layer 3 Audio', 'mpeg');
insert or ignore into MediaType (ContentType, Name, ContentSubtype) values (2, 'Ogg Vorbis Audio', 'ogg');
insert or ignore into MediaType (ContentType, Name, ContentSubtype) values (2, 'Free Lossless Audio Codec', 'x-flac');
insert or ignore into MediaType (ContentType, Name, ContentSubtype) values (3, 'Joint Photographic Experts Group Image', 'jpeg');

create table if not exists Media (
	Id integer not null primary key autoincrement,
	Type integer not null default 1,
Path text not null unique
);

create table if not exists FileExtension (
	Id integer not null primary key autoincrement,
	MediaType integer not null,
	Name text not null unique
);
insert or ignore into FileExtension (MediaType, Name) values (2, 'mp3');
insert or ignore into FileExtension (MediaType, Name) values (3, 'ogg');
insert or ignore into FileExtension (MediaType, Name) values (4, 'flac');
insert or ignore into FileExtension (MediaType, Name) values (5, 'jpg');
insert or ignore into FileExtension (MediaType, Name) values (5, 'jpeg');

create table if not exists MediaFunction (
	Id integer not null primary key autoincrement,
	Name text not null unique
);
insert or ignore into MediaFunction (Name) values ('Unknown');
insert or ignore into MediaFunction (Name) values ('Play Audio');
insert or ignore into MediaFunction (Name) values ('Play Video');
insert or ignore into MediaFunction (Name) values ('Display Image');
insert or ignore into MediaFunction (Name) values ('Display Text');
insert or ignore into MediaFunction (Name) values ('Browse Web Site');
insert or ignore into MediaFunction (Name) values ('Download File');

create table if not exists TagCategory (
	Id integer not null primary key autoincrement,
	Name text not null unique
);
insert or ignore into TagCategory (Name) values ('Unknown');
insert or ignore into TagCategory (Name) values ('None');
insert or ignore into TagCategory (Name) values ('Genre');
insert or ignore into TagCategory (Name) values ('Style');
insert or ignore into TagCategory (Name) values ('Mood');
insert or ignore into TagCategory (Name) values ('User Rating');
insert or ignore into TagCategory (Name) values ('Cover Of');
insert or ignore into TagCategory (Name) values ('Remix Of');
insert or ignore into TagCategory (Name) values ('Song Cycle');
insert or ignore into TagCategory (Name) values ('Producer');
insert or ignore into TagCategory (Name) values ('Member Of');
insert or ignore into TagCategory (Name) values ('Influenced By');
insert or ignore into TagCategory (Name) values ('Featuring');

create table if not exists Country (
	Id integer not null primary key autoincrement,
	Name text not null unique,
    Hash text not null unique,
	Code text not null unique
);
create index if not exists Country_index_Name on Country (Name);
create index if not exists Country_index_Hash on Country (Hash);
create index if not exists Country_index_Code on Country (Code);
insert or ignore into Country (Name, Hash, Code) values ('Unknown', 'UNKNOWN', 'XA');
insert or ignore into Country (Name, Hash, Code) values ('Afghanistan', 'AFGHANISTAN', 'AF');
insert or ignore into Country (Name, Hash, Code) values ('Åland Islands', 'ALANDISLANDS','AX');
insert or ignore into Country (Name, Hash, Code) values ('Albania', 'ALBANIA','AL');
insert or ignore into Country (Name, Hash, Code) values ('Algeria', 'ALGERIA','DZ');
insert or ignore into Country (Name, Hash, Code) values ('American Samoa', 'AMERICANSAMOA','AS');
insert or ignore into Country (Name, Hash, Code) values ('Andorra', 'ANDORRA','AD');
insert or ignore into Country (Name, Hash, Code) values ('Angola','ANGOLA','AO');
insert or ignore into Country (Name, Hash, Code) values ('Anguilla','ANGUILLA','AI');
insert or ignore into Country (Name, Hash, Code) values ('Antarctica','ANTARCTICA','AQ');
insert or ignore into Country (Name, Hash, Code) values ('Antigua and Barbuda','ANTIGUABARBUDA','AG');
insert or ignore into Country (Name, Hash, Code) values ('Argentina', 'ARGENTINA','AR');
insert or ignore into Country (Name, Hash, Code) values ('Armenia', 'ARMENIA','AM');
insert or ignore into Country (Name, Hash, Code) values ('Aruba', 'ARUBA','AW');
insert or ignore into Country (Name, Hash, Code) values ('Australia', 'AUSTRALIA','AU');
insert or ignore into Country (Name, Hash, Code) values ('Austria','AUSTRIA','AT');
insert or ignore into Country (Name, Hash, Code) values ('Azerbaijan','AZERBAIJAN','AZ');
insert or ignore into Country (Name, Hash, Code) values ('Bahamas','BAHAMAS','BS');
insert or ignore into Country (Name, Hash, Code) values ('Bahrain','BAHRAIN','BH');
insert or ignore into Country (Name, Hash, Code) values ('Bangladesh','BANGLADESH','BD');
insert or ignore into Country (Name, Hash, Code) values ('Barbados','BARBADOS','BB');
insert or ignore into Country (Name, Hash, Code) values ('Belarus','BELARUS','BY');
insert or ignore into Country (Name, Hash, Code) values ('Belgium','BELGIUM','BE');
insert or ignore into Country (Name, Hash, Code) values ('Belize','BELIZE','BZ');
insert or ignore into Country (Name, Hash, Code) values ('Benin','BENIN','BJ');
insert or ignore into Country (Name, Hash, Code) values ('Bermuda','BERMUDA','BM');
insert or ignore into Country (Name, Hash, Code) values ('Bhutan','BHUTAN','BT');
insert or ignore into Country (Name, Hash, Code) values ('Bolivia, Plurinational State of','BOLIVIAPLURINATIONALSTATE','BO');
insert or ignore into Country (Name, Hash, Code) values ('Bosnia and Herzegovina','BOSNIAHERZEGOVINA','BA');
insert or ignore into Country (Name, Hash, Code) values ('Botswana','BOTSWANA','BW');
insert or ignore into Country (Name, Hash, Code) values ('Bouvet Island','BOUVETISLAND','BV');
insert or ignore into Country (Name, Hash, Code) values ('Brazil','BRAZIL','BR');
insert or ignore into Country (Name, Hash, Code) values ('British Indian Ocean Territory','BRITISHINDIANOCEANTERRITORY','IO');
insert or ignore into Country (Name, Hash, Code) values ('Brunei Darussalam', 'BRUNEIDARUSSALAM','BN');
insert or ignore into Country (Name, Hash, Code) values ('Bulgaria','BULGARIA','BG');
insert or ignore into Country (Name, Hash, Code) values ('Burkina Faso','BURKINAFASO','BF');
insert or ignore into Country (Name, Hash, Code) values ('Burundi','BURUNDI','BI');
insert or ignore into Country (Name, Hash, Code) values ('Cambodia','CAMBODIA','KH');
insert or ignore into Country (Name, Hash, Code) values ('Cameroon','CAMEROON','CM');
insert or ignore into Country (Name, Hash, Code) values ('Canada','CANADA','CA');
insert or ignore into Country (Name, Hash, Code) values ('Cape Verde','CAPEVERDE','CV');
insert or ignore into Country (Name, Hash, Code) values ('Cayman Islands','CAYMANISLANDS','KY');
insert or ignore into Country (Name, Hash, Code) values ('Central African Republic','CENTRALAFRICANREPUBLIC','CF');
insert or ignore into Country (Name, Hash, Code) values ('Chad','CHAD','TD');
insert or ignore into Country (Name, Hash, Code) values ('Chile','CHILE','CL');
insert or ignore into Country (Name, Hash, Code) values ('China','CHINA','CN');
insert or ignore into Country (Name, Hash, Code) values ('Christmas Island','CHRISTMASISLAND','CX');
insert or ignore into Country (Name, Hash, Code) values ('Cocos (Keeling) Islands','COCOSKEELINGISLANDS','CC');
insert or ignore into Country (Name, Hash, Code) values ('Colombia','COLOMBIA','CO');
insert or ignore into Country (Name, Hash, Code) values ('Comoros','COMOROS','KM');
insert or ignore into Country (Name, Hash, Code) values ('Congo','CONGO','CG');
insert or ignore into Country (Name, Hash, Code) values ('Congo, The Democratic Republic of the','CONGODEMOCRATICREPUBLIC','CD');
insert or ignore into Country (Name, Hash, Code) values ('Cook Islands','COOKISLANDS','CK');
insert or ignore into Country (Name, Hash, Code) values ('Costa Rica','COSTARICA','CR');
insert or ignore into Country (Name, Hash, Code) values ('Côte d''Ivoire','COTEDIVOIRE','CI');
insert or ignore into Country (Name, Hash, Code) values ('Croatia','CROATIA','HR');
insert or ignore into Country (Name, Hash, Code) values ('Cuba','CUBA','CU');
insert or ignore into Country (Name, Hash, Code) values ('Cyprus','CYPRUS','CY');
insert or ignore into Country (Name, Hash, Code) values ('Czech Republic','CZECHREPUBLIC','CZ');
insert or ignore into Country (Name, Hash, Code) values ('Denmark','DENMARK','DK');
insert or ignore into Country (Name, Hash, Code) values ('Djibouti','DJIBOUTI','DJ');
insert or ignore into Country (Name, Hash, Code) values ('Dominica','DOMINICA','DM');
insert or ignore into Country (Name, Hash, Code) values ('Dominican Republic','DOMINICANREPUBLIC','DO');
insert or ignore into Country (Name, Hash, Code) values ('Ecuador','ECUADOR','EC');
insert or ignore into Country (Name, Hash, Code) values ('Egypt','EGYPT','EG');
insert or ignore into Country (Name, Hash, Code) values ('El Salvador','SALVADOR','SV');
insert or ignore into Country (Name, Hash, Code) values ('Equatorial Guinea','EQUATORIALGUINEA','GQ');
insert or ignore into Country (Name, Hash, Code) values ('Eritrea','ERITREA','ER');
insert or ignore into Country (Name, Hash, Code) values ('Estonia','ESTONIA','EE');
insert or ignore into Country (Name, Hash, Code) values ('Ethiopa','ETHIOPIA','ET');
insert or ignore into Country (Name, Hash, Code) values ('Falkland Islands (Malvinas)','FALKLANDISLANDSMALVINAS','FK');
insert or ignore into Country (Name, Hash, Code) values ('Faroe Islands','FAROEISLANDS','FO');
insert or ignore into Country (Name, Hash, Code) values ('Fiji','FIJI','FJ');
insert or ignore into Country (Name, Hash, Code) values ('Finland','FINLAND','FI');
insert or ignore into Country (Name, Hash, Code) values ('France','FRANCE','FR');
insert or ignore into Country (Name, Hash, Code) values ('French Guiana','FRENCHGUIANA','GF');
insert or ignore into Country (Name, Hash, Code) values ('French Polynesia','FRENCHPOLYNESIA','PF');
insert or ignore into Country (Name, Hash, Code) values ('French Southern Territories','FRENCHSOUTHTERRITORIES','TF');
insert or ignore into Country (Name, Hash, Code) values ('Gabon','GABON','GA');
insert or ignore into Country (Name, Hash, Code) values ('Gambia','GAMBIA','GM');
insert or ignore into Country (Name, Hash, Code) values ('Georgia','GEORGIA','GE');
insert or ignore into Country (Name, Hash, Code) values ('Germany','GERMANY','DE');
insert or ignore into Country (Name, Hash, Code) values ('Ghana','GHANA','GH');
insert or ignore into Country (Name, Hash, Code) values ('Gibraltar','GIBRALTAR','GI');
insert or ignore into Country (Name, Hash, Code) values ('Greece','GREECE','GR');
insert or ignore into Country (Name, Hash, Code) values ('Greenland','GREENLAND','GL');
insert or ignore into Country (Name, Hash, Code) values ('Grenada','GRENADA','GD');
insert or ignore into Country (Name, Hash, Code) values ('Guadeloupe','GUADELOUPE','GP');
insert or ignore into Country (Name, Hash, Code) values ('Guam','GUAM','GU');
insert or ignore into Country (Name, Hash, Code) values ('Guatemala','GUATEMALA','GT');
insert or ignore into Country (Name, Hash, Code) values ('Guernsey','GUERNSEY','GG');
insert or ignore into Country (Name, Hash, Code) values ('Guinea','GUINEA','GN');
insert or ignore into Country (Name, Hash, Code) values ('Guinea-Bissau','GUINEABISSAU','GW');
insert or ignore into Country (Name, Hash, Code) values ('Guyana','GUYANA','GY');
insert or ignore into Country (Name, Hash, Code) values ('Haiti','HAITI','HT');
insert or ignore into Country (Name, Hash, Code) values ('Heard Island and McDonald Islands','HEARDISLANDMCDONALDISLANDS','HM');
insert or ignore into Country (Name, Hash, Code) values ('Holy See (Vatican City State)','HOLYSEEVATICANCITYSTATE','VA');
insert or ignore into Country (Name, Hash, Code) values ('Honduras','HONDURAS','HN');
insert or ignore into Country (Name, Hash, Code) values ('Hong Kong','HONGKONG','HK');
insert or ignore into Country (Name, Hash, Code) values ('Hungary','HUNGARY','HU');
insert or ignore into Country (Name, Hash, Code) values ('Iceland','ICELAND','IS');
insert or ignore into Country (Name, Hash, Code) values ('India','INDIA','IN');
insert or ignore into Country (Name, Hash, Code) values ('Indonesia','INDONESIA','ID');
insert or ignore into Country (Name, Hash, Code) values ('Iran, Islamic Republic of','IRANISLAMICREPUBLIC','IR');
insert or ignore into Country (Name, Hash, Code) values ('Iraq','IRAQ','IQ');
insert or ignore into Country (Name, Hash, Code) values ('Ireland','IRELAND','IE');
insert or ignore into Country (Name, Hash, Code) values ('Isle of Man','ISLEMAN','IM');
insert or ignore into Country (Name, Hash, Code) values ('Israel','ISRAEL','IL');
insert or ignore into Country (Name, Hash, Code) values ('Italy','ITALY','IT');
insert or ignore into Country (Name, Hash, Code) values ('Jamaica','JAMAICA','JM');
insert or ignore into Country (Name, Hash, Code) values ('Japan','JAPAN','JP');
insert or ignore into Country (Name, Hash, Code) values ('Jersey','JERSEY','JE');
insert or ignore into Country (Name, Hash, Code) values ('Jordan','JORDAN','JO');
insert or ignore into Country (Name, Hash, Code) values ('Kazakhstan','KAZAKHSTAN','KZ');
insert or ignore into Country (Name, Hash, Code) values ('Kenya','KENYA','KE');
insert or ignore into Country (Name, Hash, Code) values ('Kiribati','KIRIBATI','KI');
insert or ignore into Country (Name, Hash, Code) values ('Korea, Democratic People''s Republic of','KOREADEMOCRATICPEOPLESREPUBLIC','KP');
insert or ignore into Country (Name, Hash, Code) values ('Korea, Republic of (South Korea)','KOREAREPUBLICSOUTHKOREA','KR');
insert or ignore into Country (Name, Hash, Code) values ('Kuwait','KUWAIT','KW');
insert or ignore into Country (Name, Hash, Code) values ('Kyrgyzstan','KYRGYZSTAN','KG');
insert or ignore into Country (Name, Hash, Code) values ('Loa People''s Democratic Republic','LAOPEOPLESDEMOCRATICREPUBLIC','LA');
insert or ignore into Country (Name, Hash, Code) values ('Latvia','LATVIA','LV');
insert or ignore into Country (Name, Hash, Code) values ('Lebanon','LEBANON','LB');
insert or ignore into Country (Name, Hash, Code) values ('Lesotho','LESOTHO','LS');
insert or ignore into Country (Name, Hash, Code) values ('Liberia','LIBERIA','LR');
insert or ignore into Country (Name, Hash, Code) values ('Libyan Arab Jamahiriya','LIBYANARABJAMAHIRIYA','LY');
insert or ignore into Country (Name, Hash, Code) values ('Liechtenstein','LIECHTENSTEIN','LI');
insert or ignore into Country (Name, Hash, Code) values ('Lithuania','LITHUANIA','LT');
insert or ignore into Country (Name, Hash, Code) values ('Luxemboug','LUXEMBOURG','LU');
insert or ignore into Country (Name, Hash, Code) values ('Macao','MACAO','MO');
insert or ignore into Country (Name, Hash, Code) values ('Macedonia, The Former Yugoslav Republic of','MACEDONIAFORMERYUGOSLAVREPUBLIC','MK');
insert or ignore into Country (Name, Hash, Code) values ('Madagascar','MADAGASCAR','MG');
insert or ignore into Country (Name, Hash, Code) values ('Malawi','MALAWI','MW');
insert or ignore into Country (Name, Hash, Code) values ('Malaysia','MALAYSIA','MY');
insert or ignore into Country (Name, Hash, Code) values ('Maldives','MALDIVES','MV');
insert or ignore into Country (Name, Hash, Code) values ('Mali','MALI','ML');
insert or ignore into Country (Name, Hash, Code) values ('Malta','MALTA','MT');
insert or ignore into Country (Name, Hash, Code) values ('Marshall Islands','MARSHALLISLANDS','MH');
insert or ignore into Country (Name, Hash, Code) values ('Martinique', 'MARTINIQUE','MQ');
insert or ignore into Country (Name, Hash, Code) values ('Mauritania','MAURITANIA','MR');
insert or ignore into Country (Name, Hash, Code) values ('Mauritius','MAURITIUS','MU');
insert or ignore into Country (Name, Hash, Code) values ('Mayotte','MAYOTTE','YT');
insert or ignore into Country (Name, Hash, Code) values ('Mexico','MEXICO','MX');
insert or ignore into Country (Name, Hash, Code) values ('Micronesia, Federated State of','MICRONESIAFEDERATEDSTATES','FM');
insert or ignore into Country (Name, Hash, Code) values ('Moldova, Republic of','MOLDOVAREPUBLIC','MD');
insert or ignore into Country (Name, Hash, Code) values ('Monaco','MONACO','MC');
insert or ignore into Country (Name, Hash, Code) values ('Mongolia','MONGOLIA','MN');
insert or ignore into Country (Name, Hash, Code) values ('Montenegro','MONTENEGRO','ME');
insert or ignore into Country (Name, Hash, Code) values ('Montserrat','MONTSERRAT','MS');
insert or ignore into Country (Name, Hash, Code) values ('Morocco','MOROCCO','MA');
insert or ignore into Country (Name, Hash, Code) values ('Mozambique','MOZAMBIQUE','MZ');
insert or ignore into Country (Name, Hash, Code) values ('Myanmar','MYANMAR','MM');
insert or ignore into Country (Name, Hash, Code) values ('Namibia','NAMIBIA','NA');
insert or ignore into Country (Name, Hash, Code) values ('Nauru','NAURU','NR');
insert or ignore into Country (Name, Hash, Code) values ('Nepal','NEPAL','NP');
insert or ignore into Country (Name, Hash, Code) values ('Netherlands','NETHERLANDS','NL');
insert or ignore into Country (Name, Hash, Code) values ('Netherlands Antilles','NETHERLANDSANTILLES','AN');
insert or ignore into Country (Name, Hash, Code) values ('New Caledonia','NEWCALEDONIA','NC');
insert or ignore into Country (Name, Hash, Code) values ('New Zealand','NEWZEALAND','NZ');
insert or ignore into Country (Name, Hash, Code) values ('Nicaragua','NICARAGUA','NI');
insert or ignore into Country (Name, Hash, Code) values ('Niger','NIGER','NE');
insert or ignore into Country (Name, Hash, Code) values ('Nigeria','NIGERIA','NG');
insert or ignore into Country (Name, Hash, Code) values ('Niue','NIUE','NU');
insert or ignore into Country (Name, Hash, Code) values ('Norfolk Island','NORFOLKISLAND','NF');
insert or ignore into Country (Name, Hash, Code) values ('Northern Mariana Islands','NORTHMARIANAISLANDS','MP');
insert or ignore into Country (Name, Hash, Code) values ('Norway','NORWAY','NO');
insert or ignore into Country (Name, Hash, Code) values ('Oman','OMAN','OM');
insert or ignore into Country (Name, Hash, Code) values ('Pakistan','PAKISTAN','PK');
insert or ignore into Country (Name, Hash, Code) values ('Palau','PALAU','PW');
insert or ignore into Country (Name, Hash, Code) values ('Palestinian Territory, Occupied','PALESTINIANTERRITORYOCCUPIED','PS');
insert or ignore into Country (Name, Hash, Code) values ('Panama','PANAMA','PA');
insert or ignore into Country (Name, Hash, Code) values ('Papua New Guinea','PAPUANEWGUINEA','PG');
insert or ignore into Country (Name, Hash, Code) values ('Paraguay','PARAGUAY','PY');
insert or ignore into Country (Name, Hash, Code) values ('Peru','PERU','PE');
insert or ignore into Country (Name, Hash, Code) values ('Philippines','PHILIPPINES','PH');
insert or ignore into Country (Name, Hash, Code) values ('Pitcairn','PITCAIRN','PN');
insert or ignore into Country (Name, Hash, Code) values ('Poland','POLAND','PL');
insert or ignore into Country (Name, Hash, Code) values ('Portugal','PORTUGAL','PT');
insert or ignore into Country (Name, Hash, Code) values ('Puerto Rico','PUERTORICO','PR');
insert or ignore into Country (Name, Hash, Code) values ('Qatar','QATAR','QA');
insert or ignore into Country (Name, Hash, Code) values ('Réunion','REUNION','RE');
insert or ignore into Country (Name, Hash, Code) values ('Romania','ROMANIA','RO');
insert or ignore into Country (Name, Hash, Code) values ('Russian Federation','RUSSIANFEDERATION','RU');
insert or ignore into Country (Name, Hash, Code) values ('Rwanda','RWANDA','RW');
insert or ignore into Country (Name, Hash, Code) values ('Saint Barthélemy','SAINTBARTHELEMY','BL');
insert or ignore into Country (Name, Hash, Code) values ('Saint Helena, Ascension and Tristan da Cunha','SAINT HELENA, ASCENSION AND TRISTAN DA CUNHA','SH');
insert or ignore into Country (Name, Hash, Code) values ('Saint Kitts and Nevis','SAINTKITTSNEVIS','KN');
insert or ignore into Country (Name, Hash, Code) values ('Saint Lucia','SAINTLUCIA','LC');
insert or ignore into Country (Name, Hash, Code) values ('Saint Martin','SAINTMARTIN','MF');
insert or ignore into Country (Name, Hash, Code) values ('Saint Pierre and Miquelon','SAINTPIERREMIQUELON','PM');
insert or ignore into Country (Name, Hash, Code) values ('Saint Vincent and the Grenadines','SAINTVINCENTGRENADINES','VC');
insert or ignore into Country (Name, Hash, Code) values ('Samoa','SAMOA','WS');
insert or ignore into Country (Name, Hash, Code) values ('San Marino','SANMARINO','SM');
insert or ignore into Country (Name, Hash, Code) values ('Sao Tome and Principe','SAOTOMEPRINCIPE','ST');
insert or ignore into Country (Name, Hash, Code) values ('Saudi Arabia','SAUDIARABIA','SA');
insert or ignore into Country (Name, Hash, Code) values ('Senegal','SENEGAL','SN');
insert or ignore into Country (Name, Hash, Code) values ('Serbia','SERBIA','RS');
insert or ignore into Country (Name, Hash, Code) values ('Seychelles','SEYCHELLES','SC');
insert or ignore into Country (Name, Hash, Code) values ('Sierra Leone','SIERRALEONE','SL');
insert or ignore into Country (Name, Hash, Code) values ('Singapore','SINGAPORE','SG');
insert or ignore into Country (Name, Hash, Code) values ('Slovakia','SLOVAKIA','SK');
insert or ignore into Country (Name, Hash, Code) values ('Slovenia','SLOVENIA','SI');
insert or ignore into Country (Name, Hash, Code) values ('Solomon Islands','SOLOMONISLANDS','SB');
insert or ignore into Country (Name, Hash, Code) values ('Somalia','SOMALIA','SO');
insert or ignore into Country (Name, Hash, Code) values ('South Africa','SOUTHAFRICA','ZA');
insert or ignore into Country (Name, Hash, Code) values ('South Georgia and the South Sandwich Islands','SOUTHGEORGIASOUTHSANDWICHISLANDS','GS');
insert or ignore into Country (Name, Hash, Code) values ('Spain','SPAIN','ES');
insert or ignore into Country (Name, Hash, Code) values ('Sri Lanka','SRILANKA','LK');
insert or ignore into Country (Name, Hash, Code) values ('Sudan','SUDAN','SD');
insert or ignore into Country (Name, Hash, Code) values ('Suriname','SURINAME','SR');
insert or ignore into Country (Name, Hash, Code) values ('Svalbard and Jan Mayen','SVALBARDJANMAYEN','SJ');
insert or ignore into Country (Name, Hash, Code) values ('Swaziland','SWAZILAND','SZ');
insert or ignore into Country (Name, Hash, Code) values ('Sweden','SWEDEN','SE');
insert or ignore into Country (Name, Hash, Code) values ('Switzerland','SWITZERLAND','CH');
insert or ignore into Country (Name, Hash, Code) values ('Syrian Arab Republic','SYRIANARABREPUBLIC','SY');
insert or ignore into Country (Name, Hash, Code) values ('Taiwan, Province of China','TAIWANPROVINCECHINA','TW');
insert or ignore into Country (Name, Hash, Code) values ('Tajikistan','TAJIKISTAN','TJ');
insert or ignore into Country (Name, Hash, Code) values ('Tanzania, United Republic of','TANZANIAUNITEDREPUBLIC','TZ');
insert or ignore into Country (Name, Hash, Code) values ('Thailand','THAILAND','TH');
insert or ignore into Country (Name, Hash, Code) values ('Timor-Leste','TIMORLESTE','TL');
insert or ignore into Country (Name, Hash, Code) values ('Togo','TOGO','TG');
insert or ignore into Country (Name, Hash, Code) values ('Tokelau','TOKELAU','TK');
insert or ignore into Country (Name, Hash, Code) values ('Tonga','TONGA','TO');
insert or ignore into Country (Name, Hash, Code) values ('Trinidad and Tobago','TRINIDADTOBAGO','TT');
insert or ignore into Country (Name, Hash, Code) values ('Tunisia','TUNISIA','TN');
insert or ignore into Country (Name, Hash, Code) values ('Turkey','TURKEY','TR');
insert or ignore into Country (Name, Hash, Code) values ('Turkmenistan','TURKMENISTAN','TM');
insert or ignore into Country (Name, Hash, Code) values ('Turks and Caicos Islands','TURKSCAICOSISLANDS','TC');
insert or ignore into Country (Name, Hash, Code) values ('Tuvalu','TUVALU','TV');
insert or ignore into Country (Name, Hash, Code) values ('Uganda','UGANDA','UG');
insert or ignore into Country (Name, Hash, Code) values ('Ukraine','UKRAINE','UA');
insert or ignore into Country (Name, Hash, Code) values ('United Arab Emirates','UNITEDARABEMIRATES','AE');
insert or ignore into Country (Name, Hash, Code) values ('United Kingdom','UNITEDKINGDOM','GB');
insert or ignore into Country (Name, Hash, Code) values ('United States of America', 'UNITEDSTATESAMERICA','US');
insert or ignore into Country (Name, Hash, Code) values ('United States Minor Outlying Islands','UNITEDSTATESMINOROUTLYINGISLANDS','UM');
insert or ignore into Country (Name, Hash, Code) values ('Uruguay','URUGUAY','UY');
insert or ignore into Country (Name, Hash, Code) values ('Uzbekistan','UZBEKISTAN','UZ');
insert or ignore into Country (Name, Hash, Code) values ('Vanuatu','VANUATU','VU');
insert or ignore into Country (Name, Hash, Code) values ('Venezuela, Bolivarian Republic of','VENEZUELABOLIVARIANREPUBLIC','VE');
insert or ignore into Country (Name, Hash, Code) values ('Viet Nam','VIETNAM','VN');
insert or ignore into Country (Name, Hash, Code) values ('Virgin Islands, British','VIRGINISLANDSBRITISH','VG');
insert or ignore into Country (Name, Hash, Code) values ('Virgin Islands, U.S.','VIRGINISLANDSUS','VI');
insert or ignore into Country (Name, Hash, Code) values ('Wallis and Futuna','WALLISFUTUNA','WF');
insert or ignore into Country (Name, Hash, Code) values ('Western Sahara','WESTSAHARA','EH');
insert or ignore into Country (Name, Hash, Code) values ('Yemen','YEMEN','YE');
insert or ignore into Country (Name, Hash, Code) values ('Zambia','ZAMBIA','ZM');
insert or ignore into Country (Name, Hash, Code) values ('Zimbabwe','ZIMBABWE','ZW');


create table if not exists Artist (
	Id integer not null primary key autoincrement,
	Country int not null default 1,
    Name text not null,
	Hash text not null,
    Abbreviation text not null default '',
	Note text not null default '',
	StartDate text not null default '0001-01-01',
	EndDate text not null default '9999-12-31',
	constraint Artist_unique_CountryNameStartDate unique (Country, Name, StartDate)
);
create index if not exists Artist_index_Name on Artist (Name);
create index if not exists Artist_index_Hash on Artist (Hash);
insert or ignore into Artist (Name, Hash) values ('Unknown', 'UNKNOWN');
insert or ignore into Artist (Name, Hash) values ('None', 'NONE');
insert or ignore into Artist (Name, Hash) values ('Various', 'VARIOUS');

create table if not exists ArtistMedia (
	Id integer not null primary key autoincrement,
	Subject integer not null,
	Media integer not null,
	Function integer not null default 1,
	constraint ArtistMedia_unique_SubjectMediaFunction unique (Subject, Media, Function)
);

create table if not exists ArtistTag (
	Id integer not null primary key autoincrement,
	Subject integer not null,
	Category integer not null default 1,
	Name text not null,
	Hash text not null,
	constraint ArtistTag_unique_SubjectCategoryName unique (Subject, Category, Name)
);
create index if not exists ArtistTag_index_Subject on ArtistTag (Subject);
create index if not exists ArtistTag_index_Category on ArtistTag (Category);
create index if not exists ArtistTag_index_Name on ArtistTag (Name);
create index if not exists ArtistTag_index_Hash on ArtistTag (Hash);

create table if not exists AlbumType (
	Id integer not null primary key autoincrement,
	Name text not null unique
);
insert or ignore into AlbumType (Name) values ('Unknown');
insert or ignore into AlbumType (Name) values ('None');
insert or ignore into AlbumType (Name) values ('Other');
insert or ignore into AlbumType (Name) values ('Cast Recording');
insert or ignore into AlbumType (Name) values ('Compilation');
insert or ignore into AlbumType (Name) values ('Demo');
insert or ignore into AlbumType (Name) values ('Film Score');
insert or ignore into AlbumType (Name) values ('Greatest Hits');
insert or ignore into AlbumType (Name) values ('Live');
insert or ignore into AlbumType (Name) values ('Rarities');
insert or ignore into AlbumType (Name) values ('Remix');
insert or ignore into AlbumType (Name) values ('Sampler');
insert or ignore into AlbumType (Name) values ('Soundtrack');
insert or ignore into AlbumType (Name) values ('Split');
insert or ignore into AlbumType (Name) values ('Studio');
insert or ignore into AlbumType (Name) values ('Tribute');

create table if not exists Album (
	Id integer not null primary key autoincrement,
    Type integer not null default 1,
    Artist integer not null default 1,
    Country integer not null default 1,
	Name text not null,
    ReleaseDate text not null default '0001-01-01',
	Hash text not null,
    Abbreviation text not null default '',
	Note text not null default '',
	NumberOfDiscs integer not null default 1,
	constraint Album_unique_ArtistCountryNameReleaseDate unique (Artist, Country, Name, ReleaseDate)
);
create index if not exists Album_index_Country on Album (Country);
create index if not exists Album_index_Name on Album (Name);
create index if not exists Album_index_Hash on Album (Hash);
insert or ignore into Album (Name, Hash) values ('Unknown', 'UNKNOWN');
insert or ignore into Album (Name, Hash) values ('None', 'NONE');

create table if not exists AlbumMedia (
	Id integer not null primary key autoincrement,
	Subject integer not null,
	Media integer not null,
	Function integer not null default 1,
	constraint AlbumMedia_unique_SubjectMediaFunction unique (Subject, Media, Function)
);

create table if not exists AlbumTag (
	Id integer not null primary key autoincrement,
	Subject integer not null,
	Category integer not null default 1,
	Name text not null,
	Hash text not null,
	constraint AlbumTag_unqiue_SubjectCategoryName unique (Subject, Category, Name)
);
create index if not exists AlbumTag_index_Subject on AlbumTag (Subject);
create index if not exists AlbumTag_index_Category on AlbumTag (Category);
create index if not exists AlbumTag_index_Name on AlbumTag (Name);
create index if not exists AlbumTag_index_Hash on AlbumTag (Hash);

create table if not exists Track (
	Id integer not null primary key autoincrement,
	Album integer not null default 1,
	Artist integer not null default 1,
	Name text not null,
	Hash text not null,
    Abbreviation text not null default '',
	Note text not null default '',
	Disc integer not null default 1,
	Number integer not null default 1,
	constraint Track_unique_AlbumDiscNumber unique (Album, Disc, Number)
);
create index if not exists Track_index_Album on Track (Album);
create index if not exists Track_index_Artist on Track (Artist);
create index if not exists Track_index_Name on Track (Name);
create index if not exists Track_index_Hash on Track (Hash);
insert or ignore into Track (Name, Hash) values ('Unknown', 'UNKNOWN');
insert or ignore into Track (Album, Artist, Name, Hash) values (2, 2, 'None', 'NONE');

create table if not exists TrackMedia (
	Id integer not null primary key autoincrement,
	Subject integer not null,
	Media integer not null,
	Function integer not null default 1,
	constraint TrackMedia_unique_SubjectMediaFunction unique (Subject, Media, Function)
);

create table if not exists TrackTag (
	Id integer not null primary key autoincrement,
	Subject integer not null,
	Category integer not null default 1,
	Name text not null,
	Hash text not null,
	constraint TrackTag_unqiue_SubjectCategoryName unique (Subject, Category, Name)
);
create index if not exists TrackTag_index_Subject on TrackTag (Subject);
create index if not exists TrackTag_index_Category on TrackTag (Category);
create index if not exists TrackTag_index_Name on TrackTag (Name);
create index if not exists TrackTag_index_Hash on TrackTag (Hash);
";
        }

        protected override IFactory<T> GetFactory<T>()
        {
            if (typeof(T) == typeof(IArtist))
                return new GenericFactory<Artist, IArtist>(this) as IFactory<T>;
            if (typeof(T) == typeof(ICountry))
                return new GenericFactory<Country, ICountry>(this) as IFactory<T>;
            else
                return null;
        }

        protected override T GetCached<T>(long id)
        {
            if (typeof(T) == typeof(ICountry))
                return (T)Country.GetById(id);
            else
                return default(T);
        }

        protected override ICollection<T> GetCached<T>(ICommandBuilder builder)
        {
            if (typeof(T) == typeof(ICountry) && builder == allCountriesBuilder)
            {
                return (ICollection<T>)Country.All();
            }

            return null;
        }

        #endregion
    }
     */
}
