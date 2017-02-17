using System.Collections.Generic;

namespace Arbetsprov.CN.Models.Entities
{
    public class Category
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public IList<ImageInfo> Images { get; set; }
    }
}
