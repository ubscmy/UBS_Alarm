using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UBIOCClass.ViewModels;

namespace UBIOCClass.Models
{
    public class ICommandModel
    {

        public ICommand? DB_Update { get; set; }
        public ICommand? DB_Insert { get; set; }
        public ICommand? DB_Select { get; set; }
        public ICommand? DB_Delete{ get; set; }
        
        public ICommand? DB_SearchSelect { get; set; }
        public ICommand? CreateTable { get; set; }
        public ICommand? AlarmTest { get; set; }
    }
}
