using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using UBIOCClass.Commands;
using UBIOCClass.Models;
using UBIOCClass.Services;
using UBIOCClass.Views;

namespace UBIOCClass.ViewModels
{
    public class AlarmRegisterViewModel : NotifyPropertyChangedBase
    {
        private RegisterModel _RegisterModel = new RegisterModel();
        public RegisterModel RegisterModel { get => _RegisterModel; set => SetProperty(ref _RegisterModel, value); }
        //public ICommand DBInsert { get; set; }

        private ICommandModel _ICommandModel = new ICommandModel();
        public ICommandModel ICommandModel { get => _ICommandModel; set => SetProperty(ref _ICommandModel, value); }


        private void DataInsert(object _)
        {
            bool bSuccess = dbCommand.Regster_DataInsert(ref _RegisterModel);
            if (bSuccess == true)
                DataSelect(null);
        }
        private void DataUpdate(object _)
        {
            bool bSuccess = dbCommand.Register_DataUpdate(ref _RegisterModel);
            if (bSuccess == true)
                DataSelect(null);
        }
        private void DataDelete(object _)
        {
            bool bSuccess = dbCommand.Register_DataDelete(ref _RegisterModel);
            if (bSuccess == true)
                DataSelect(null);
        }

        private void DataSelect(object _)
        {
            RegisterModel.AlarmData = dbCommand.RegisterDataSelect(ref _RegisterModel);
            NotifyPropertyChanged();
        }

        public AlarmRegisterViewModel(INavigationService navigationService)
        {
            DataSelect(null);
            RegisterCommands();
        }

        private void RegisterCommands()
        {
            ICommandModel.DB_Insert = new DelegateCommand(DataInsert);
            ICommandModel.DB_Update = new DelegateCommand(DataUpdate);
            ICommandModel.DB_Delete = new DelegateCommand(DataDelete);
            ICommandModel.DB_Select = new DelegateCommand(DataSelect);
        }

    }
}
