using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Browser.Models {
    public enum FileType {
        CS,
        VB
    }
    public enum FileFilterType {
        All,
        CS,
        VB
    }

    public enum MessageType {
        SelectedFileChanged
    }
}
