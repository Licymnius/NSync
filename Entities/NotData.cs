using System;
using System.Xml.Serialization;

namespace NSync.Entities
{
    [Serializable]
    [XmlRoot("NData", IsNullable = false)]
    public class NotData
    {
        /// <summary>
        /// Федеральный номер
        /// </summary>
        public string NotID { get; set; }

        /// <summary>
        /// Фамилия 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Имя и отчество
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Номер лицензии на право нот. деятельности
        /// </summary>
        public string N_Licen { get; set; }

        /// <summary>
        /// Дата лицензии на право нот. деятельности
        /// </summary>
        public string D_Licen { get; set; }

        /// <summary>
        /// Номер приказа о назначении
        /// </summary>
        public string N_Prikaz { get; set; }

        /// <summary>
        /// Дата приказа о назначении
        /// </summary>
        public string D_Prikaz { get; set; }

        /// <summary>
        /// Код нотариального округа
        /// </summary>
        public int District { get; set; }

        /// <summary>
        /// Наименование нотариального округа
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// Статус действующего нотариуса - логический тип
        /// </summary>
        public bool Real { get; set; }

        /// <summary>
        /// Адрес нотариальной конторы
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Район 
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Проезд
        /// </summary>
        public string Proezd { get; set; }

        /// <summary>
        /// Код телефона
        /// </summary>
        public string Phone_cod { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// e-mail (Email)
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Адрес Интернет
        /// </summary>
        public string Internet { get; set; }

        /// <summary>
        /// ИНН нотариуса
        /// </summary>
        public string INN { get; set; }

        /// <summary>
        /// Примечание
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// Языки перевода
        /// </summary>
        public LanguageType[] Languages { get; set; }

        /// <summary>
        /// Переданные архивы
        /// </summary>
        public Archive[] Archives { get; set; }
    }
}