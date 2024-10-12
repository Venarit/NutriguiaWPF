using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nutriguia.ViewModel
{
    public class PageViewModelBase : BindableBase, INavigationAware
    {
        protected IRegionManager regionManager;

        public PageViewModelBase(IRegionManager regionManager) 
        {
            this.regionManager = regionManager;
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {

        }
    }
}
