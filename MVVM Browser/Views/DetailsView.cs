using MVVM_Browser.ViewModel;

namespace MVVM_Browser.Views {
    public partial class DetailsView : DevExpress.XtraEditors.XtraUserControl {
        public DetailsView() {
            InitializeComponent();
            Init();
        }
        private void Init() {
            this.mvvmContext1.ViewModelType = typeof(DetailsViewModel);
            var fluentAPI = this.mvvmContext1.OfType<DetailsViewModel>();
            fluentAPI.SetBinding(this.propertyGridControl1, pg => pg.SelectedObject, x => x.FileInfo);
        }
    }
}
