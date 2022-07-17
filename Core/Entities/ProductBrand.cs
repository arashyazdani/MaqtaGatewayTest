using Core.Entities.BaseEntities;
using System.ComponentModel;

namespace Core.Entities
{
    [DisplayName("Product Brand")]
    public class ProductBrand : BaseEntity
    {
        public string Name { get; set; }
    }
}
