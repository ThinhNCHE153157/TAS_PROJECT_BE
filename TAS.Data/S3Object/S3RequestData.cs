using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.S3Object
{
    public class S3RequestData
    {
        public string Name { get; set; } = null!;
        public Stream? InputStream { get; set; } = null!;
        public string BucketName { get; set; } = null!;
    }
}
