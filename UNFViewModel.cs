using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace UNFScoreRecorder
{
    class UNFViewModel : ViewModelBase
    {
        private UNFModel _model = new UNFModel();
        private int updatePeriod = 60; //In Minutes
        private ICommand _start;
        private ICommand _stop;
        private bool _isRunning = false;
        private string _test;

        public UNFViewModel()
        {

        }

        public UNFModel Model
        {
            get { return _model; }
        }

        public string Cookie
        {
            get { return Model.Cookie; }
            set
            {
                if (value != Model.Cookie)
                {
                    Model.Cookie = value;
                    RaisePropertyChanged("Cookie");
                }
            }
        }

        public string Xversion
        {
            get { return Model.XVERSION; }
            set
            {
                if(value != Model.XVERSION)
                {
                    Model.XVERSION = value;
                    RaisePropertyChanged("XVERSION");
                }
            }
        }

        public string OutputFile
        {
            get { return Model.cachedData; }
            set
            {
                if(value != Model.cachedData)
                {
                    Model.cachedData = value;
                    RaisePropertyChanged("OutputFile");
                }
            }
        }

        public bool IsRunning
        {
            get { return _isRunning; }
            set
            {
                if(value != _isRunning)
                {
                    _isRunning = value;
                    RaisePropertyChanged("IsRunning");
                }
            }
        }

        public bool IsNotRunning
        {
            get { return !_isRunning; }
        }

        public ICommand StartCommand
        {
            get
            {
                if (_start == null)
                    _start = new RelayCommand(continuousUpdating);
                return _start;
            }
        }

        public ICommand StopCommand
        {
            get
            {
                if (_stop == null)
                    _stop = new RelayCommand(stopUpdates);
                return _stop;
            }
        }

        public string test
        {
            get { return _test; }
            set
            {
                if (value != _test)
                {
                    _test = value;
                    RaisePropertyChanged("test");
                }
            }
        }

        private async void continuousUpdating()
        {
            IsRunning = true;
            while (IsRunning)
            {
                Model.update();
                await Task.Delay(updatePeriod * 1000 * 3600);
            }
        }

        private void stopUpdates()
        {
            Console.WriteLine("Hello");
            IsRunning = false;
        }
    }
}
