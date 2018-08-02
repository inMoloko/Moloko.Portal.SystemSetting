using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moloko.Portal.SystemSetting.Model
{
    /// <summary>
    /// Настройка
    /// </summary>
    public class SystemSetting
    {
        public int SystemSettingID { get; set; }
        /// <summary>
        /// Идентификатор торгового центра
        /// </summary>
        public int? CustomerID { get; set; }
        /// <summary>
        /// Связь с терминалом
        /// </summary>
        public int? TerminalID { get; set; }
        /// <summary>
        /// Тип настройки 
        /// </summary>
        public int SystemSettingTypeID { get; set; }
        /// <summary>
        /// Связанный объект типа настройки
        /// </summary>
        public virtual SystemSettingType SystemSettingType { get; set; }
        /// <summary>
        /// Это действие произойдет когда настройка изменится
        /// </summary>
        public string ChangeAction { get; set; }
        /// <summary>
        /// История изменений
        /// </summary>
        public virtual ICollection<SystemSettingHistory> History { get; set; } = new HashSet<SystemSettingHistory>();
    }
}
