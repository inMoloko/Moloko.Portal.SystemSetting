using Moloko.Portal.SystemSetting.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moloko.Portal.SystemSetting.EF
{
    /// <summary>
    /// Интерфейс описывает контекст содержащий системные настройки
    /// </summary>
    public interface ISystemSettingsContext
    {
        /// <summary>
        /// Системные настройки
        /// </summary>
        DbSet<Model.SystemSetting> SystemSettings { get; set; }
        /// <summary>
        /// Типы системных настроек
        /// </summary>
        DbSet<SystemSettingType> SystemSettingTypes { get; set; }
        /// <summary>
        /// История системных настроек
        /// </summary>
        DbSet<SystemSettingHistory> SystemSettingHistories { get; set; }

        int SaveChanges();
    }
}
