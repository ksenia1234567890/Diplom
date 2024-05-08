using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Npgsql;

namespace Диплом_1
{
    internal static class Program
    {
        public static ApplicationContext Form0;

        static string ConnectionString = "Host=localhost;Port=5432;Username=postgres;Password=cxNTVJas;Database=Families";
        public static NpgsqlConnection connection = new NpgsqlConnection(ConnectionString);

        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Form0 = new ApplicationContext(new Entrance());
            Application.Run(Form0);
        }
        public static void ChangeForm(Form oldForm, Form newForm)
        {
            Form0.MainForm = newForm;
            oldForm.Close();
            Form0.MainForm.Show();
        }
    }
}
