using Bechelor.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bechelor.Core.Domin.Core
{
    public class MediaFile : BaseEntity
    {
        public string FileName { get; set; }
        public string FileUrl { get; set; }
        public int FileId { get; set; }
        public int FileSize { get; set; }
    }
}
