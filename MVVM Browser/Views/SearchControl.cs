using MVVM_Browser.ViewModel;

namespace MVVM_Browser.Views {
    [DevExpress.Utils.MVVM.UI.ViewType("SearchFiles")]
    public partial class SearchControl : DevExpress.XtraEditors.XtraUserControl {
        public SearchControl() {
            InitializeComponent();
            mvvmContext1.ViewModelType = typeof(SearchFileViewModel);
            var fluentAPI = mvvmContext1.OfType<SearchFileViewModel>();
            fluentAPI.SetBinding(this.gridControl1, gControl => gControl.DataSource, x => x.Files);
            fluentAPI.SetBinding(this.textEdit1, te => te.EditValue, x => x.SearchText);
        }
    }
}
