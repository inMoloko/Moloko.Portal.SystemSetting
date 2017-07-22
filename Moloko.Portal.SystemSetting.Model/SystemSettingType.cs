using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moloko.Portal.SystemSetting.Model
{
    /// <summary>
    /// Системная настройка
    /// </summary>
    public class SystemSettingType
    {
        public SystemSettingType()
        {
        }

        public SystemSettingType(string name, string description, SystemType systemType, string format)
        {
            Name = name;
            Description = description;
            SystemType = systemType;
            Format = format;
        }

        /// <summary>
        /// Ид
        /// </summary>
        public int SystemSettingTypeID { get; set; }
        /// <summary>
        /// Имя настройки
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Описание настройки
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Тип подсистемы системы
        /// </summary>
        public SystemType SystemType { get; set; }
        /// <summary>
        /// Формат настройки string, datetime, integer
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// Настройки для данного типа
        /// </summary>
        public virtual HashSet<SystemSetting> Settings { get; set; } = new HashSet<SystemSetting>();
    }
}
