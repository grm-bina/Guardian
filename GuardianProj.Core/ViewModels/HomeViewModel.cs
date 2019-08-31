using GuardianProj.Core.Interfaces;
using GuardianProj.Core.Models;
using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuardianProj.Core.ViewModels
{
    public class HomeViewModel: MvxViewModel
    {
        #region fields
        private readonly IMvxNavigationService _navService;
        private readonly IHttpService _suplier;
        private SectionSearchResult _sectionsSearchResult;
        private List<Section> _sections;
        private Section _selectedSection;

        #endregion

        #region props
        public List<Section> Sections
        {
            get => _sections;
            private set => SetProperty(ref _sections, value);
        }
        
        public Section SelectedSection
        {
            get => _selectedSection;
            set => SetProperty(ref _selectedSection, value);
        }

        public bool CanGoBack
        {
            get
            {
                return PreviousSections?.Count > 0;
            }
        }

        public Stack<Section> PreviousSections { get; }

        #endregion


        #region ctor
        public HomeViewModel(IMvxNavigationService navServ, IHttpService suplier)
        {
            _navService = navServ;
            _suplier = suplier;
            PreviousSections = new Stack<Section>();
        }
        #endregion


        #region lifecicle
        //public override Task Initialize()
        //{
        //    // there is a bug in the first view initialize: DONT PUT HERE NOTHING ASYNC!!!
        //}

        public override void Prepare()
        {
            GoToManePage = new MvxCommand<string>(async (sectionID) =>
            {
                PreviousSections.Push(Sections.First(s=> s.Id==sectionID));
                await RaisePropertyChanged("CanGoBack");
                await _navService.Navigate<StoriesPageViewModel, string>(sectionID);
            });

            // this shit is only for header-changing, working not every time and close the programm :(
            FakeGoBack = new MvxCommand(async () =>
            {
                SelectedSection = PreviousSections.Pop();
                await RaisePropertyChanged("CanGoBack");
            });
        }

        public override async void ViewAppeared()
        {
            await GetSections();

            GoToManePage.Execute("allNews");

        }

        #endregion


        #region commands
        public IMvxCommand GoToManePage { get; private set; }
        public IMvxCommand FakeGoBack { get; private set; }

        #endregion

        #region methods
        public async Task GetSections()
        {
            // pseudo section for all news
            Section all = new Section() { Id = "allNews", WebTitle = "All News" };

            // real sections from guardian

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(Constants.API_KEY_PARAM, Constants.API_KEY);
            var result = await _suplier.GetAsync<SectionSearchResult>(Constants.SECTIONS_API_URL, parameters);
            _sectionsSearchResult = result;

            List<Section> tempList = new List<Section>();
            // adding pseudo section
            tempList.Add(all);
            // adding real sections
            tempList.AddRange(_sectionsSearchResult.SearchResponse.Sections?.ToList());

            Sections = tempList;
            SelectedSection = tempList[0];

        }

    #endregion
}
}
