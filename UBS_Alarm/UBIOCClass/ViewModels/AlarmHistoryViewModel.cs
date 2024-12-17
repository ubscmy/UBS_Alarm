using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UBIOCClass.Commands;
using UBIOCClass.Models;
using UBIOCClass.Services;

namespace UBIOCClass.ViewModels
{
    public class AlarmHistoryViewModel : NotifyPropertyChangedBase
    {

        private HistoryModel _HistoryModel = new HistoryModel();
        public HistoryModel HisotryModel { get => _HistoryModel; set => SetProperty(ref _HistoryModel, value); }

        private ICommandModel _ICommandModel = new ICommandModel();
        public ICommandModel ICommandModel { get => _ICommandModel; set => SetProperty(ref _ICommandModel, value); }

        private readonly INavigationService _navigationService;

        private void DataSelect(object _)
        {
            HisotryModel.AlarmData = dbCommand.HistoryDataSelect(ref _HistoryModel);
            NotifyPropertyChanged();
        }

        public void TableCreate(object _) 
        { 
            if(dbCommand.CreateTable())
                MessageBox.Show("Table 생성 완료");
        }
        private void DataInsert(object _)
        {
            dbCommand.Hisotry_DataInsert(ref _HistoryModel);
            DataSelect(null);
        }

        private void DataSearch(object _)
        {
            HisotryModel.AlarmData = dbCommand.HistoryDataSearch(ref _HistoryModel);
            NotifyPropertyChanged();
        }


        public AlarmHistoryViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            DataSelect(null);
            HistoryCommands();
        }

        private void HistoryCommands()
        {
            ICommandModel.DB_Select = new DelegateCommand(DataSelect);
            ICommandModel.DB_Insert = new DelegateCommand(DataInsert);
            ICommandModel.DB_SearchSelect = new DelegateCommand(DataSearch);
            ICommandModel.CreateTable = new DelegateCommand(TableCreate);
        }
    }


}
