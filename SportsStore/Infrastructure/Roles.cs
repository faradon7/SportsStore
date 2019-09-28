using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Infrastructure
{
    public class Roles
    {
        public const string Admin = "Administrator";
        public const string AdminDescription = "Администратор сайта, имеет полный доступ";
        public const string User = "User";
        public const string UserDecription = "Пользователь сайта, имеет доступ только к своему профилю";
    }
}
