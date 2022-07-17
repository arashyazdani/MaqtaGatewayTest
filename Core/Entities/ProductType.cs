using Core.Entities.BaseEntities;
using System.ComponentModel;

namespace Core.Entities
{
    [DisplayName("Product Type")]
    public class ProductType : BaseEntity
    {
        public string Name { get; set; }
    }
}
