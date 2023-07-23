﻿using System.Text.Json.Serialization;

namespace CardsAgainstHumanity.DatabaseAccess.Entities;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Language
{
    Abkhazian,
    Afar,
    Afrikaans,
    Akan,
    Albanian,
    Amharic,
    Arabic,
    Aragonese,
    Armenian,
    Assamese,
    Avaric,
    Avestan,
    Aymara,
    Azerbaijani,
    Bambara,
    Bashkir,
    Basque,
    Belarusian,
    Bengali,
    Bislama,
    Bosnian,
    Breton,
    Bulgarian,
    Burmese,
    Catalan,
    Chamorro,
    Chechen,
    Chichewa,
    Chinese,
    Church,
    Chuvash,
    Cornish,
    Corsican,
    Cree,
    Croatian,
    Czech,
    Danish,
    Divehi,
    Dutch,
    Dzongkha,
    English,
    Esperanto,
    Estonian,
    Ewe,
    Faroese,
    Fijian,
    Finnish,
    French,
    Western,
    Fulah,
    Gaelic,
    Galician,
    Ganda,
    Georgian,
    German,
    Greek,
    Kalaallisut,
    Guarani,
    Gujarati,
    Haitian,
    Hausa,
    Hebrew,
    Herero,
    Hindi,
    Hiri,
    Hungarian,
    Icelandic,
    Ido,
    Igbo,
    Indonesian,
    Interlingua,
    Interlingue,
    Inuktitut,
    Inupiaq,
    Irish,
    Italian,
    Japanese,
    Javanese,
    Kannada,
    Kanuri,
    Kashmiri,
    Kazakh,
    Central,
    Kikuyu,
    Kinyarwanda,
    Kirghiz,
    Komi,
    Kongo,
    Korean,
    Kuanyama,
    Kurdish,
    Lao,
    Latin,
    Latvian,
    Limburgan,
    Lingala,
    Lithuanian,
    Luba,
    Luxembourgish,
    Macedonian,
    Malagasy,
    Malay,
    Malayalam,
    Maltese,
    Manx,
    Maori,
    Marathi,
    Marshallese,
    Mongolian,
    Nauru,
    Navajo,
    North,
    South,
    Ndonga,
    Nepali,
    Norwegian,
    Sichuan,
    Occitan,
    Ojibwa,
    Oriya,
    Oromo,
    Ossetian,
    Pali,
    Pashto,
    Persian,
    Polish,
    Portuguese,
    Punjabi,
    Quechua,
    Romanian,
    Romansh,
    Rundi,
    Russian,
    Northern,
    Samoan,
    Sango,
    Sanskrit,
    Sardinian,
    Serbian,
    Shona,
    Sindhi,
    Sinhala,
    Slovak,
    Slovenian,
    Somali,
    Southern,
    Spanish,
    Sundanese,
    Swahili,
    Swati,
    Swedish,
    Tagalog,
    Tahitian,
    Tajik,
    Tamil,
    Tatar,
    Telugu,
    Thai,
    Tibetan,
    Tigrinya,
    Tonga,
    Tsonga,
    Tswana,
    Turkish,
    Turkmen,
    Twi,
    Uighur,
    Ukrainian,
    Urdu,
    Uzbek,
    Venda,
    Vietnamese,
    Volapük,
    Walloon,
    Welsh,
    Wolof,
    Xhosa,
    Yiddish,
    Yoruba,
    Zhuang,
    Zulu,
}
