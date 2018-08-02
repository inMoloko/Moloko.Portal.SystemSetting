using Moloko.Portal.SystemSetting.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moloko.Portal.SystemSetting.EF
{
    /// <summary>
    /// Хелпер для работы с системными настройками
    /// </summary>
    public static class SettingsHelper
    {
        /// <summary>
        /// Получить системную настройку
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="name">Название настройки</param>
        /// <param name="def">Значение по умолчанию</param>
        /// <returns>Настройка</returns>
        public static string GetSystemSetting(this ISystemSettingsContext context, int customerID, string name,
            string def)
        {
            var setting = context.SystemSettings.FirstOrDefault(i =>
                i.CustomerID == customerID && i.SystemSettingType.Name == name && i.TerminalID == null);
            var history = setting?.History.OrderByDescending(i => i.ModifiedDate).FirstOrDefault();
            var value = history?.SettingValue;
            return value ?? def;
        }

        /// <summary>
        /// Получить системную настройку для терминала
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="terminalID">Ид терминала</param>
        /// <param name="name">Название настройки</param>
        /// <param name="def">Значение по умолчанию</param>
        /// <returns></returns>
        public static string GetTerminalSetting(this ISystemSettingsContext context, int terminalID, string name,
            string def)
        {
            var setting =
                context.SystemSettings.FirstOrDefault(i =>
                    i.SystemSettingType.Name == name && i.TerminalID == terminalID);
            var history = setting?.History.OrderByDescending(i => i.ModifiedDate).FirstOrDefault();
            var value = history?.SettingValue;
            return value ?? def;
        }

        /// <summary>
        /// Получить системную настройку для терминала как число
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="terminalID">Ид терминала</param>
        /// <param name="name">Название настройки</param>
        /// <param name="def">Значение по умолчанию</param>
        /// <returns></returns>
        public static int GetTerminalSetting(this ISystemSettingsContext context, int terminalID, string name, int def)
        {
            var result = context.GetTerminalSetting(terminalID, name, null);
            if (string.IsNullOrEmpty(result))
            {
                return def;
            }

            return int.Parse(result);
        }

        /// <summary>
        /// Получить системную настройку как число
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="name">Название настройки</param>
        /// <param name="def">Значение по умолчанию</param>
        /// <returns>Настройка</returns>
        public static int GetSystemSetting(this ISystemSettingsContext context, int customerID, string name, int def)
        {
            var result = context.GetSystemSetting(customerID, name, null);
            if (string.IsNullOrEmpty(result))
            {
                return def;
            }

            return int.Parse(result);
        }

        /// <summary>
        /// Добавить настройку если нет в систему и в торговый центр
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="settings">Коллекция настроек</param>
        public static void AddSettings(this ISystemSettingsContext context, int customerID,
            IEnumerable<SystemSettingType> settings)
        {
            foreach (SystemSettingType settingType in settings)
            {
                var systemSettingType = context.SystemSettingTypes.FirstOrDefault(i => i.Name == settingType.Name);
                if (systemSettingType == null)
                {
                    systemSettingType = settingType;
                    context.SystemSettingTypes.Add(systemSettingType);
                }

                var setting = systemSettingType.Settings.FirstOrDefault(i => i.CustomerID == customerID);
                if (setting == null)
                {
                    context.SystemSettings.Add(new Model.SystemSetting()
                    {
                        CustomerID = customerID,
                        SystemSettingType = systemSettingType,
                        TerminalID = null
                    });
                }
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Получить значение системной настройки, если нет то добавить
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="systemSettingType">Тип системной настройки</param>
        /// <param name="changeAction">Действие при изменении настройки</param>
        /// <param name="def">Значение по умолчанию</param>
        /// <returns>Значение настройки</returns>
        public static string GetOrCreateSystemSetting(this ISystemSettingsContext context, int customerID,
            SystemSettingType systemSettingType, string changeAction, string def)
        {
            var setting = context.SystemSettings.FirstOrDefault(i =>
                i.CustomerID == customerID && i.SystemSettingType.Name == systemSettingType.Name);
            if (setting == null)
            {
                var st = context.SystemSettingTypes.FirstOrDefault(i => i.Name == systemSettingType.Name);
                if (st == null)
                {
                    st = systemSettingType;
                    context.SystemSettingTypes.Add(st);
                }

                var ss = new Model.SystemSetting()
                {
                    SystemSettingType = st,
                    CustomerID = customerID,
                    ChangeAction = changeAction,
                    TerminalID = null
                };
                context.SystemSettings.Add(ss);
                ss.History.Add(new SystemSettingHistory()
                {
                    SettingValue = def,
                    ModifiedDate = DateTime.Now
                });
                context.SaveChanges();
                return def;
            }

            var history = setting.History.OrderByDescending(i => i.ModifiedDate).FirstOrDefault();
            var value = history?.SettingValue;
            return value ?? def;
        }

        /// <summary>
        /// Получить значение системной настройки, если нет то добавить
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="name">Название настройки</param>
        /// <param name="settingsFormat">Формат настройки</param>
        /// <param name="description">Описание</param>
        /// <param name="type">Тип настройки</param>
        /// <param name="changeAction">Действие при изменении настройки</param>
        /// <param name="def">Значение по умолчанию</param>
        /// <returns>Значение настройки</returns>
        public static string GetOrCreateSystemSetting(this ISystemSettingsContext context, int customerID, string name,
            string settingsFormat, string description, SystemType type, string changeAction, string def)
        {
            var setting =
                context.SystemSettings.FirstOrDefault(i =>
                    i.CustomerID == customerID && i.SystemSettingType.Name == name);
            if (setting == null)
            {
                var st = context.SystemSettingTypes.FirstOrDefault(i => i.Name == name);
                if (st == null)
                {
                    st = new SystemSettingType()
                    {
                        Name = name,
                        Description = description,
                        SystemType = type,
                        Format = settingsFormat
                    };
                    context.SystemSettingTypes.Add(st);
                }

                var ss = new Model.SystemSetting()
                {
                    SystemSettingType = st,
                    CustomerID = customerID,
                    ChangeAction = changeAction,
                    TerminalID = null
                };
                context.SystemSettings.Add(ss);
                ss.History.Add(new SystemSettingHistory()
                {
                    SettingValue = def,
                    ModifiedDate = DateTime.Now
                });
                context.SaveChanges();
                return def;
            }

            var history = setting.History.OrderByDescending(i => i.ModifiedDate).FirstOrDefault();
            var value = history?.SettingValue;
            return value ?? def;
        }

        /// <summary>
        /// Установить значение настройки
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="name">Название настройки</param>
        /// <param name="value">Значение по умолчанию</param>
        public static void SetSystemSetting(this ISystemSettingsContext context, int customerID, string name,
            string value)
        {
            var setting = context.GetSystemSetting(customerID, name, null);
            if (setting != value)
            {
                var ss = context.SystemSettings.FirstOrDefault(i =>
                    i.CustomerID == customerID && 
                    i.SystemSettingType.Name == name && 
                    i.TerminalID == null);
                ss.History.Add(new SystemSettingHistory()
                {
                    SettingValue = value,
                    ModifiedDate = DateTime.Now
                });
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Установить значение настройки для терминала
        /// </summary>
        /// <param name="context">Контекст</param>
        /// <param name="customerID">Ид торгового центра</param>
        /// <param name="terminalID">Ид терминала</param>
        /// <param name="name">Название настройки</param>
        /// <param name="value">Значение по умолчанию</param>
        public static void SetTerminalSetting(this ISystemSettingsContext context, int customerID, int terminalID,
            string name,
            string value)
        {
            var setting = context.GetTerminalSetting(terminalID, name, null);
            if (setting != value)
            {
                var ss = context.SystemSettings.FirstOrDefault(i =>
                    i.CustomerID == customerID && 
                    i.SystemSettingType.Name == name && 
                    i.TerminalID == terminalID);
                ss.History.Add(new SystemSettingHistory()
                {
                    SettingValue = value,
                    ModifiedDate = DateTime.Now
                });
                context.SaveChanges();
            }
        }
    }
}