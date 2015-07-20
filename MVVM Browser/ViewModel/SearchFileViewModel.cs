using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Utils.MVVM.Services;
using MVVM_Browser.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace MVVM_Browser.ViewModel {
    public class SearchFileViewModel : ISupportParameter {
        public virtual IList<MVVM_Browser.Models.File> Files { get; set; }
        public virtual string SearchText { get; set; }
        object[] parameters;
        public object Parameter {
            get {
                return parameters[0];
            }
            set {
                parameters = (object[])value;
                Files = parameters[0] as IList<MVVM_Browser.Models.File>;
            }
        }
        protected void OnSearchTextChanged() {
            SearchCore();
        }
        private void SearchCore() {
            for(int i = Files.Count - 1; i >=0; i--)
            {
                if (!Files[i].FileContent.Contains(SearchText))
                    Files.Remove(Files[i]);
            }
        }
    }
}
