using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moloko.Portal.SystemSetting.Model
{
    /// <summary>
    /// История изменений настройки
    /// </summary>
    public class SystemSettingHistory
    {
        /// <summary>
        /// Первичный ключ
        /// </summary>
        public int SystemSettingHistoryID { get; set; }
        /// <summary>
        /// Значение настройки
        /// </summary>
        public string SettingValue { get; set; }
        /// <summary>
        /// Дата изменения
        /// </summary>
        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Связь с системной настройкой
        /// </summary>
        public int SystemSettingID { get; set; }
        /// <summary>
        /// Системная настройка
        /// </summary>
        public SystemSetting SystemSetting { get; set; }
    }
}
