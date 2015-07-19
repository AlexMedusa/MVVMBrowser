using DevExpress.Utils.MVVM.Services;
using DevExpress.XtraBars.Docking2010.Views.Tabbed;
using DevExpress.XtraTreeList;
using MVVM_Browser.Models;
using MVVM_Browser.ViewModel;
using System;
using System.Windows.Forms;

namespace MVVM_Browser.Views {
    public partial class FilesView : DevExpress.XtraEditors.XtraUserControl {
        public FilesView() {
            InitializeComponent();
            if (!DesignMode)
                Init();
        }
        private TabbedView _TabbedView;
        private File core;
        public File SelectedFile {
            get { return core; }
            set { core = value; }
        }
        public TabbedView TabbedView {
            get { return _TabbedView; }
            set {
                _TabbedView = value;
                this.mvvmContext1.RegisterService(DocumentManagerService.Create(value));
            }
        }

        private void Init() {
            IninCombobox();
            InitMVVMContext();
        }

        private void IninCombobox() {
            this.repositoryItemComboBox1.Items.Add(FileFilterType.All);
            this.repositoryItemComboBox1.Items.Add(FileFilterType.CS);
            this.repositoryItemComboBox1.Items.Add(FileFilterType.VB);
            this.barEditItem1.EditValue = FileFilterType.All;

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

        private void InitMVVMContext() {
            mvvmContext1.ViewModelType = typeof(FileCollectionViewModel);
            var fluentAPI = mvvmContext1.OfType<FileCollectionViewModel>();
            fluentAPI.SetBinding(this.buttonEdit1, bEdit => bEdit.EditValue, x => x.FolderPath);
            fluentAPI.SetBinding(this.treeList1, gControl => gControl.DataSource, x => x.Files);
            fluentAPI.SetBinding(this, f => f.SelectedFile, x => x.SelectedFile);
            fluentAPI.SetBinding(this.barEditItem1, bi => bi.EditValue, x => x.FilterType);
            fluentAPI.WithEvent<TreeList, FocusedNodeChangedEventArgs>(this.treeList1, "FocusedNodeChanged")
                .SetBinding<File>(x => x.SelectedFile, args => args.Node.TreeList.GetDataRecordByNode(args.Node) as File, (tl, file) => tl.FocusedNode = tl.FindNodeByFieldValue("Path", file.Path));
            fluentAPI.WithEvent<MouseEventArgs>(this.treeList1, "MouseDoubleClick")
                .EventToCommand(x => x.Show(), x => x.Button);
        }


        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e) {

        }

        private void treeList1_MouseDoubleClick(object sender, MouseEventArgs e) {

        }
    }
}
