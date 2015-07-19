using MVVM_Browser.ViewModel;
using System;
namespace MVVM_Browser.Views {
    public partial class SingleFileView : DevExpress.XtraEditors.XtraUserControl {
        public SingleFileView() {
            InitializeComponent();
            Init();
        }
        private void Init() {
            this.mvvmContext1.ViewModelType = typeof(DocumentViewModel);
            var fluentAPI = this.mvvmContext1.OfType<DocumentViewModel>();
            fluentAPI.SetBinding(this.memoEdit1, me => me.EditValue, x => x.FileContent);
            fluentAPI.WithEvent<EventArgs>(this, "Load")
            .EventToCommand(x => x.GetContent());
        }
    }
}
