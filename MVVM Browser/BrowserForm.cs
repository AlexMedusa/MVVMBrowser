using DevExpress.Utils.MVVM.Services;
using MVVM_Browser.Models;
using MVVM_Browser.ViewModel;
namespace MVVM_Browser {
    public partial class BrowserForm : DevExpress.XtraBars.Ribbon.RibbonForm {
        public BrowserForm() {
            InitializeComponent();
            if (!DesignMode)
                Init();
        }

        private void Init() {
            this.mvvmContext1.ViewModelType = typeof(DocumentViewModel);
            this.mvvmContext1.RegisterService(DocumentManagerService.Create(this.tabbedView1));
            var fluentAPI = this.mvvmContext1.OfType<DocumentViewModel>();
            fluentAPI.BindCommand(this.barButtonItem1, (x) => x.Show(x.SelectedFile), x => x.SelectedFile);
            this.filesView1.TabbedView = this.tabbedView1;
        }
        private void BrowserForm_Load(object sender, System.EventArgs e) {

        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e) {
            this.filesView1.Text = "dsfdf";
            this.filesView1.SelectedFile = new File();
        }
    }
}
