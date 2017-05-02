using BookingBreakerBusinessLogic;
using NLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace BookingBreaker
{
    public partial class BookingParcer : ServiceBase
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public BookingParcer()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            _logger.Info("Service Starting");
            // Set up a timer to trigger every minute.  
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 1800000; // 60 seconds  
            timer.AutoReset = true;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(this.OnTimer);
            timer.Start();
            _logger.Info("Service Started");
        }

        protected override void OnStop()
        {
            _logger.Info("Service Stopped");
        }

        public void OnTimer(object sender, System.Timers.ElapsedEventArgs args)
        {
            _logger.Info("Service Invocing");
            PlanetaParcingBusinessLogic.ExecuteParcing().ConfigureAwait(false).GetAwaiter().GetResult();
            _logger.Info("Service Invoced");
        }
    }
}
