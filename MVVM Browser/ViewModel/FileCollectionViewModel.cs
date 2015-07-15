using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm.POCO;
using MVVM_Browser.Models;
using System.ComponentModel;
using System.IO;


namespace MVVM_Browser.ViewModel {
    public class FileCollectionViewModel {
        public virtual string FolderPath { get; set; }
        private MVVM_Browser.Models.File core;
        public virtual MVVM_Browser.Models.File SelectedFile {
            get { return core; }
            set { core = value;  NotifyResultAndResultTextChanged(); }
        }
        IList<MVVM_Browser.Models.File> filesCore;
        public IList<MVVM_Browser.Models.File> Files {
            get {
                return this.filesCore;
            }
        }
        public FileCollectionViewModel() {
            FolderPath = string.Empty;
            filesCore = new BindingList<MVVM_Browser.Models.File>();
        }
        private void GetFiles(string path) {
            if (!string.IsNullOrEmpty(path)) {
                if (filesCore.Count > 0) {
                    filesCore.Clear();
                }
                string[] filePaths = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).Where(fn => fn.EndsWith(".cs") || fn.EndsWith(".vb")).ToArray();
                for (int i = 0; i < filePaths.Length; i++) {
                    Models.File file = new Models.File();
                    file.Path = filePaths[i];
                    file.FileName = filePaths[i].Split('\\').Last();
                    string type = file.FileName.Split('.').Last();
                    if (type == "cs")
                        file.FileType = FileType.CS;
                    else
                        file.FileType = FileType.VB;
                    filesCore.Add(file);
                }

            }
        }
        protected virtual void OnFolderPathChanged() {
            //this.RaiseCanExecuteChanged(x => x.DeleteTask(null));
            GetFiles(FolderPath);
        }
        protected virtual void OnSelectedFileChanged() {
        }

        protected void NotifyResultAndResultTextChanged() {
            this.RaisePropertyChanged(x => x.SelectedFile); // change-notification for the Result
        }

    }
}
