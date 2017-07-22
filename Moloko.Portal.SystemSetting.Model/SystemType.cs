using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Moloko.Portal.SystemSetting.Model
{
    /// <summary>
    /// Перечень подсистем
    /// </summary>
    public enum SystemType
    {
        /// <summary>
        /// Подсистема Терминал
        /// </summary>
        Terminal = 1,
        /// <summary>
        /// Подсистема информационного взаимодействия
        /// </summary>
        Integration = 2,
        /// <summary>
        /// Подсистема портал
        /// </summary>
        ContentManagement = 3,
        /// <summary>
        /// Подсистема карты на сайте
        /// </summary>
        SiteMap = 4,
        /// <summary>
        /// Подсистема работы с ботами
        /// </summary>
        Bot = 5,
        /// <summary>
        /// Подсистемы каналов коммуникаций
        /// </summary>
        CommunicationChannel = 6,
    }
}
