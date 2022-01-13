using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EntityGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;// 窗体居中

            InitializeComponent();

            DBTypes dBTypes = new DBTypes();
            this.DataContext = dBTypes;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string dbstring = $"server={txtHost.Text};uid={txtAccount.Text};pwd={txtPassword.Text};database={txtDB.Text}";
            DbType dbtype = (DbType)cbType.SelectedIndex;
            string snamespace = txtNamespace.Text;

            txtTip.Text = "生成中...";
            txtTip.Foreground = Brushes.Brown;

            btnCreate.IsEnabled = false;
            new Thread(() =>
            {
                DBRun(dbstring, dbtype, snamespace);
            }).Start();

        }

        private void DBRun(string dbstring, DbType dbtype, string snamespace)
        {
            try
            {

                SqlSugarClient db = new SqlSugarClient(new ConnectionConfig()
                {
                    ConnectionString = dbstring,//连接符字串
                    DbType = dbtype,
                    IsAutoCloseConnection = true //不设成true要手动close
                }); ;

                string path = $"{Environment.CurrentDirectory}\\Entity";
                db.DbFirst.IsCreateAttribute().CreateClassFile(path, snamespace);

                this.Dispatcher.Invoke(new Action(() => {
                    txtTip.Text = "成功生成到Entity目录下！";
                    txtTip.Foreground = Brushes.Green;
                }));
                
            }
            catch (Exception ex)
            {
                this.Dispatcher.Invoke(new Action(() => {
                    txtTip.Text = "生成失败！" + ex.Message;
                    txtTip.Foreground = Brushes.Red;
                }));
                
            }
            this.Dispatcher.Invoke(() => {
                btnCreate.IsEnabled = true;
            });
            

        }
    }
}
