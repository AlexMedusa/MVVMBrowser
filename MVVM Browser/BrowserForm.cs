using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraEditors;
using MVVM_Browser.ViewModel;
namespace MVVM_Browser {
    public partial class BrowserForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        public BrowserForm() {
            InitializeComponent();
        }

        private void Init() {
            this.mvvmContext1.ViewModelType = typeof(DocumentViewModel);
            this.mvvmContext1.RegisterService(DocumentManagerService.Create(this.tabbedView1));
            var fluentAPI = this.mvvmContext1.OfType<DocumentViewModel>();
            fluentAPI.BindCommand(this.barButtonItem1, (x) => x.Show(this.filesView1.SelectedFile), null);
        }
        private void BrowserForm_Load(object sender, System.EventArgs e) {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            XtraMessageBox.Show(this.filesView1.SelectedFile.FileName);
        }
    }
}
