using Microsoft.Practices.Prism.Mvvm;
using System;
using System.ComponentModel.Composition;

namespace Azelea.Demostrator.ViewModels
{
    [Export]
    public class ShellViewModel : BindableBase, IPartImportsSatisfiedNotification
    {
        [Import(AllowDefault = true)]
        private Action<string> _Callback = null;

        public void OnImportsSatisfied()
        {
            CallbackTraceListener.Instance.Callback = _Callback;
            CallbackTraceListener.Instance.ReplaySavedLogs();
        }
    }
}
