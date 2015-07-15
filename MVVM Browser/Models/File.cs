using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVM_Browser.Models {
    public class File {
        // Fields...
        private FileType _FileType;
        private string _FileName;
        private string _Path;

        public string Path {
            get { return _Path; }
            set {
                _Path = value;
            }
        }

        public string FileName {
            get { return _FileName; }
            set {
                _FileName = value;
            }
        }

        public FileType FileType {
            get { return _FileType; }
            set {
                _FileType = value;
            }
        }
        public File() {

        }        
    }
}
