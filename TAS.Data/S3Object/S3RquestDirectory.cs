using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TAS.Data.S3Object
{
    public class S3RquestDirectory
    {
        public string BucketName { get; set; } = null!;
        public string Directory { get; set; } = null!;
        public string Prefix { get; set; } = null!;
    }
}
