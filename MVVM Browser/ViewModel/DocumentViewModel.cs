using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using MVVM_Browser.Models;

namespace MVVM_Browser.ViewModel {
    public class DocumentViewModel {
        public virtual string FileContent { get; set; }
        public virtual MVVM_Browser.Models.File SelectedFile { get; set; }
        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public DocumentViewModel() {
            Messenger.Default.Register<MVVM_Browser.Models.File>(this, MessageType.SelectedFileChanged, OnSelectedFileChangedMessageRecieved);
        }

        protected void OnSelectedFileChangedMessageRecieved(File file) {
            SelectedFile = file;
        }

        protected void OnSelectedFileChanged() {
        }

        public void GetContent() {
        }


        public void Show(File selectedFile) {
            ShowCore(SelectedFile);
        }
        private void ShowCore(File selectedFile) {
            IDocument document = DocumentManagerService.CreateDocument("SingleFileView", selectedFile, this);
            document.Title = selectedFile.FileName;
            document.DestroyOnClose = true;
            document.Show();
            FileContent = selectedFile.FileContent;
        }

        protected void OnFileContentChanged() {
        }
    }
}
