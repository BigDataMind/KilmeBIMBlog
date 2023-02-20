using System;
using System.Windows.Input;

namespace ysh.core
{
    public class RouteCommands : ICommand
    {
        #region private members

        private Action mAction = null;
        #endregion


        #region event

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion

        #region Contructor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public RouteCommands(Action action)
        {
            mAction = action;
        }

        #endregion

        #region public methods

        /// <summary>
        /// 检查当前命令是否可用
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// 当命令被执行时委托方法运行
        /// </summary>
        /// <param name="parameter"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void Execute(object parameter)
        {
            mAction();
        }

        #endregion



    }
}
