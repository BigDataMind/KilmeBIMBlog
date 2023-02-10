using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ysh.core
{
    public static class CoreAssembly
    {
        /// <summary>
        /// 获取程序集位置
        /// </summary>
        /// <returns></returns>
        public static string GetAssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }
}
