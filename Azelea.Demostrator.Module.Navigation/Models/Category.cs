using System.Collections.Generic;

namespace Azelea.Demostrator.Module.Navigation.Models
{
    public class Category
    {
        public string Name { get; set; }
        public List<Menu> Menus { get; set; }
    }
}
