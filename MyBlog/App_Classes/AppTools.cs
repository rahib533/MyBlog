using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Web;

namespace MyBlog.App_Classes
{
    public static class AppTools
    {
        private static Size meh;

        public static Size MeqaleBoyuk
        {
            get
            {
                meh.Height = Convert.ToInt32(ConfigurationManager.AppSettings["boyukH"]);
                meh.Width = Convert.ToInt32(ConfigurationManager.AppSettings["boyukW"]);
                return meh;
            }
        }

        private static Size mehh;

        public static Size MeqaleOrta
        {
            get
            {
                mehh.Height = Convert.ToInt32(ConfigurationManager.AppSettings["ortaH"]);
                mehh.Width = Convert.ToInt32(ConfigurationManager.AppSettings["ortaW"]);
                return mehh;
            }
        }

        private static Size meeh;

        public static Size MeqaleKicik
        {
            get
            {
                meeh.Height = Convert.ToInt32(ConfigurationManager.AppSettings["kicikH"]);
                meeh.Width = Convert.ToInt32(ConfigurationManager.AppSettings["kicikW"]);
                return meeh;
            }
        }

    }
}