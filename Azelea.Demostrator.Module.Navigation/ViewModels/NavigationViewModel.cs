using Azelea.Demostrator.Infrastructure.Interfaces;
using Azelea.Demostrator.Module.Navigation.Models;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;

namespace Azelea.Demostrator.Module.Navigation.ViewModels
{
    [Export]
    public class NavigationViewModel : BindableBase, IPartImportsSatisfiedNotification
    {
        [ImportMany]
        private Lazy<IMenu>[] _InternalMenus = null;

        private List<Category> _Categories;
        public List<Category> Categories
        {
            get { return _Categories; }
            set
            {
                SetProperty(ref _Categories, value);
            }
        } 

        public void OnImportsSatisfied()
        {
            Categories = new List<Category>
            {
                new Category
                {
                    Name = "数据结构",
                    Menus = new List<Menu>
                    {
                        new Menu { Name = "二叉树" },
                        new Menu { Name = "图" },
                        new Menu { Name = "二叉搜索树" }
                    }
                },
                new Category
                {
                    Name = "密码学",
                    Menus = new List<Menu>
                    {
                        new Menu { Name = "DES" },
                        new Menu { Name = "MD5" },
                        new Menu { Name = "RSA" }
                    }
                }
            };
        }
    }
}
