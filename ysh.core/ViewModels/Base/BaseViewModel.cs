using System;
using System.ComponentModel;
using System.Windows.Input;

namespace ysh.core
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged =(sender,e) => { };

        #region Get or set command properties change when notification

        //(1)此处用Nuget包配置

        //(2)手动配置DataProperty as Notification
       


        #endregion


        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

