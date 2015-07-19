using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM.Services;
using MVVM_Browser.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;


namespace MVVM_Browser.ViewModel {
    public class FileCollectionViewModel {
        public virtual string FolderPath { get; set; }
        public virtual MVVM_Browser.Models.File SelectedFile { get; set; }
        public virtual FileFilterType FilterType { get; set; }

        protected IDocumentManagerService DocumentManagerService { get { return this.GetService<IDocumentManagerService>(); } }
        public virtual IList<MVVM_Browser.Models.File> Files {
            get;
            set;

        }
        public FileCollectionViewModel() {
            FolderPath = string.Empty;
            Files = new BindingList<MVVM_Browser.Models.File>();
        }
        private void GetFiles(string path) {
            if (!string.IsNullOrEmpty(path)) {
                if (Files.Count > 0) {
                    Files.Clear();
                }
                string[] filePaths = filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(fn => fn.EndsWith(".cs") || fn.EndsWith(".vb")).ToArray();
                for (int i = 0; i < filePaths.Length; i++) {
                    Models.File file = new Models.File();
                    file.Path = filePaths[i];
                    file.FileName = filePaths[i].Split('\\').Last();
                    string type = file.FileName.Split('.').Last();
                    if (type == "cs")
                        file.FileType = FileType.CS;
                    else
                        file.FileType = FileType.VB;
                    Files.Add(file);
                }
                if (FilterType != FileFilterType.All)
                    FilterFiles();
            }
        }
        private void FilterFiles() {
            FileType type = FileType.CS;
            if (FilterType == FileFilterType.VB)
                type = FileType.VB;
            for (int i = Files.Count - 1; i >= 0; i--) {
                if (Files[i].FileType != type)
                    Files.Remove(Files[i]);
            }
        }

        public void Show() {
            ShowCore(SelectedFile);
        }
        private void ShowCore(MVVM_Browser.Models.File selectedFile) {
            DevExpress.Mvvm.IDocument document = DocumentManagerService.CreateDocument("SingleFileView", null, this);
            document.Title = selectedFile.FileName;
            document.DestroyOnClose = true;
            document.Show();
        }


        protected virtual void OnFolderPathChanged() {
            GetFiles(FolderPath);
        }
        protected virtual void OnFilterTypeChanged() {
            GetFiles(FolderPath);
        }
        protected virtual void OnSelectedFileChanged() {
            Messenger.Default.Send<MVVM_Browser.Models.File>(SelectedFile, MessageType.SelectedFileChanged);
        }

        protected void NotifyResultAndResultTextChanged() {
            this.RaisePropertyChanged(x => x.SelectedFile); // change-notification for the Result
        }

    }
}
