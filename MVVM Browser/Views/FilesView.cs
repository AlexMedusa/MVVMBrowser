using DevExpress.XtraTreeList;
using MVVM_Browser.Models;
using MVVM_Browser.ViewModel;
using System.Windows.Forms;

namespace MVVM_Browser.Views {
    public partial class FilesView : DevExpress.XtraEditors.XtraUserControl {
        public FilesView() {
            InitializeComponent();
            Init();
        }
        private File core;
        public File SelectedFile {
            get { return core; }
            set { core = value; }
        }

        private void Init() {
            mvvmContext1.ViewModelType = typeof(FileCollectionViewModel);
            var fluentAPI = mvvmContext1.OfType<FileCollectionViewModel>();
            fluentAPI.SetBinding(this.buttonEdit1, bEdit => bEdit.EditValue, x => x.FolderPath);
            fluentAPI.SetBinding(this.treeList1, gControl => gControl.DataSource, x => x.Files);
            fluentAPI.SetBinding(this, f => f.SelectedFile, x => x.SelectedFile);

            fluentAPI.WithEvent<TreeList, FocusedNodeChangedEventArgs>(this.treeList1, "FocusedNodeChanged")
                .SetBinding(x => x.SelectedFile, args => args.Node.TreeList.GetDataRecordByNode(args.Node) as File, (tl, file) => tl.FocusedNode = tl.FindNodeByFieldValue("Path", file.Path));

            //fluentAPI.SetTrigger(x => x.SelectedFile, (file) => { this.SelectedFile = file; });

            //this.buttonEdit1.EditValue = @"C:\MVVMTest";
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e) {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) {
                FolderBrowserDialog dialog = new FolderBrowserDialog();
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK) {
                    buttonEdit1.EditValue = dialog.SelectedPath;
                }
            }
        }

        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e) {

        }
    }
}
