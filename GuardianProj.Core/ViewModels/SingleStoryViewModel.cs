using GuardianProj.Core.Interfaces;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace GuardianProj.Core.ViewModels
{
    public class SingleStoryViewModel : MvxViewModel<String>
    {
        #region fields
        private readonly IMvxNavigationService _navService;
        private readonly IHttpService _suplier;
        private string _webUrl;

        #endregion

        #region ctor
        public SingleStoryViewModel(IMvxNavigationService navServ, IHttpService suplier)
        {
            _navService = navServ;
            _suplier = suplier;
        }
        #endregion

        #region props
        public string WebUrl
        {
            get => _webUrl;
            private set => SetProperty(ref _webUrl, value);
        }
        #endregion

        #region lifecicle
        public override void Prepare(string url)
        {
            WebUrl = url;
        }

        #endregion
    }
}
