using DevExpress.Mvvm;
using MVVM_Browser.Models;
using System.IO;

namespace MVVM_Browser.ViewModel {
    public class DetailsViewModel {
        public virtual object FileInfo { get; set; }
        public virtual MVVM_Browser.Models.File SelectedFile { get; set; }
        public DetailsViewModel() {
            Messenger.Default.Register<MVVM_Browser.Models.File>(this, MessageType.SelectedFileChanged, OnSelectedFileChangedMessageRecieved);
        }
        protected void OnSelectedFileChangedMessageRecieved(Models.File file) {
            FileInfo = new FileInfo(file.Path);
        }
    }
}
