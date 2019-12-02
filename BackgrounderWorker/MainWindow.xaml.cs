using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackgrounderWorker
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Init_BackgroundWoker();
        }
        BackgroundWorker bw;
        private void Init_BackgroundWoker()
        {
            //实例化BackgroundWorker类。
            bw = new BackgroundWorker();
            //指示BackgroundWorker能否报告进度更新(Bool类型)
            bw.WorkerReportsProgress = true;
            //是否支持异步取消操作,当该属性值为True是，将可以成功调用CancelAsync方法，否则将引发异常。
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            //RunWorkerAsync():用于开始执行后台操作。其中100是参数。
            //bw.RunWorkerAsync(100);
        }

        int num1, num2;
        bool stopDrag = false; //取消异步操作的标志。


        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            //装换成BackgroundWorker对象。
            BackgroundWorker bwer = sender as BackgroundWorker;
            //Argumeng：异步获取参数的值。
            if (e.Argument != null)
            {
                if (stopDrag) //如果点击了取消异步操作。
                {
                    num1 = Convert.ToInt32(e.Argument);
                }
                else
                {
                    num2 = Convert.ToInt32(e.Argument);
                }
                for (int i = num1; i <= num2; i++)
                {
                    //CancellationPending:指示应用程序是否已请求取消后台操作
                    if (bwer.CancellationPending)
                    {
                        e.Cancel = true;
                        return;
                    }
                    num1 = i; //为num1赋值。
                    string lblContent = "已完成:" + i + "%";
                    //引发ProgressChanged事件。
                    bwer.ReportProgress(i, lblContent);
                    Thread.Sleep(100);
                }
            }
        }

        private void bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //ProgressPercentage:获取异步任务的百分比。
            this.proGressBar1.Value = e.ProgressPercentage;
            //UserState：获取唯一的用户状态。
            this.lblContent.Content = e.UserState.ToString();
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show(string.Format("异常信息为:{0}", e.Error.ToString()));
                return;
            }
            if (e.Cancelled) //Cancelled:指示异步操作是否已经取消。
            {
                this.lblContent.Content = "操作已暂停！";
            }
            else
            {
                this.lblContent.Content = "处理完毕！";
            }
        }
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            //将开始按钮设置为禁用状态。
            this.btn_Start.IsEnabled = false;
            //将结束按钮色设置为启用状态。
            this.btn_End.IsEnabled = true;
            //IsBusy:指示BackgroundWorker是否正在运行异步操作，运行为True，否则为false。
            if (bw.IsBusy)
            {
                MessageBox.Show("异步执行开启中");
                return;
            }
            //RunWorkerAsync():用于开始执行后台操作。其中100是参数。
            if (stopDrag)
            {
                bw.RunWorkerAsync(num1);
            }
            else
            {
                bw.RunWorkerAsync(100);
            }
        }

        private void btnEnd_Click(object sender, RoutedEventArgs e)
        {
            //将开始按钮设置为启用状态。
            this.btn_Start.IsEnabled = true;
            //将结束按钮色设置为禁用状态。
            this.btn_End.IsEnabled = false;
            //CancelAsync():请求取消挂起的异步操作。
            //并非每次调用CancelAsync()都能确保异步操作，CancelAsync()通常不适用于取消一个紧密执行的操作，更适用于在循环体中执行
            bw.CancelAsync();
            stopDrag = true;
        }
    }
}
