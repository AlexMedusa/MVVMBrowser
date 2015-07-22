using DevExpress.Mvvm;
using System.Collections.Generic;

namespace MVVM_Browser.ViewModel {
    public class SearchFileViewModel : ISupportParameter {
        public virtual IList<MVVM_Browser.Models.File> Files { get; set; }
        private MVVM_Browser.Models.File[] FilesCore { get; set; }
        public virtual string SearchText { get; set; }
        object[] parameters;
        public object Parameter {
            get {
                return parameters[0];
            }
            set {
                parameters = (object[])value;
                Files = parameters[0] as IList<MVVM_Browser.Models.File>;
                FilesCore = new Models.File[Files.Count];
                Files.CopyTo(FilesCore, 0);
            }
        }
        protected void OnSearchTextChanged() {
            SearchCore();
        }
        private void SearchCore() {
            Files.Clear();
            foreach (Models.File file in FilesCore) {
                Files.Add(file);
            }
            for (int i = Files.Count - 1; i >= 0; i--) {
                if (!Files[i].FileContent.ToLower().Contains(SearchText.ToLower()))
                    Files.Remove(Files[i]);
            }
        }
    }
}
